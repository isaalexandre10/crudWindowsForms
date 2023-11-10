using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win02.Modelo;
using Win02.Banco;

namespace Win02 {
    public partial class CadastroFuncionario : Form {
        private TelaPrincipal telaPrincipal;
        private Funcionario func;
        public CadastroFuncionario(TelaPrincipal tela) {
            telaPrincipal = tela;
            InitializeComponent();
        }

        public CadastroFuncionario(TelaPrincipal tela, int id) {
            telaPrincipal = tela;
            InitializeComponent();

            func = FuncionarioDataAccess.PegarFuncionario(id);
            FuncionarioParaTela(func);
        }

        private void FuncionarioParaTela(Funcionario funcionario) {
            txtNome.Text = funcionario.Nome.Trim();
            txtEmail.Text = funcionario.Email.Trim();
            txtSalario.Text = funcionario.Salario.ToString();
            if (funcionario.Sexo == "M") { rbMasculino.Checked = true; } else { rbFeminino.Checked = false; };
            if ( funcionario.TipoContrato == "CLT" ) { rbCLT.Checked = true; } else if (funcionario.TipoContrato == "PJ") { rbPJ.Checked = true; } else { rbAutonomo.Checked = true; };
        }

        private void SalvarAction(object sender, EventArgs e) {
            Funcionario funcionario;

            if (func != null) {
                //Atualizacao
                funcionario = func;
                funcionario.DataAtualizacao = DateTime.Now;
            } else {
                //Cadastro novo
                funcionario = new Funcionario();
                funcionario.DataCadastro = DateTime.Now;
            }

            //Mover os dados para a classe Funcionário
            funcionario.Nome = txtNome.Text.Trim();
            funcionario.Email = txtEmail.Text.Trim();
            funcionario.Salario = decimal.Parse(txtSalario.Text);
            funcionario.Sexo = (rbMasculino.Checked) ? "M" : "F";
            funcionario.TipoContrato = (rbCLT.Checked) ? "CLT" : (rbPJ.Checked) ? "PJ" : "AUT";

            //Validar os dados
            List<ValidationResult> listErros = new List<ValidationResult>();
            ValidationContext contexto = new ValidationContext(funcionario);
            bool validado = Validator.TryValidateObject(funcionario, contexto, listErros, true);
            if (validado) {
                //Salvar os dados
                //Fechar e Atualizar a TelaPrincipal
                bool resultado;
                if(func != null) {
                    //Atualizar;
                    resultado = FuncionarioDataAccess.AtualizarFuncionario(funcionario);
                } else {
                    resultado = FuncionarioDataAccess.SalvarFuncionario(funcionario);
                }

                if(resultado) {
                    //Sucesso
                    this.telaPrincipal.AtualizarTabela();
                    this.Close();
                } else {
                    //Erro
                    lbrError.Text = "Erro na inserção no Banco de Dados!";
                }
            }
            else {
                //Validação Erro.
                StringBuilder sb = new StringBuilder();
                foreach (ValidationResult erro in listErros) { 
                    sb.Append(erro.ErrorMessage + "\n");
                }
                lbrError.Text = sb.ToString();
            }

            

        }
    }
}
