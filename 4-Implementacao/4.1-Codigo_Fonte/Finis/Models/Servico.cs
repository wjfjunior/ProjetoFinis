using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finis.Models
{
    public class Servico : EntidadeAbstrata
    {
        public Servico()
        {

        }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Por favor insira um nome")]
        [StringLength(50, ErrorMessage = "O nome é muito longo")]
        public string nome { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(200, ErrorMessage = "A descrição é muito longa")]
        [DataType(DataType.MultilineText)]
        public string descricao { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "Por favor insira um valor")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem valor")]
        [Range(0, 100000, ErrorMessage = "O valor deverá ser entre 0 e 100000")]
        public decimal valor { get; set; }

        [Display(Name = "Quantidade")]
        [DisplayFormat(DataFormatString = "{0}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Estoque vazio")]
        [Range(0, 5000, ErrorMessage = "A quantidade deverá ser entre 0 e 5000")]
        public int quantidade { get; set; }
    }
}