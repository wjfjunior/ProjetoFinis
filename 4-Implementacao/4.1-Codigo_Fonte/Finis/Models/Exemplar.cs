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

    public class Exemplar
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
    }
}