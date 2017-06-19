using Finis.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Finis.Models
{
    public enum situacaoAvaliacao
    {
        [Display(Name = "Aguardando avaliação")]
        AGUARDANDO_AVALIACAO = 1,

        [Display(Name = "Avaliado")]
        AVALIADO = 2,

        [Display(Name = "Aguardando retorno do cliente")]
        AGUARDANDO_CLIENTE = 3,

        [Display(Name = "Concluído")]
        CONCLUIDO = 4,
    }

    public class Avaliacao : EntidadeAbstrata
    {
        [Display(Name = "Data de entrada")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Por favor insira uma data")]
        public DateTime dataEntrada { get; set; }

        [Display(Name = "Quantidade de exemplares")]
        [Required(ErrorMessage = "Por favor insira um número")]
        [DisplayFormat(DataFormatString = "{0}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Nenhum")]
        [Range(0, 5000, ErrorMessage = "A quantidade deverá ser entre 0 e 5000")]
        public int quantidadeExemplares { get; set; }

        [Display(Name = "Crédito especial")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem valor")]
        public Nullable<decimal> creditoEspecial { get; set; }

        [Display(Name = "Crédito parcial")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem valor")]
        public Nullable<decimal> creditoParcial { get; set; }

        [Display(Name = "Situação")]
        [Required(ErrorMessage = "Por favor selecione uma opção")]
        public situacaoAvaliacao situacao { get; set; }

        [Display(Name = "Observação")]
        [StringLength(200, ErrorMessage = "A observacao é muito longa")]
        [DataType(DataType.MultilineText)]
        public string observacao { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Por favor selecione um cliente")]
        public int? clienteId { get; set; }
        [ForeignKey("clienteId")]
        public virtual Cliente cliente { get; set; }

        [NotMapped]
        public String situacaoString
        {
            get
            {
                if (situacao == situacaoAvaliacao.AGUARDANDO_AVALIACAO)
                    return "Aguardando avaliação";
                else if (situacao == situacaoAvaliacao.AVALIADO)
                    return "Avaliado";
                else if (situacao == situacaoAvaliacao.AGUARDANDO_CLIENTE)
                    return "Aguardando retorno do cliente";
                else if (situacao == situacaoAvaliacao.CONCLUIDO)
                    return "Concluído";

                else return "";
            }
        }

        public void Avaliar()
        {
            this.situacao = situacaoAvaliacao.AVALIADO;
        }

        public void ConcluirAvaliacao()
        {
            if(this.creditoEspecial != 0)
            {
                TransacoesController transacaoCreditoEspecial = new TransacoesController();
                transacaoCreditoEspecial.GeraTransacaoEntrada(this.creditoEspecial, TipoCredito.ESPECIAL, this.clienteId);

                ClientesController clientesController = new ClientesController();
                clientesController.AtualizaSaldoEspecial(this.clienteId, this.creditoEspecial);
            }
            if(this.creditoParcial != 0)
            {
                TransacoesController transacaoCreditoParcial = new TransacoesController();
                transacaoCreditoParcial.GeraTransacaoEntrada(this.creditoParcial, TipoCredito.PARCIAL, this.clienteId);

                ClientesController clientesController = new ClientesController();
                clientesController.AtualizaSaldoParcial(this.clienteId, this.creditoParcial);
            }
            this.situacao = situacaoAvaliacao.CONCLUIDO;
        }
    }
}