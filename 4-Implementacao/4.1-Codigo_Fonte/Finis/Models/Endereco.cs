using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Finis.Models
{
    public class Pais : EntidadeAbstrata
    {
        public Pais()
        {

        }

        [Display(Name = "Nome do país")]
        [Required(ErrorMessage = "Por favor insira um nome")]
        [StringLength(30, ErrorMessage = "O nome é muito longo")]
        public string nome { get; set; }

        [Display(Name = "Sigla")]
        [Required(ErrorMessage = "Por favor insira uma sigla")]
        [StringLength(2, ErrorMessage = "O tamanho máximo é de 2 caracteres")]
        public string sigla { get; set; }
    }

    public class Estado : EntidadeAbstrata
    {
        public Estado()
        {
            this.pais = new Pais();
        }

        [Display(Name = "Nome do estado")]
        [Required(ErrorMessage = "Por favor insira um nome")]
        [StringLength(30, ErrorMessage = "O nome é muito longo")]
        public string nome { get; set; }

        [Display(Name = "Sigla")]
        [Required(ErrorMessage = "Por favor insira uma sigla")]
        [StringLength(2, ErrorMessage = "O tamanho máximo é de 2 caracteres")]
        public string sigla { get; set; }

        
        [Display(Name = "País")]
        [Required(ErrorMessage = "Por favor selecione um país")]
        public int paisId { get; set; }
        [ForeignKey("paisId")]
        public virtual Pais pais { get; set; }
    }

    public class Cidade : EntidadeAbstrata
    {
        public Cidade()
        {
            this.estado = new Estado();
        }

        [Display(Name = "Nome da cidade")]
        [Required(ErrorMessage = "Por favor insira um nome")]
        [StringLength(30, ErrorMessage = "O nome é muito longo")]
        public string nome { get; set; }
        
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Por favor selecione um estado")]
        public int estadoId { get; set; }
        [ForeignKey("estadoId")]
        public virtual Estado estado { get; set; }
    }

    public class Endereco : EntidadeAbstrata
    {
        public Endereco()
        {
            this.cidade = new Cidade();
        }

        [Display(Name = "Logradouro")]
        [Required(ErrorMessage = "Por favor insira um logradouro")]
        [StringLength(30, ErrorMessage = "O nome é muito longo")]
        public string logradouro { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "Por favor insira um número")]
        public int numero { get; set; }

        [Display(Name = "Complemento")]
        [StringLength(30, ErrorMessage = "O nome é muito longo")]
        public string complemento { get; set; }

        [Display(Name = "Bairro")]
        [StringLength(20, ErrorMessage = "O nome é muito longo")]
        [Required(ErrorMessage = "Por favor insira um bairro")]
        public string bairro { get; set; }

        [Display(Name = "CEP")]
        public int cep { get; set; }
        
        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "Por favor selecione uma cidade")]
        public int cidadeId { get; set; }
        [ForeignKey("cidadeId")]
        public virtual Cidade cidade { get; set; }
    }
}