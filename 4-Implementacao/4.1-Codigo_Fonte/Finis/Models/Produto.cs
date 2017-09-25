using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Finis.Models
{
    public class UnidadeMedida : EntidadeAbstrata
    {
        [Display(Name = "Grandeza")]
        [Required(ErrorMessage = "Por favor insira um nome")]
        [StringLength(50, ErrorMessage = "O nome é muito longo")]
        public string grandeza { get; set; }

        [Display(Name = "Unidade")]
        [Required(ErrorMessage = "Por favor insira uma unidade de medida")]
        [StringLength(20, ErrorMessage = "O nome é muito longo")]
        public string unidade { get; set; }

        [Display(Name = "Símbolo")]
        [Required(ErrorMessage = "Por favor insira um símbolo")]
        [StringLength(3, ErrorMessage = "O simbolo é muito longo")]
        public string simbolo { get; set; }
    }

    public class Marca : EntidadeAbstrata
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Por favor insira um nome")]
        [StringLength(50, ErrorMessage = "O nome é muito longo")]
        public string nome { get; set; }
    }

    public class Produto : EntidadeAbstrata
    {
        public Produto()
        {
           
        }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Por favor insira um nome")]
        [StringLength(50, ErrorMessage = "O nome é muito longo")]
        public string nome { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Por favor insira uma descrição")]
        [StringLength(200, ErrorMessage = "A descrição é muito longa")]
        public string descricao { get; set; }

        [Display(Name = "Preço de Compra")]
        [Required(ErrorMessage = "Por favor insira um valor")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem preço")]
        [Range(0, 500, ErrorMessage = "O preço deverá ser entre 0 e 500")]
        public decimal precoCompra { get; set; }

        [Display(Name = "Preço de Venda")]
        [Required(ErrorMessage = "Por favor insira um valor")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem preço")]
        [Range(0, 500, ErrorMessage = "O preço deverá ser entre 0 e 500")]
        public decimal precoVenda { get; set; }

        [Display(Name = "Quantidade")]
        [DisplayFormat(DataFormatString = "{0}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Estoque vazio")]
        [Required(ErrorMessage = "Por favor insira a quantidade disponível")]
        [Range(0, 5000, ErrorMessage = "A quantidade deverá ser entre 0 e 5000")]
        public int quantidade { get; set; }

        [Display(Name = "Unidade de Medida")]
        [Required(ErrorMessage = "Por favor selecione uma unidade de medida")]
        public int? unidadeMedidaId { get; set; }
        [ForeignKey("unidadeMedidaId")]
        public virtual UnidadeMedida unidadeMedida { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage = "Por favor selecione uma marca")]
        public int? marcaId { get; set; }
        [ForeignKey("marcaId")]
        public virtual Marca marca { get; set; }

        [Display(Name = "Estoque mínimo")]
        [DisplayFormat(DataFormatString = "{0}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Zero")]
        [Required(ErrorMessage = "Por favor insira o estoque mínimo")]
        [Range(0, 5000, ErrorMessage = "O estoque mínimo deverá ser entre 0 e 5000")]
        public int estoqueMinimo { get; set; }

        [NotMapped]
        [Display(Name = "Unidade de Medida")]
        [Required(ErrorMessage = "Por favor selecione uma unidade de medida")]
        public string unidadeMedidaNome { get; set; }

        [NotMapped]
        [Display(Name = "Marca")]
        [Required(ErrorMessage = "Por favor selecione uma marca")]
        public string marcaNome { get; set; }
    }
}