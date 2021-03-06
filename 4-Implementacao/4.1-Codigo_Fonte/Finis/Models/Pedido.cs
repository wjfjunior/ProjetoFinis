﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Finis.Models
{
    public enum situacaoPedido
    {
        [Display(Name = "Pendente")]
        PENDENTE = 1,

        [Display(Name = "Realizado")]
        REALIZADO = 2,

        [Display(Name = "Aguardando cliente")]
        AGUARDANDO_CLIENTE = 3,

        [Display(Name = "Concluído")]
        CONCLUIDO = 4,
    }

    public class Pedido : EntidadeAbstrata
    {
        private DAL.Contexto db = new DAL.Contexto();

        public Pedido()
        {
            this.dataPedido = DateTime.Now;
            this.Exemplares = db.Item.OfType<Exemplar>().OrderBy(e => e.nome).ToList();
            this.situacao = situacaoPedido.PENDENTE;
        }

        [NotMapped]
        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Por favor selecione um cliente")]
        public string clienteNome { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Por favor selecione um cliente")]
        public int? clienteId { get; set; }
        [ForeignKey("clienteId")]
        public virtual Cliente cliente { get; set; }

        [ScriptIgnore]
        public virtual IList<Exemplar> Exemplares { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(200, ErrorMessage = "A descrição é muito longa")]
        [DataType(DataType.MultilineText)]
        public string descricao { get; set; }

        [Display(Name = "Data de pedido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dataPedido { get; set; }

        [Display(Name = "Situação")]
        [Required(ErrorMessage = "Por favor selecione uma opção")]
        public situacaoPedido situacao { get; set; }
    }
}