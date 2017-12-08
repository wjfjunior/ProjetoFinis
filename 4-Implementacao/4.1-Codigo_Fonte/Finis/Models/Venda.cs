using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Finis.Models
{
    public enum FormaPagamento
    {
        [Display(Name = "Dinheiro")]
        DINHEIRO = 1,

        [Display(Name = "Cartão de crédito")]
        CARTAO_CREDITO = 2,

        [Display(Name = "Cartão de débito")]
        CARTAO_DEBITO = 3
    }

    public class Venda : EntidadeAbstrata
    {
        public Venda()
        {
            this.itensVenda = new List<ItemVenda>();
            this.formaPagamento = FormaPagamento.DINHEIRO;
            this.dataCompra = DateTime.Now;
            this.creditoEspecial = 0;
            this.creditoParcial = 0;
            this.desconto = 0;
            this.descontoPorcentagem = 0;
            this.subtotal = 0;
            this.total = 0;
            this.recebido = 0;
            this.troco = 0;
        }

        [Display(Name = "Data da Compra")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [Required(ErrorMessage = "Por favor insira uma data")]
        public DateTime dataCompra { get; set; }

        [Display(Name = "Crédito especial")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem valor")]
        public decimal creditoEspecial { get; set; }

        [Display(Name = "Crédito parcial")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem valor")]
        public decimal creditoParcial { get; set; }

        [Display(Name = "Desconto")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem valor")]
        public decimal desconto { get; set; }

        [Display(Name = "Desconto (%)")]
        [Range(0, 100, ErrorMessage = "O valor deve ser entre 0 e 100")]
        public int descontoPorcentagem { get; set; }

        [Display(Name = "Subtotal")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem valor")]
        public decimal subtotal { get; set; }

        [Display(Name = "Valor final")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem valor")]
        public decimal total { get; set; }

        [Display(Name = "Recebido")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem valor")]
        public decimal recebido { get; set; }

        [Display(Name = "Troco")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem valor")]
        public decimal troco { get; set; }

        [Display(Name = "Forma de Pagamento")]
        [Required(ErrorMessage = "Por favor selecione uma opção")]
        public FormaPagamento formaPagamento { get; set; }

        [ForeignKey("vendaId")]
        public IList<ItemVenda> itensVenda { get; set; }

        //[NotMapped]
        //[Display(Name = "Cliente")]
        //[Required(ErrorMessage = "Por favor selecione um cliente")]
        //public string clienteNome { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Por favor selecione um cliente")]
        public int? clienteId { get; set; }
        [ForeignKey("clienteId")]
        public virtual Cliente cliente { get; set; }

        [NotMapped]
        public string creditoParcialString
        {
            get
            {
                return this.creditoParcial.ToString("C");
            }
        }
        
        [NotMapped]
        public string creditoEspecialString
        {
            get
            {
                return this.creditoEspecial.ToString("C");
            }
        }

        [NotMapped]
        public string dataCompraString
        {
            get
            {
                return this.dataCompra.ToString("0:dd/MM/yyyy HH:mm");
            }
            set
            {
                this.dataCompra = Convert.ToDateTime(value).Date;
            }
        }

        [NotMapped]
        public String formaPagamentoString
        {
            get
            {
                if (this.formaPagamento == FormaPagamento.DINHEIRO)
                    return "Dinheiro";
                else if (this.formaPagamento == FormaPagamento.CARTAO_CREDITO)
                    return "Cartão de crédito";
                else if (this.formaPagamento == FormaPagamento.CARTAO_DEBITO)
                    return "Cartão de débito";

                else return "";
            }
        }

    }
}