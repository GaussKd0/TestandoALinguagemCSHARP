using System.Windows.Forms;

namespace teste.Classes
{
    internal class Mensagem
    {
        public void Mensagens(string mensagem, string tipoDaMensagem)
        {
            switch (tipoDaMensagem)
            {
                case "erro":
                    MessageBox.Show(mensagem, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case "sucesso":
                    MessageBox.Show(mensagem, "Sucesso!", MessageBoxButtons.OK);
                    break;

                case "information":
                    MessageBox.Show(mensagem, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case "aviso":
                    MessageBox.Show(mensagem, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                default:
                    MessageBox.Show("Erro Ao Configurar Mensagem", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
            

        }
    }
}
