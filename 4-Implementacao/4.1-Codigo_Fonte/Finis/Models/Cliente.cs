using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Finis.Models
{
    public enum TipoGenero
    {
        FEMININO = 1,
        MASCULINO = 2,
    }

    public class Cliente : Pessoa
    {
        [Display(Name = "Saldo de crédito parcial")]
        [Required(ErrorMessage = "Por favor insira um valor")]
        [DataType(DataType.Currency, ErrorMessage = "Por favor insira um valor válido")]
        decimal saldoCreditoParcial { get; set; }

        [Display(Name = "Saldo de crédito especial")]
        [Required(ErrorMessage = "Por favor insira um valor")]
        [DataType(DataType.Currency, ErrorMessage = "Por favor insira um valor válido")]
        decimal saldoCreditoEspecial { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        [Required(ErrorMessage = "Por favor insira uma data")]
        [DataType(DataType.Date, ErrorMessage = "Por favor insira uma data válida")]
        DateTime dataNascimento { get; set; }

        [Display(Name = "Sexo")]
        [Required(ErrorMessage = "Por favor selecione uma opção")]
        TipoGenero genero { get; set; }

        [Display(Name = "Registro Geral (RG)")]
        [Required(ErrorMessage = "Por favor um número de documento")]
        [StringLength(50, ErrorMessage = "O número do documento é muito longo")]
        string rg { get; set; }

        [NotMapped]
        public String generoString
        {
            get
            {
                if (this.genero == TipoGenero.FEMININO)
                    return "Feminino";
                else if (this.genero == TipoGenero.MASCULINO)
                    return "Masculino";

                else return "";
            }
        }

        public void EmitirCartao(Cliente cliente)
        {

        }
    }
}