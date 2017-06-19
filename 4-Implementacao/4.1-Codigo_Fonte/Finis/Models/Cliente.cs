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
        [Display(Name = "Feminino")]
        FEMININO = 1,

        [Display(Name = "Masculino")]
        MASCULINO = 2,
    }

    public class Cliente : Pessoa
    {
        public Cliente()
        {
            
        }

        [Display(Name = "Saldo de crédito parcial")]
        [Required(ErrorMessage = "Por favor insira um valor")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem preço")]
        [Range(0, 500, ErrorMessage = "O preço deverá ser entre 0 e 5000")]
        public decimal saldoCreditoParcial { get; set; }

        [Display(Name = "Saldo de crédito especial")]
        [Required(ErrorMessage = "Por favor insira um valor")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem preço")]
        [Range(0, 500, ErrorMessage = "O preço deverá ser entre 0 e 5000")]
        public decimal saldoCreditoEspecial { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Por favor insira uma data")]
        public DateTime dataNascimento { get; set; }

        [Display(Name = "Gênero")]
        [Required(ErrorMessage = "Por favor selecione uma opção")]
        public TipoGenero genero { get; set; }

        [Display(Name = "Registro Geral (RG)")]
        [Required(ErrorMessage = "Por favor um número de documento")]
        [StringLength(20, ErrorMessage = "O número do documento é muito longo")]
        public string rg { get; set; }

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

        public void NovoSaldo(decimal creditoEspecial, decimal creditoParcial)
        {
            this.saldoCreditoEspecial = creditoEspecial;
            this.saldoCreditoEspecial = creditoEspecial;
        }

        public void AtualizaSaldoEspecial(decimal creditoEspecial)
        {
            this.saldoCreditoEspecial += creditoEspecial;
        }

        public void AtualizaSaldoParcial(decimal creditoParcial)
        {
            this.saldoCreditoParcial += creditoParcial;
        }
    }
}