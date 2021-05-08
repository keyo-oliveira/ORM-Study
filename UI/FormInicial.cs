using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AulaORM
{
    public partial class FormInicial : Form
    {
        public FormInicial()
        {
            InitializeComponent();
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            FormCliente cliente = new FormCliente();
            this.Hide();
            cliente.Show();
        }

        private void btnFornecedor_Click(object sender, EventArgs e)
        {
            TelaFornecedor forn = new TelaFornecedor();
            this.Hide();
            forn.Show();
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            FormProdutos prod = new FormProdutos();
            this.Hide();
            prod.Show();
        }

        private void btnVendedor_Click(object sender, EventArgs e)
        {
            FormVendedor vendedor = new FormVendedor();
            this.Hide();
            vendedor.Show();
        }
    }
}
