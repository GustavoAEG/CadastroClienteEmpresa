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

        void ClearOnlyCPF()
        {
            txtCPF.Text = "";
        }

        void ClearOnlyEmail()
        {
            txtEmail.Text = "";
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

            using (EFBANCO_CLIENTEEntities db = new EFBANCO_CLIENTEEntities())
            {
                string email = txtEmail.Text;
                string cpf = txtCPF.Text;

                cpf = new string(cpf.Where(char.IsDigit).ToArray());

                if (IsValidEmail(email) && IsValidCPF(cpf))
                {
                    if (model.ID == 0)
                        db.Clientes.Add(model);
                    else
                        db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();

                    Clear();
                    LoadData();
                    MessageBox.Show("Salvo com sucesso!");
                }
                else
                {
                    if (IsValidCPF(cpf) && !IsValidEmail(email))
                    {
                        MessageBox.Show("Email invalido. Informe um E-mail válido");
                        ClearOnlyEmail();
                        this.ActiveControl = txtEmail;
                    }
                    else
                    {
                        MessageBox.Show("CPF inválido! Tente novamente.");
                        ClearOnlyCPF();
                        this.ActiveControl = txtCPF;
                    }       
                    //MessageBox.Show("O e-mail inserido é inválido. Por favor, insira um e-mail válido.");
                    //Clear();
                    //this.ActiveControl = txtNome;
                    //LoadData();
                }

            }

        }

        private bool IsValidCPF(string cpf)
        {
            // Verificar se o CPF possui 11 dígitos
            if (cpf.Length != 11)
                return false;

            // Verificar se todos os dígitos são iguais
            if (cpf.Distinct().Count() == 1)
                return false;

            // Verificar o primeiro dígito verificador
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += int.Parse(cpf[i].ToString()) * (10 - i);

            int remainder = sum % 11;
            int digit1 = (remainder < 2) ? 0 : 11 - remainder;

            if (int.Parse(cpf[9].ToString()) != digit1)
                return false;

            // Verificar o segundo dígito verificador
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(cpf[i].ToString()) * (11 - i);

            remainder = sum % 11;
            int digit2 = (remainder < 2) ? 0 : 11 - remainder;

            if (int.Parse(cpf[10].ToString()) != digit2)
                return false;

            return true;
        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[\w\.-]+@([\w-]+\.)+[\w-]{2,4}$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
        }

        void LoadData()
        {
            dataGridView1.AutoGenerateColumns = false;
            using (EFBANCO_CLIENTEEntities db = new EFBANCO_CLIENTEEntities())
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
            if (dataGridView1.CurrentRow.Index != -1)
            {
                model.ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgClienteID"].Value);

                using (EFBANCO_CLIENTEEntities db = new EFBANCO_CLIENTEEntities())
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
            if (MessageBox.Show("Você tem certeza que deseja excluir este registro?", "Mensagem", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (EFBANCO_CLIENTEEntities db = new EFBANCO_CLIENTEEntities())
                {
                    var entry = db.Entry(model);
                    if (entry.State == EntityState.Detached)
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
        private void SetPlaceholderText(TextBox textBox, string placeholderText)
        {
            // Define o texto sombra no campo
            textBox.Text = placeholderText;
            textBox.ForeColor = SystemColors.GrayText;

            // Manipula o evento Enter para limpar o campo quando o usuário entrar nele
            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholderText)
                {
                    textBox.Text = "";
                    textBox.ForeColor = SystemColors.ControlText;
                }
            };

            // Manipula o evento Leave para restaurar o texto sombra quando o usuário sair do campo
            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholderText;
                    textBox.ForeColor = SystemColors.GrayText;
                }
            };
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
