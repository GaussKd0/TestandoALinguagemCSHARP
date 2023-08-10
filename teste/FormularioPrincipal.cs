using System;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;

namespace teste
{

    public partial class FormularioPrincipal : Form
    {
        Point lastPoint;

        private string NomeUsuario;

        private string getUsuario()
        {
            return NomeUsuario;
        }

        private void setNomeUsuario(string Usuario)
        {
            this.NomeUsuario = Usuario;
        }

        public FormularioPrincipal(String Nome)
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            if (File.Exists(Nome + ".png"))
            {
                pictureBox1.Image = Image.FromFile(Nome + ".png");
            }

            else if (File.Exists(Nome + ".jpg"))
            {
                pictureBox1.Image = Image.FromFile(Nome + ".jpg");
            }

            else if (File.Exists(Nome + ".jpeg"))
            {
                pictureBox1.Image = Image.FromFile(Nome + ".jpeg");
            }

            else if (File.Exists(Nome + ".jpg"))
            {
                pictureBox1.Image = Image.FromFile(Nome + ".gif");
            }
       

            MakePictureBoxRound(pictureBox1, panel1, 10, Color.FromArgb(20,20,20));
            label1.Text = Nome;
            setNomeUsuario(Nome);
            
        }

        private void MakePictureBoxRound(PictureBox picBox, Panel panel, int borderWidth, Color borderColor)
        {
            Rectangle rectangle = new Rectangle(0, 0, picBox.Width - 1, picBox.Height - 1);

            // Criar uma região elíptica com base no retângulo
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(rectangle);

            // Definir a região elíptica como a região do PictureBox
            picBox.Region = new Region(path);

            // Configurações visuais do painel
            panel.BorderStyle = BorderStyle.None;
            panel.Padding = new Padding(borderWidth);
            panel.BackColor = borderColor;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void FormularioPrincipal_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void FormularioPrincipal_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string novaImagemLocal = getUsuario() + Path.GetExtension(openFileDialog.FileName);
                   
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                    }

                    File.Copy(openFileDialog.FileName, novaImagemLocal, true);
                    pictureBox1.Image = Image.FromFile(novaImagemLocal);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Classes.Mensagem mensagem = new Classes.Mensagem();
            string[] fileUrls = {
                "https://code.visualstudio.com/sha/download?build=stable&os=win32-x64-user",
                "https://us.download.nvidia.com/GFE/GFEClient/3.27.0.112/GeForce_Experience_v3.27.0.112.exe",
                "https://discord.com/api/downloads/distributions/app/installers/latest?channel=stable&platform=win&arch=x86",
                "https://laptop-updates.brave.com/download/BRV030?bitness=64",
                "https://cdn.cloudflare.steamstatic.com/client/installer/SteamSetup.exe",

            };

            foreach (string url in fileUrls)
            {
                string fileName = "Arq.exe"; 
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        Console.WriteLine($"Baixando {fileName}");
                        client.Headers.Add("User-Agent", "Mozilla/5.0"); 
                        client.DownloadFile(url, fileName);
                    }

                    Console.WriteLine($"Instalando {fileName}");
                    Process process = new Process();
                    process.StartInfo.FileName = fileName;
                    process.Start();
                    process.WaitForExit();

                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                        Console.WriteLine($"Arquivo {fileName} excluído após instalação.");
                    }
                }
                catch (Exception ex)
                {
                    mensagem.Mensagens($"Erro ao baixar ou instalar {fileName}: {ex.Message}", "erro");
                }
            }


            mensagem.Mensagens("Todos Os Arquvios Estao Instalados!", "sucesso");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Classes.Mensagem mensagem = new Classes.Mensagem();

            mensagem.Mensagens("Espere os scripts serem baixados e executado", "aviso");

            string script1 = @"
                iwr -useb https://raw.githubusercontent.com/spicetify/spicetify-cli/master/install.ps1 | iex
            ";

            string script2 = @"
                iwr -useb https://raw.githubusercontent.com/spicetify/spicetify-marketplace/main/resources/install.ps1 | iex

            ";

            ExecutePowerShellScript(script1);
            ExecutePowerShellScript(script2);

            mensagem.Mensagens("Scripts executados.", "sucesso");
        }

        static void ExecutePowerShellScript(string script)
        {

            Classes.Mensagem mensagem = new Classes.Mensagem();

            string tempScriptFile = Path.GetTempFileName() + ".ps1";
            File.WriteAllText(tempScriptFile, script);

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "powershell.exe";
            startInfo.Arguments = $"-ExecutionPolicy Bypass -File \"{tempScriptFile}\"";

            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();

                process.WaitForExit();

                mensagem.Mensagens("Saída:", "sucesso");
                mensagem.Mensagens(output, "sucesso");

            }

            File.Delete(tempScriptFile);
        }
    
    }
}
