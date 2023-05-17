using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
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
                string email = txtEmail.Text;

                if (IsValidEmail(email))
                {
                    // O e-mail é válido, faça o que desejar aqui
                    MessageBox.Show("O e-mail é válido. Salvando...");

                    if (model.ID == 0)
                        db.Clientes.Add(model);
                    else
                        db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();

                    Clear();
                    LoadData();
                    MessageBox.Show("Salvo com sucesso!");

                    // Adicione o código para salvar o e-mail ou realizar outras ações necessárias
                }
                else
                {
                    // O e-mail é inválido, exiba uma mensagem de erro
                    MessageBox.Show("O e-mail inserido é inválido. Por favor, insira um e-mail válido.");
                    Clear();
                    this.ActiveControl = txtNome;
                    LoadData();
                }


              
            }
 
        }

        private bool IsValidEmail(string email)
        {
            // Padrão de expressão regular para validar o e-mail
            string emailPattern = @"^[\w\.-]+@([\w-]+\.)+[\w-]{2,4}$";

            // Verificar se o e-mail corresponde ao padrão
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
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

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow.Index != -1)
            {
                model.ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgClienteID"].Value);

                using(EFBANCO_CLIENTEEntities db = new EFBANCO_CLIENTEEntities())
                {
                    model = db.Clientes.Where(x => x.ID == model.ID).FirstOrDefault();
                    txtNome.Text = model.Nome;
                    txtCPF.Text = model.CPF;
                    txtTelefone.Text = model.Telefone;
                    txtEmail.Text = model.Email;
                    txtSexo.Text = model.Sexo;
                    txtEndereco.Text = model.Endereco;
                    txtDataNascimento.Text = model.DataNascimento.ToString();
                   
                }
                btnSalvar.Text = "Update";
                btnDeletar.Enabled = true;
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Você tem certeza que deseja excluir este registro?", "Mensagem", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (EFBANCO_CLIENTEEntities db = new EFBANCO_CLIENTEEntities())
                {
                    var entry = db.Entry(model);
                    if(entry.State == EntityState.Detached)
                    {
                        db.Clientes.Attach(model);
                        db.Clientes.Remove(model);
                        db.SaveChanges();
                        LoadData();
                        Clear();
                        MessageBox.Show("Deletado com Sucesso!");
                    }
                }
            }
        }
    }
}
