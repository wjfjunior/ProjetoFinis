using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finis.Models
{
    public enum TipoFornecedor
    {
        [Display(Name = "Editora")]
        EDITORA = 1,

        [Display(Name = "Produto")]
        PRODUTO = 2,

        [Display(Name = "Serviço")]
        SERVICO,
    }

    public class Fornecedor : EntidadeAbstrata
    {
        [Display(Name = "Nome")]
        [StringLength(50, ErrorMessage = "O nome é muito longo")]
        public string nome { get; set; }

        [Display(Name = "CNPJ")]
        [StringLength(50, ErrorMessage = "O número do documento é muito longo")]
        //[Remote(action: "ValidaCPF", controller: "Fornecedor", ErrorMessage = "CPF Inválido")]
        public string cnpj { get; set; }
        
        [Display(Name = "Telefone")]
        [StringLength(15, ErrorMessage = "O número é muito longo")]
        public string telefone { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Por favor insira um e-mail válido")]
        [StringLength(50, ErrorMessage = "O e-mail é muito longo")]
        public string email { get; set; }

        [Display(Name = "Tipo de Fornecedor")]
        [Required(ErrorMessage = "Por favor selecione uma opção")]
        public TipoFornecedor tipoFornecedor { get; set; }
    }
}