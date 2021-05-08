using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business;

namespace AulaORM
{
    public partial class FormProdutos : Form
    {
        public FormProdutos()
        {
            InitializeComponent();
        }

        private void FormProdutos_Load(object sender, EventArgs e)
        {
            CarregaDatagrid();
        }
        private void CarregaDatagrid()
        {
            dgvDados.AutoGenerateColumns = false;
            dgvDados.DataSource = new ClassProduto().Todos();
        }
        private void Limpar()
        {
            txtId.Text = null;
            txtNome.Text = null;
            txtQtd.Text = null;
            txtValor.Text = null;
        }

        private void dgvDados_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            ClassProduto produtos = (ClassProduto)dgvDados.Rows[e.RowIndex].DataBoundItem;
            txtId.Text = produtos.Id.ToString();
            txtNome.Text = produtos.Nome;
            txtQtd.Text = produtos.Quantidade.ToString();
            txtValor.Text = produtos.Valor.ToString();
            btnSalvar.Text = "alterar";
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            FormInicial voltar = new FormInicial();
            this.Hide();
            voltar.Show();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            
            ClassProduto produtos = new ClassProduto();
            if(txtId.Text != "")
            {
                produtos.Id = int.Parse(txtId.Text);
                btnSalvar.Text = "alterar";
            }
            produtos.Nome = txtNome.Text;
            produtos.Valor = txtValor.Text;
            produtos.Quantidade = txtQtd.Text;
            produtos.Salvar();
            
            
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            ClassProduto produtos = new ClassProduto();
            produtos.Id = int.Parse(txtId.Text);
            var retorno = MessageBox.Show("Confirma exclusão do item?", txtId.Text, MessageBoxButtons.YesNo);
            if (retorno == DialogResult.Yes)
            {
                produtos.Excluir();
                CarregaDatagrid();
                Limpar();
            }
        }

        private void btnBusca_Click(object sender, EventArgs e)
        {
            ClassProduto produtos = new ClassProduto();
            if (txtId.Text.Equals(""))
            {
                produtos.Id = int.Parse(txtId.Text);
            }
            if (txtNome.Text.Equals(""))
            {
                produtos.Nome = txtNome.Text;
            }
            foreach(ClassProduto produto in produtos.Busca())
            {
                txtId.Text = produto.Id.ToString();
                txtNome.Text = produto.Nome;
                txtQtd.Text = produto.Quantidade.ToString();
                txtValor.Text = produto.Valor.ToString();
            }
        }

        private void btnTabelas_Click(object sender, EventArgs e)
        {
            ClassProduto produto = new ClassProduto();
            produto.CreateTable();
            CarregaDatagrid();
        }
    }
}
