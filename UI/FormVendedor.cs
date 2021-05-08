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
    public partial class FormVendedor : Form
    {
        public FormVendedor()
        {
            InitializeComponent();
        }
        private void CarregaDatagrid()
        {
            dgvDados.AutoGenerateColumns = false;
            dgvDados.DataSource = new ClassVendedor().Todos();
        }
        private void Limpar()
        {
            txtArea.Text = null;
            txtCPF.Text = null;
            txtArea.Text = null;
            txtEmail.Text = null;
            txtId.Text = null;
            txtNome.Text = null;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
             ClassVendedor vendedor = new ClassVendedor();
            if(txtId.Text != "")
            {
                vendedor.Id = int.Parse(txtId.Text);
                btnSalvar.Text = "alterar";
            }
            vendedor.Nome = txtNome.Text;
            vendedor.CPF = txtCPF.Text;
            vendedor.Email = txtEmail.Text;
            vendedor.Area = txtArea.Text;
            vendedor.Salvar();
            CarregaDatagrid();
            Limpar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            ClassVendedor vendedor = new ClassVendedor();
            vendedor.Id = int.Parse(txtId.Text);
            var retorno = MessageBox.Show("Confirma exclusão do item?", txtId.Text, MessageBoxButtons.YesNo);
            if (retorno == DialogResult.Yes)
            {
                vendedor.Excluir();
                CarregaDatagrid();
                Limpar();
            }

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            ClassVendedor vendedor = new ClassVendedor();
            if (txtId.Text.Equals(""))
            {
                vendedor.Id = int.Parse(txtId.Text);
            }
            if (txtNome.Text.Equals(""))
            {
                vendedor.Nome = txtNome.Text;
            }
            foreach (ClassVendedor vendedores in vendedor.Busca())
            {
                txtId.Text = vendedores.Id.ToString();
                txtNome.Text = vendedores.Nome;
                txtCPF.Text = vendedores.CPF;
                txtEmail.Text = vendedores.Email;
                txtArea.Text = vendedores.Area;
            }
            Limpar();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            FormInicial voltar = new FormInicial();
            this.Hide();
            voltar.Show();
        }

        private void FormVendedor_Load(object sender, EventArgs e)
        {
            CarregaDatagrid();
        }

        private void btnTabelas_Click(object sender, EventArgs e)
        {
            ClassVendedor vendedor = new ClassVendedor();
            vendedor.CreateTable();
            CarregaDatagrid();
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dgvDados_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ClassVendedor vendedor = (ClassVendedor)dgvDados.Rows[e.RowIndex].DataBoundItem;
            txtId.Text = vendedor.Id.ToString();
            txtNome.Text = vendedor.Nome;
            txtArea.Text = vendedor.Area;
            txtCPF.Text = vendedor.CPF;
            txtEmail.Text = vendedor.Email;
            btnSalvar.Text = "alterar";
        }
    }
}
