using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Finis.Models
{
    public class Pais
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Por favor insira um nome")]
        [StringLength(30, ErrorMessage = "O nome é muito longo")]
        string nome;

        [Display(Name = "Sigla")]
        [Required(ErrorMessage = "Por favor insira uma sigla")]
        [StringLength(2, ErrorMessage = "O tamanho máximo é de 2 caracteres")]
        string sigla;
    }

    public class Estado
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Por favor insira um nome")]
        [StringLength(30, ErrorMessage = "O nome é muito longo")]
        string nome;

        [Display(Name = "Sigla")]
        [Required(ErrorMessage = "Por favor insira uma sigla")]
        [StringLength(2, ErrorMessage = "O tamanho máximo é de 2 caracteres")]
        string sigla;

        [ForeignKey("Pais")]
        [Display(Name = "País")]
        [Required(ErrorMessage = "Por favor selecione um país")]
        int paisId;

        public virtual Pais pais { get; set; }
    }

    public class Cidade
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Por favor insira um nome")]
        [StringLength(30, ErrorMessage = "O nome é muito longo")]
        string nome;

        [ForeignKey("Estado")]
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Por favor selecione um estado")]
        int estadoId;

        public virtual Estado estado { get; set; }
    }

    public class Endereco
    {
        [Display(Name = "Logradouro")]
        [Required(ErrorMessage = "Por favor insira um logradouro")]
        [StringLength(30, ErrorMessage = "O nome é muito longo")]
        string logradouro;
        
        [Display(Name = "Número")]
        [Required(ErrorMessage = "Por favor insira um número")]
        int numero;

        [Display(Name = "Complemento")]
        [StringLength(30, ErrorMessage = "O nome é muito longo")]
        string complemento;

        [Display(Name = "Bairro")]
        [StringLength(20, ErrorMessage = "O nome é muito longo")]
        [Required(ErrorMessage = "Por favor insira um bairro")]
        string bairro;

        [Display(Name = "CEP")]
        int cep;

        [ForeignKey("Cidade")]
        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "Por favor selecione uma cidade")]
        int cidadeId;

        public virtual Cidade cidade { get; set; }
    }
}