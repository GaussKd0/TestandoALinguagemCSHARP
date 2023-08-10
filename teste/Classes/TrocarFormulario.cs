using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace teste.Classes
{
    internal class TrocarFormulario
    {
        private Form mainForm;
        private Form formularioAtual;

        public void GerenciadorDeFormularios(Form mainForm)
        {
            this.mainForm = mainForm;
        }

        public void MostrarFormulario(Form newForm)
        {
            if (formularioAtual != null)
            {
                formularioAtual.Hide();
                formularioAtual.Dispose();
            }

            formularioAtual = newForm;
            formularioAtual.TopLevel = false;
            formularioAtual.FormBorderStyle = FormBorderStyle.None;
            formularioAtual.Dock = DockStyle.Fill;
            mainForm.Controls.Add(formularioAtual);
            formularioAtual.Show();
        }
    }
}
