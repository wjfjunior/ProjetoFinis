using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Finis.Models
{
    public class Pessoa
    {
        [Key]
        [Editable(false)]
        public int? id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Por favor insira um nome")]
        [StringLength(50, ErrorMessage = "O nome é muito longo")]
        string nome { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Por favor insira um endereço de e-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Por favor insira um e-mail válido")]
        [StringLength(50, ErrorMessage = "O e-mail é muito longo")]
        string email { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Por favor insira um número de telefone")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Por favor insira um número de telefone válido")]
        int telefone { get; set; }

        [Display(Name = "Celular")]
        [Required(ErrorMessage = "Por favor insira um número de celular")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Por favor insira um número de celular válido")]
        int celular { get; set; }

        [ForeignKey("Endereco")]
        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "Por favor selecione um endereço")]
        int cidadeId;

        public virtual Endereco endereco { get; set; }
    }
}