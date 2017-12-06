using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Finis.Models
{
    public class Item : EntidadeAbstrata
    {
        public Item()
        {
            this.vendas = new HashSet<Venda>();
        }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Por favor insira um nome")]
        [StringLength(50, ErrorMessage = "O nome é muito longo")]
        public string nome { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(200, ErrorMessage = "A descrição é muito longa")]
        [DataType(DataType.MultilineText)]
        public string descricao { get; set; }

        [Display(Name = "Preço de Compra")]
        [Required(ErrorMessage = "Por favor insira um valor")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem preço")]
        [Range(0, 500, ErrorMessage = "O preço deverá ser entre 0 e 5000")]
        public decimal precoCompra { get; set; }

        [Display(Name = "Preço de Venda")]
        [Required(ErrorMessage = "Por favor insira um valor")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem preço")]
        [Range(0, 500, ErrorMessage = "O preço deverá ser entre 0 e 5000")]
        public decimal precoVenda { get; set; }

        [Display(Name = "Preço Total")]
        [Required(ErrorMessage = "Por favor insira um valor")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem preço")]
        [Range(0, 500, ErrorMessage = "O preço deverá ser entre 0 e 5000")]
        public decimal precoTotal { get; set; }

        [Display(Name = "Quantidade")]
        [DisplayFormat(DataFormatString = "{0}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Estoque vazio")]
        [Required(ErrorMessage = "Por favor insira a quantidade disponível")]
        [Range(0, 5000, ErrorMessage = "A quantidade deverá ser entre 0 e 5000")]
        public int quantidade { get; set; }

        [Display(Name = "Estoque mínimo")]
        [DisplayFormat(DataFormatString = "{0}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Zero")]
        [Range(0, 5000, ErrorMessage = "O estoque mínimo deverá ser entre 0 e 5000")]
        public int estoqueMinimo { get; set; }

        public virtual ICollection<Venda> vendas { get; set; }

    }
}