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
    public partial class FormProdutos : Form
    {
        //Váriaveis globais:
        Model.Usuario usuario;
        int idSelecionado = 0;
        Model.Produto produto = new Model.Produto();

        //Construtor:
        public FormProdutos(Model.Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;

            ListarCategoriasCmb();

            AtualizarDgv();
        }

        public void AtualizarDgv()
        {
            dgvProdutos.DataSource = produto.Listar();
        }

        public void ListarCategoriasCmb()
        {
            Model.Categoria categoria = new Model.Categoria();

            //Tabela para receber o resultado do SELECT:
            DataTable tabela = categoria.Listar();

            foreach (DataRow dr in tabela.Rows)
            {
                cmbCategoriaCadastro.Items.Add($"{dr["id"]} - {dr["nome"]}");
                cmbCategoriaEditar.Items.Add($"{dr["id"]} - {dr["nome"]}");
            }
        }

        public void ResetarCampos()
        {
            AtualizarDgv();

            //Limpar campos de edição:
            txbNomeEditar.Clear();
            txbPrecoEditar.Clear();


            //Retornar o idSelecionado para 0:
            idSelecionado = 0;

            //Retornar o texto padrão do apagar:
            lblApagarDescricao.Text = "Selecione o usuário que deseja apagar.";

            //Desabilitar os grbs:
            grbApagar.Enabled = false;
            grbEditar.Enabled = false;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            //Tratamento de erros:
            if (txbNomeCadastro.Text.Length < 2)
            {
                MessageBox.Show("O nome do produto deve ter no mínimo 3 caracteres!",
                "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txbPrecoCadastro.Text.Length == 0)
            {
                MessageBox.Show("Preencha o campo Preço!",
                "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (cmbCategoriaCadastro.SelectedIndex == 0)
            {
                MessageBox.Show("Selecione uma categoria!",
                "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Realizar o cadastro


                // Salvar os valores dos campos nos atributos do obj:
                produto.Nome = txbNomeCadastro.Text;
                produto.Preco = double.Parse(txbPrecoCadastro.Text);

                string ctgSelecionada = cmbCategoriaCadastro.SelectedItem.ToString();
                string[] separar = ctgSelecionada.Split('-');
                int id = int.Parse(separar[0].Trim());

                produto.IdCategoria = id;
                produto.IdRespCadastro = usuario.Id;

                // Executar o INSERT:
                if (produto.Cadastrar())
                {
                    MessageBox.Show("Produto cadastrado com êxito!", "Sucesso!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Atualizar o dgv:
                    AtualizarDgv();

                    // Apagar os campos de cadastro:
                    ResetarCampos();


                }
                else
                {
                    MessageBox.Show("Falha ao cadastrar o produto!",
                    "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void dgvProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Pegar a linha selecionada:
            int ls = dgvProdutos.SelectedCells[0].RowIndex;

            //Colocar os valores das células nos txb de edição:
            txbNomeEditar.Text = dgvProdutos.Rows[ls].Cells[1].Value.ToString();
            txbPrecoEditar.Text = dgvProdutos.Rows[ls].Cells[2].Value.ToString();

            //Armazenar o id de quem foi selecionado:
            idSelecionado = (int)dgvProdutos.Rows[ls].Cells[0].Value;

            //Ativar o grbEditar:
            grbEditar.Enabled = true;

            //Ativar o grbApagar:
            grbApagar.Enabled = true;

            //Ajustes no grbApagar:
            lblApagarDescricao.Text = $"Apagar: {dgvProdutos.Rows[ls].Cells[1].Value}";

            
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            //Perguntar se realmente quer apagar:
            DialogResult result = MessageBox.Show("Tem certeza que deseja excluir este produto?", "Atenção!",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                //Prosseguir com a exclusão dos dados:
                Model.Produto produtoApagar = new Model.Produto();

                produtoApagar.Id = idSelecionado;

                if (produtoApagar.Apagar())
                {
                    MessageBox.Show("Produto apagado com successo!", "Sucesso!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ResetarCampos();

                }
                else
                {
                    MessageBox.Show("Falha ao apagar o produto!",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            //Tratamento de erros
            if (txbNomeCadastro.Text.Length < 2)
            {
                MessageBox.Show("O nome do produto deve ter no mínimo 3 caracteres!",
                "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txbPrecoCadastro.Text.Length == 0)
            {
                MessageBox.Show("Preencha o campo Preço!",
                "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (cmbCategoriaCadastro.SelectedIndex == 0)
            {
                MessageBox.Show("Selecione uma categoria!",
                "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Realizar a edição
                produto.Nome = txbNomeEditar.Text;
                produto.Preco = double.Parse(txbPrecoEditar.Text);

                string ctgSelecionada = cmbCategoriaEditar.SelectedItem.ToString();
                string[] separar = ctgSelecionada.Split('-');
                int id = int.Parse(separar[0].Trim());

                produto.IdCategoria = id;
                produto.IdRespCadastro = usuario.Id;

                // Executar o INSERT:
                if (produto.Cadastrar())
                {
                    MessageBox.Show("Produto modificado com êxito!", "Sucesso!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Atualizar o dgv:
                    AtualizarDgv();

                    // Apagar os campos de cadastro:
                    ResetarCampos();


                }
                else
                {
                    MessageBox.Show("Falha ao modificar o produto!",
                    "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }

        private void cmbCategoriaCadastro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCadastrar.PerformClick();
            }
        }

        private void cmbCategoriaEditar_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnEditar.PerformClick();
            }
        }
    }
}
