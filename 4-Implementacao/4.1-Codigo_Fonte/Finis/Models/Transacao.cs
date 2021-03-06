﻿using Finis.Controllers;
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
        [Display(Name = "Entrada de créditos")]
        ENTRADA = 1,

        [Display(Name = "Saída de créditos")]
        SAIDA = 2,
    }

    public enum TipoCredito
    {
        [Display(Name = "Parcial")]
        PARCIAL = 1,

        [Display(Name = "Especial")]
        ESPECIAL = 2,
    }

    [NotMapped]
    public class TotalTransacao
    {
        public DateTime mesReferencia { get; set; }

        public int totalEntrada { get; set; }

        public int totalSaida { get; set; }
    }

    public class Transacao : EntidadeAbstrata
    {
        public Transacao()
        {
            this.data = DateTime.Now;
        }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "Por favor insira um valor")]
        public decimal valor { get; set; }

        [Display(Name = "Data da transação")]
        [Required(ErrorMessage = "Por favor insira uma data")]
        public DateTime data { get; set; }

        [Display(Name = "Tipo de transação")]
        [Required(ErrorMessage = "Por favor selecione uma opção")]
        public TipoTransacao tipoTransacao { get; set; }

        [Display(Name = "Tipo de crédito")]
        [Required(ErrorMessage = "Por favor selecione uma opção")]
        public TipoCredito tipoCredito { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Por favor selecione um cliente")]
        public int? clienteId { get; set; }
        [ForeignKey("clienteId")]
        public virtual Cliente cliente { get; set; }

        [NotMapped]
        public string dataString
        {
            get
            {
                return this.data.ToString("dd/MM/yyyy");
            }
        }

        [NotMapped]
        public string valorString
        {
            get
            {
                return this.valor.ToString("C");
            }
        }

        [NotMapped]
        public string tipoTransacaoString
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

        [NotMapped]
        public string tipoCreditoString
        {
            get
            {
                if (this.tipoCredito == TipoCredito.PARCIAL)
                    return "Parcial";
                else if (this.tipoCredito == TipoCredito.ESPECIAL)
                    return "Especial";

                else return "";
            }
        }

        public void NovaTransacaoEntrada(decimal? valor, TipoCredito credito, int? ClienteId)
        {
            this.data = DateTime.Now;
            this.valor = valor.GetValueOrDefault();
            this.tipoCredito = credito;
            this.clienteId = ClienteId;
            this.tipoTransacao = TipoTransacao.ENTRADA;
        }

        public void NovaTransacaoSaida(decimal? valor, TipoCredito credito, int? ClienteId)
        {
            this.data = DateTime.Now;
            this.valor = valor.GetValueOrDefault();
            this.tipoCredito = credito;
            this.clienteId = ClienteId;
            this.tipoTransacao = TipoTransacao.SAIDA;
        }
    }
}