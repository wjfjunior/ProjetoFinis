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

    public class Produto : Item
    {
        public Produto()
        {
           
        }
        
        [Display(Name = "Unidade de Medida")]
        [Required(ErrorMessage = "Por favor selecione uma unidade de medida")]
        public int unidadeMedidaId { get; set; }
        [ForeignKey("unidadeMedidaId")]
        public virtual UnidadeMedida unidadeMedida { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage = "Por favor selecione uma marca")]
        public int marcaId { get; set; }
        [ForeignKey("marcaId")]
        public virtual Marca marca { get; set; }

        [NotMapped]
        [Display(Name = "Marca")]
        [Required(ErrorMessage = "Por favor selecione uma marca")]
        public string marcaNome { get; set; }
    }
}