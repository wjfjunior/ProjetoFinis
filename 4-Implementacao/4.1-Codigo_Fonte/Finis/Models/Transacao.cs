using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Finis.Models
{
    public enum TipoTransacao
    {
        ENTRADA = 1,
        SAIDA = 2,
    }

    public class Transacao : EntidadeAbstrata
    {
        [Display(Name = "Valor")]
        [Required(ErrorMessage = "Por favor insira um valor")]
        [DataType(DataType.Currency, ErrorMessage = "Por favor insira um valor válido")]
        public decimal valor { get; set; }

        [Display(Name = "Data da transação")]
        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        [Required(ErrorMessage = "Por favor insira uma data")]
        [DataType(DataType.Date, ErrorMessage = "Por favor insira uma data válida")]
        public DateTime data { get; set; }

        [Display(Name = "Tipo de transação")]
        [Required(ErrorMessage = "Por favor selecione uma opção")]
        public TipoTransacao tipoTransacao { get; set; }

        [ForeignKey("Cliente")]
        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Por favor selecione um cliente")]
        public int clienteId;

        public virtual Cliente cliente { get; set; }

        [NotMapped]
        public String tipoTransacaoString
        {
            get
            {
                if (this.tipoTransacao == TipoTransacao.ENTRADA)
                    return "Entrada";
                else if (this.tipoTransacao == TipoTransacao.SAIDA)
                    return "Saída";

                else return "";
            }
        }
    }
}