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
            //VERIFICAR SE O USUARIO DIGITOU E-MAIL E SENHA:
            if (txbEmail.Text.Length < 6)
            {
                MessageBox.Show("Digite um e-mail válido!", "Erro!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (txbSenha.Text.Length < 3)
            {
                MessageBox.Show("Digite uma senha válida!", "Erro!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else //SE AS INFORMAÇÕES ESTIVEREM CORRETAS, PROSSEGUIR:
            {
                //Criar objeto "usuario" da classe Usuario
                Model.Usuario usuario = new Model.Usuario();

                //Inserir os valores dos campos (login e senha) nos atributos do "usuário":
                usuario.Email = txbEmail.Text;
                usuario.Senha = txbSenha.Text;

                //Criar tabela que irá receber o resultado do Login (SELECT):
                DataTable resultado = usuario.Logar();

                 
                //VERIFICAR SE ACERTOU E-MAIL E SENHA:
                if (resultado.Rows.Count == 0)
                {
                    MessageBox.Show("E-mail e/ou senha inválidas!", "Erro!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //Armazenar as informações vindas do BD no objeto "usuario":
                    usuario.Id = int.Parse(resultado.Rows[0]["id"].ToString());
                    usuario.NomeCompleto = resultado.Rows[0]["nome_completo"].ToString();

                    //Mudar para o Menu Principal:
                    MenuPrincipal menuPrincipal = new MenuPrincipal(usuario);


                    Hide(); //esconder a janela atual

                    menuPrincipal.ShowDialog(); //Mostrar Menu Principal

                    Show(); //Voltar a tela de login ao sair do menu principal
                }

            }
        }
    }
}
