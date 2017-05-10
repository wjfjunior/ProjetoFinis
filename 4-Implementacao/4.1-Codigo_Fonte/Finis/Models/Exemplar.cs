using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finis.Models
{
    public enum Conservacao
    {
        NOVO = 1,
        USADO = 2,
    }

    public class Exemplar : EntidadeAbstrata
    {
        //Normalizar elementos para adicionar

        [Display(Name = "Título")]
        [Required(ErrorMessage = "Por favor insira um título")]
        [StringLength(30, ErrorMessage = "O título é muito longo")]
        string titulo { get; set; }

        [Display(Name = "Conservação")]
        [Required(ErrorMessage = "Por favor selecione uma opção")]
        Conservacao conservacao { get; set; }

        [Display(Name = "ISBN")]
        int isbn;

        [Display(Name = "Ano")]
        [DisplayFormat(DataFormatString = "yyyy")]
        DateTime ano { get; set; }

        [Display(Name = "Edição")]
        int edicao;

        [Display(Name = "Preço")]
        [Required(ErrorMessage = "Por favor insira um preço")]
        decimal preco;

        [Display(Name = "Descrição")]
        [StringLength(200, ErrorMessage = "A descrição é muito longa")]
        string descricao { get; set; }

        [Display(Name = "Peso")]
        decimal peso;

        [Display(Name = "Disponibilizar para venda na internet?")]
        bool vendaOnline { get; set; }

        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "Por favor insira a quantidade disponível")]
        int quantidade;

        public String conservacaoString
        {
            get
            {
                if (this.conservacao == Conservacao.NOVO)
                    return "Novo";
                else if (this.conservacao == Conservacao.USADO)
                    return "Usado";

                else return "";
            }
        }
    }
}