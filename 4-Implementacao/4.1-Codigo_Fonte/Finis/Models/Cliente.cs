using Finis.Controllers;
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
        public decimal saldoCreditoParcial { get; set; }

        [Display(Name = "Saldo de crédito especial")]
        [Required(ErrorMessage = "Por favor insira um valor")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem preço")]
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
        public string saldoCreditoParcialString
        {
            get
            {
                return this.saldoCreditoParcial.ToString("C");
            }
        }

        [NotMapped]
        public string saldoCreditoEspecialString
        {
            get
            {
                return this.saldoCreditoEspecial.ToString("C");
            }
        }

        [NotMapped]
        public string dataNascimentoString
        {
            get
            {
                return this.dataNascimento.ToString("dd/MM/yyyy");
            }
            set
            {
                this.dataNascimento = Convert.ToDateTime(value).Date;
            }
        }

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

        public void NovoSaldoEspecial(decimal credito)
        {
            TransacoesController transacaoCreditoEspecial = new TransacoesController();
            transacaoCreditoEspecial.GeraTransacaoEntrada(credito, TipoCredito.ESPECIAL, this.id);
            this.saldoCreditoEspecial = credito;
        }

        public void NovoSaldoParcial(decimal credito)
        {
            TransacoesController transacaoCreditoParcial = new TransacoesController();
            transacaoCreditoParcial.GeraTransacaoEntrada(credito, TipoCredito.PARCIAL, this.id);
            this.saldoCreditoParcial = credito;
        }

        public void AtualizaSaldoEspecial(decimal creditoEspecial)
        {
            TransacoesController transacaoCreditoEspecial = new TransacoesController();
            transacaoCreditoEspecial.GeraTransacaoEntrada(creditoEspecial, TipoCredito.ESPECIAL, this.id);
            this.saldoCreditoEspecial += creditoEspecial;
        }

        public void AtualizaSaldoParcial(decimal creditoParcial)
        {
            TransacoesController transacaoCreditoParcial = new TransacoesController();
            transacaoCreditoParcial.GeraTransacaoEntrada(creditoParcial, TipoCredito.PARCIAL, this.id);
            this.saldoCreditoParcial += creditoParcial;
        }
    }
}