using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Win02.Modelo {
    public class Funcionario {
        public int Id { get; set; }

        [Required, StringLength(70, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required, EmailAddress, StringLength(70, MinimumLength = 5)]
        public string Email { get; set;}

        [Required]
        public decimal Salario { get; set;}

        [Required]
        public string Sexo { get; set;}

        [Required]
        public string TipoContrato { get; set;} 

        public DateTime DataCadastro { get; set;}
        public DateTime DataAtualizacao { get; set;}

    }
}
