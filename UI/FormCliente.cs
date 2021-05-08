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
    public partial class FormCliente : Form
    {
        public FormCliente()
        {
            InitializeComponent();
        }
        void limpar()
        {
            txtId.Text = null;
            txtEndereço.Text = null;
            txtNome.Text = null;
            txtEmail.Text = null;
            txtTelefone.Text = null;
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            if (txtId.Text != "")
            {
                cliente.Id = int.Parse(txtId.Text);
            }
            cliente.Nome = txtNome.Text;
            cliente.Email = txtEmail.Text;
            cliente.Endereco = txtEndereço.Text;
            cliente.Telefone = txtTelefone.Text;
            cliente.Salvar();
            carrega_datagrid();
        }

        private void FormCliente_Load(object sender, EventArgs e)
        {
            carrega_datagrid();
        }

        private void carrega_datagrid()
        {
            Cliente cliente = new Cliente();
            dgvDados.AutoGenerateColumns = false;
            dgvDados.DataSource = cliente.Todos();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            FormInicial voltar = new FormInicial();
            this.Hide();
            voltar.Show();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Cliente cliInstance = new Cliente();
            cliInstance.Id = int.Parse(txtId.Text);
            var retorno = MessageBox.Show("Confirma exclusão do item?", txtId.Text ,MessageBoxButtons.YesNo);
            if(retorno == DialogResult.Yes)
            {
                cliInstance.Excluir();
                carrega_datagrid();
                limpar();
            }
            
        }
    }
}
