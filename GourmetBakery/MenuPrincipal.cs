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
    public partial class MenuPrincipal : Form
    {
        //Váriaveis globais (Métodos)
        Model.Usuario usuario = new Model.Usuario(); //Tornar o objeto "usuario" global.

        public MenuPrincipal(Model.Usuario usuario) //Assinatura do MenuPrincipal (Regra para ser utilizado)
        {
            InitializeComponent();
            this.usuario = usuario; // <- Esse Método irá chamar o objeto "usuario" (juntar os dois)

            //Atribuir texto ao lblCumprimento na tela inicial:
            lblCumprimento.Text = $"Olá {usuario.NomeCompleto},\n Escolha uma opção abaixo";
        }

        private void btnComandas_Click(object sender, EventArgs e)
        {
            FormComandas formComandas = new FormComandas(usuario);
            formComandas.ShowDialog();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            FormUsuarios formUsuarios = new FormUsuarios(usuario);
            formUsuarios.ShowDialog(); // Mostrar o form
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            FormProdutos formProdutos = new FormProdutos(usuario);
            formProdutos.ShowDialog();
        }

        private void btnCaixa_Click(object sender, EventArgs e)
        {
            FormCaixa formCaixa = new FormCaixa(usuario);
            formCaixa.ShowDialog();
        }
    }
}
