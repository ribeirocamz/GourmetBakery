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
            this.usuario = usuario; // <- Esse Método irá chamar o objeto "usuario"
            lblCumprimento.Text = $"Olá {usuario.NomeCompleto},\n Escolha uma opção abaixo";
        }
    }
}
