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
            Clear();
            this.ActiveControl = txtNome;
            LoadData();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Clear();
        }

        void Clear()
        {
            txtNome.Text = txtEndereco.Text = txtEmail.Text = txtSexo.Text = txtTelefone.Text = txtCPF.Text = txtDataNascimento.Text = "";
            btnSalvar.Text = "Salvar";
            btnDeletar.Enabled = false;
            model.ID = 0;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            model.Nome = txtNome.Text.Trim();
            model.Telefone = txtTelefone.Text.Trim();
            model.Email = txtEmail.Text.Trim();
            model.CPF = txtCPF.Text.Trim();
            model.Sexo = txtSexo.Text.Trim();
            model.Endereco = txtEndereco.Text.Trim();
            model.DataNascimento = DateTime.Parse(txtDataNascimento.Text.Trim());

            using(EFBANCO_CLIENTEEntities db = new EFBANCO_CLIENTEEntities())
            {
                db.Clientes.Add(model);
                db.SaveChanges();
            }
            Clear();
            LoadData();
            MessageBox.Show("Salvo com sucesso!");
        }

        void LoadData()
        {
            dataGridView1.AutoGenerateColumns = false;
            using(EFBANCO_CLIENTEEntities db = new EFBANCO_CLIENTEEntities())
            {

                //dgClienteID.IsDataBound = db.Clientes.ToList<Cliente>();

                dataGridView1.DataSource = db.Clientes.ToList<Cliente>();
               
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
