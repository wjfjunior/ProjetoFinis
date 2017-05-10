using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finis.Models
{
    public class Fornecedor : Pessoa
    {
        [Display(Name = "Cadastro Nacional de Pessoas Jurídicas (CNPJ)")]
        [StringLength(50, ErrorMessage = "O número do documento é muito longo")]
        string cnpj { get; set; }

        public bool ValidaCnpj()
        {
            return true;
        }
    }
}