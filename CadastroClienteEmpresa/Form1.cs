using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroClienteEmpresa
{
    public partial class Form1 : Form
    {
        Cliente model = new Cliente();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        void Clear()
        {
            txtNome.Text = txtEndereco.Text = txtEmail.Text = txtSexo.Text = txtTelefone.Text = txtCPF.Text = txtDataNascimento.Text = "";
        }
    }
}
