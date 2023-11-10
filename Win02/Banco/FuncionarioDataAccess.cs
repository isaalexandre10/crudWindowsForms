using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Win02.Modelo;
using Win02.Banco;

namespace Win02.Banco {
    public class FuncionarioDataAccess {
        private static SqlConnection conexao = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;Initial Catalog=banco_dados_curso;Integrated Security=SSPI;Trusted_Connection=yes;");
        public static DataTable PegarFuncionarios() {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Funcionario", conexao);

            conexao.Open(); // abre a conexão com o banco

            SqlDataAdapter da = new SqlDataAdapter(); /* da, adapta o banco de dados ao nosso projeto */

            DataSet ds = new DataSet();
            
            da.SelectCommand = cmd; // adapta cmd ao projeto
            da.Fill(ds); // preenche todas as informações dentro do DataSet
            
            return ds.Tables[0];
        }

        public static bool AtualizarFuncionario(Funcionario funcionario) {

            string sql = "UPDATE [Funcionario] SET Nome = @Nome, Email = @Email, Salario = @Salario, Sexo = @Sexo, TipoContrato = @TipoContrato, DataAtualizacao = @DataAtualizacao WHERE Id = @Id";

            SqlCommand comando = new SqlCommand(sql, conexao);

            comando.Parameters.Add("@Id", funcionario.Id);
            comando.Parameters.Add("@Nome", funcionario.Nome);
            comando.Parameters.Add("@Email", funcionario.Email);
            comando.Parameters.Add("@Salario", funcionario.Salario);
            comando.Parameters.Add("@Sexo", funcionario.Sexo);
            comando.Parameters.Add("@TipoContrato", funcionario.TipoContrato);
            comando.Parameters.Add("@DataAtualizacao", funcionario.DataAtualizacao);

            conexao.Close();

            conexao.Open();

            if (comando.ExecuteNonQuery() > 0) {
                conexao.Close();
                return true;
            }
            else {
                conexao.Close();
                return false;
            }
        }

        public static bool SalvarFuncionario(Funcionario funcionario) {

            string sql = "INSERT INTO [Funcionario](Nome, Email, Salario, Sexo, TipoContrato, DataCadastro) VALUES(@Nome, @Email, @Salario, @Sexo, @TipoContrato, @DataCadastro)";

            SqlCommand comando = new SqlCommand(sql, conexao);

            comando.Parameters.Add("@Nome", funcionario.Nome);
            comando.Parameters.Add("@Email", funcionario.Email);
            comando.Parameters.Add("@Salario", funcionario.Salario);
            comando.Parameters.Add("@Sexo", funcionario.Sexo);
            comando.Parameters.Add("@TipoContrato", funcionario.TipoContrato);
            comando.Parameters.Add("@DataCadastro", funcionario.DataCadastro);
            
            conexao.Close();

            conexao.Open();

            if (comando.ExecuteNonQuery() > 0) {
                conexao.Close();
                return true;
            } else {
                conexao.Close();
                return false;
            }
        }

        public static Funcionario PegarFuncionario(int id) {
            string sql = "SELECT * FROM [Funcionario] Where Id = @id";

            SqlCommand comando = new SqlCommand( sql, conexao);
            comando.Parameters.Add("@id", id);

            conexao.Close();

            conexao.Open();

            SqlDataReader resposta = comando.ExecuteReader();

            Funcionario funcionario = new Funcionario();

            while (resposta.Read()) {
                funcionario.Id = resposta.GetInt32(0);
                funcionario.Nome = resposta.GetString(1);
                funcionario.Email = resposta.GetString(2);
                funcionario.Salario = resposta.GetDecimal(3);
                funcionario.Sexo = resposta.GetString(4);
                funcionario.TipoContrato = resposta.GetString(5);
                funcionario.DataCadastro = resposta.GetDateTime(6);
                if(resposta.IsDBNull(7)) { funcionario.DataAtualizacao = DateTime.Now; } else { funcionario.DataAtualizacao = resposta.GetDateTime(7); }
            }

            conexao.Close() ;

            return funcionario;
        }

        public static bool ExcluirFuncionario(int id) {
            string sql = "DELETE FROM [Funcionario] Where Id = @id";

            SqlCommand comando = new SqlCommand(sql, conexao);
            comando.Parameters.Add("@id", id);

            conexao.Close();

            conexao.Open();

            if (comando.ExecuteNonQuery() > 0) {
                conexao.Close(); 
                return true;
            } else { 
                conexao.Close();
                return false;
            }
        }
    }
}
