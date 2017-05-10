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
        AGUARDANDO_AVALIACAO = 1,
        AVALIADO = 2,
        AGUARDANDO_CLIENTE = 3,
        CONCLUIDO = 4,
    }

    public class Avaliacao : EntidadeAbstrata
    {
        [Display(Name = "Data de entrada")]
        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        [Required(ErrorMessage = "Por favor insira uma data")]
        [DataType(DataType.Date, ErrorMessage = "Por favor insira uma data válida")]
        public DateTime dataEntrada;

        [Display(Name = "Quantidade de exemplares")]
        [Required(ErrorMessage = "Por favor insira um número")]
        public int quantidadeExemplares;

        [Display(Name = "Crédito especial")]
        [DataType(DataType.Currency, ErrorMessage = "Por favor insira um valor válido")]
        public decimal creditoEspecial;

        [Display(Name = "Crédito parcial")]
        [DataType(DataType.Currency, ErrorMessage = "Por favor insira um valor válido")]
        public decimal creditoParcial;

        [Display(Name = "Situação")]
        [Required(ErrorMessage = "Por favor selecione uma opção")]
        public situacaoAvaliacao situacao;

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

        public void Avaliar(Avaliacao avaliacao)
        {

        }
    }
}