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
    public partial class TelaFornecedor : Form
    {
        public TelaFornecedor()
        {
            InitializeComponent();
        }
        private void Limpar()
        {
            txtCnpj.Text = null;
            txtEmail.Text = null;
            txtId.Text = null;
            txtRazao.Text = null;
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Fornecedor fornecedor = new Fornecedor();
            if (txtId.Text != "")
            {
                fornecedor.Id = int.Parse(txtId.Text);
                btnSalvar.Text = "alterar";
            }
            fornecedor.Razao = txtRazao.Text;
            fornecedor.Cnpj = txtCnpj.Text;
            fornecedor.Email = txtEmail.Text;
            fornecedor.Salvar();
            carrega_datagridview();
        }

        private void TelaFornecedor_Load(object sender, EventArgs e)
        {
            carrega_datagridview();
        }

        private void carrega_datagridview()
        {
            dgvDados.AutoGenerateColumns = false;
            dgvDados.DataSource = new Fornecedor().Todos();
        }

        private void dgvDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Fornecedor fornecedor = (Fornecedor)dgvDados.Rows[e.RowIndex].DataBoundItem;
            txtCnpj.Text = fornecedor.Cnpj;
            txtId.Text = fornecedor.Id.ToString();
            txtEmail.Text = fornecedor.Email;
            txtRazao.Text = fornecedor.Razao;
            btnSalvar.Text = "Alterar";
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Fornecedor fornecedor = new Fornecedor();
            if (!txtId.Text.Equals(""))
            {
                fornecedor.Id = int.Parse(txtId.Text);
            }
            if (!txtRazao.Text.Equals(""))
            {
                fornecedor.Razao = txtRazao.Text;
            }
            if (!txtCnpj.Text.Equals(""))
            {
                fornecedor.Cnpj = txtCnpj.Text;
            }
            
            foreach(Fornecedor forn in fornecedor.Busca())
            {
                txtCnpj.Text = forn.Cnpj;
                txtEmail.Text = forn.Email;
                txtId.Text = forn.Id.ToString();
                txtRazao.Text = forn.Razao;
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            FormInicial voltar = new FormInicial();
            this.Hide();
            voltar.Show();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Fornecedor forn = new Fornecedor();
            forn.Id = int.Parse(txtId.Text);
            var retorno = MessageBox.Show("Confirma exclusão do item?", txtId.Text, MessageBoxButtons.YesNo);
            if(retorno == DialogResult.Yes)
            {
                forn.Excluir();
                carrega_datagridview();
                Limpar();
            }
        }
    }
}
