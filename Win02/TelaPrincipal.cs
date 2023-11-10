using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win02.Modelo;
using Win02.Banco;

namespace Win02 {
    public partial class TelaPrincipal : Form {
        public TelaPrincipal() {
            InitializeComponent();

            AtualizarTabela();
        }

        public void AtualizarTabela() {
            dgvTabelaFuncionario.DataSource = Banco.FuncionarioDataAccess.PegarFuncionarios();
        }

        private void NovoAction(object sender, EventArgs e) {
            new CadastroFuncionario(this).Show();
        }

        private void EditarAction(object sender, EventArgs e) {
            int id = (int)dgvTabelaFuncionario.SelectedRows[0].Cells[0].Value;
            new CadastroFuncionario(this, id).Show();
        }

        private void ExcluirAction(object sender, EventArgs e) {
            int id = (int)dgvTabelaFuncionario.SelectedRows[0].Cells[0].Value;
            FuncionarioDataAccess.ExcluirFuncionario(id);
            AtualizarTabela();
        }
    }
}
