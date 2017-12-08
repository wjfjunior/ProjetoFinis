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
        
        [Display(Name = "Finalizada")]
        FINALIZADA = 4
    }

    public enum statusAvaliacao
    {
        [Display(Name = "Aberta")]
        ABERTA = 1,

        [Display(Name = "Cancelada")]
        CANCELADA = 2,

        [Display(Name = "Concluída")]
        CONCLUIDA = 3,
    }

    public class Avaliacao : EntidadeAbstrata
    {
        public Avaliacao()
        {
            this.status = statusAvaliacao.ABERTA;
        }

        [Display(Name = "Data de entrada")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Por favor insira uma data")]
        public DateTime dataEntrada { get; set; }

        [Display(Name = "Quantidade de exemplares")]
        [Required(ErrorMessage = "Por favor insira um número")]
        [DisplayFormat(DataFormatString = "{0}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Nenhum")]
        [Range(1, 5000, ErrorMessage = "A quantidade deverá ser entre 1 e 5000")]
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

        [Display(Name = "Status")]
        public statusAvaliacao status { get; set; }

        [Display(Name = "Observação")]
        [StringLength(200, ErrorMessage = "A observacao é muito longa")]
        [DataType(DataType.MultilineText)]
        public string observacao { get; set; }

        [NotMapped]
        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Por favor selecione um cliente")]
        public string clienteNome { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Por favor selecione um cliente")]
        public int? clienteId { get; set; }
        [ForeignKey("clienteId")]
        public virtual Cliente cliente { get; set; }

        [NotMapped]
        public string creditoEspecialString => this.creditoEspecial == null ? "Sem valor" : string.Format("{0:C}", this.creditoEspecial.Value);

        [NotMapped]
        public string creditoParcialString => this.creditoParcial == null ? "Sem valor" : string.Format("{0:C}", this.creditoParcial.Value);

        [NotMapped]
        public string dataEntradaString
        {
            get
            {
                return this.dataEntrada.ToString("dd/MM/yyyy");
            }
            set
            {
                this.dataEntrada = Convert.ToDateTime(value).Date;
            }
        }

        [NotMapped]
        public String statusString
        {
            get
            {
                if (this.status == statusAvaliacao.ABERTA)
                    return "Aberta";
                else if (this.status == statusAvaliacao.CANCELADA)
                    return "Cancelada";
                else if (this.status == statusAvaliacao.CONCLUIDA)
                    return "Concluída";

                else return "";
            }
        }

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
                else if (situacao == situacaoAvaliacao.FINALIZADA)
                    return "Finalizada";

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
                ClientesController clientesController = new ClientesController();
                clientesController.AtualizaSaldoEspecial(this.clienteId, this.creditoEspecial);
            }
            if(this.creditoParcial != 0)
            {
                ClientesController clientesController = new ClientesController();
                clientesController.AtualizaSaldoParcial(this.clienteId, this.creditoParcial);
            }
            this.status = statusAvaliacao.CONCLUIDA;
        }

        public void CancelarAvaliacao()
        {
            this.status = statusAvaliacao.CANCELADA;
        }
    }
}