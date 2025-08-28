using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GourmetBakery
{
    public partial class LoginUsuario : Form
    {
        public LoginUsuario()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            //Verificar se a pessoa digitou o email e a senha:
            if(txbEmail.Text.Length < 6)
            {
                MessageBox.Show("Digite um e-mail válido!", "Erro!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            }else if(txbSenha.Text.Length < 3)
            {
                MessageBox.Show("Digite uma senha válida!", "Erro!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Se as informações estiverem corretas, prosseguir:

            }
        }
    }
}
