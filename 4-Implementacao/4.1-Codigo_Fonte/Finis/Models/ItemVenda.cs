using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Finis.Models
{
    public class ItemVenda : EntidadeAbstrata
    {
        public ItemVenda()
        {
            this.quantidade = 1;
        }

        public int indice { get; set; }

        [Display(Name = "Quantidade")]
        [DisplayFormat(DataFormatString = "{0}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Estoque vazio")]
        [Range(0, 5000, ErrorMessage = "A quantidade deverá ser entre 0 e 5000")]
        public int quantidade { get; set; }

        [Display(Name = "Preço Total")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem preço")]
        public decimal precoTotal { get; set; }
        
        [Display(Name = "Venda")]
        public int vendaId { get; set; }
        [ForeignKey("vendaId")]
        public virtual Venda venda { get; set; }

        [Display(Name = "Item")]
        public int itemId { get; set; }
        [ForeignKey("itemId")]
        public virtual Item item { get; set; }
    }
}