using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace teste.Classes
{
    internal class Registrar
    {

        Thread t1;

        private string user;

        Classes.Mensagem mensagem = new Classes.Mensagem();
        private string databaseFilePath = "userDatabase.json";
        private List<Classes.Usuario> usuarios;

        public Registrar()
        {
            CarregarUsuarios();
        }

        public void Registro(string username, string password)
        {
            usuarios.Add(new Classes.Usuario { Username = username, Password = password });
            SalvarUsuarios();
            mensagem.Mensagens("Registrado Com Sucesso!", "sucesso");
        }

        public void Login(string username, string password)
        {
            this.user = username;

            if (File.Exists(databaseFilePath))
            {
                var user = usuarios.Find(u => u.Username == username && u.Password == password);
                if (user != null)
                {
                    t1 = new Thread(abrirFormulario);
                    t1.SetApartmentState(ApartmentState.STA);
                    t1.Start();

                }

                else
                {
                    mensagem.Mensagens("Não foi possível encontrar o nome de usuário", "erro");
                }
            }
        }

        private void abrirFormulario()
        {
            Application.Run(new FormularioPrincipal(this.user));
           
            //FormularioPrincipal principal = new FormularioPrincipal(username);
            //principal.ShowDialog();
        }

        private void CarregarUsuarios()
        {
            if (File.Exists(databaseFilePath))
            {
                string jsonData = File.ReadAllText(databaseFilePath);
                usuarios = JsonSerializer.Deserialize<List<Classes.Usuario>>(jsonData);
            }

            else
            {
                usuarios = new List<Classes.Usuario>();
            }
        }

        private void SalvarUsuarios()
        {
            string jsonData = JsonSerializer.Serialize(usuarios, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(databaseFilePath, jsonData);
        }
    }
}
