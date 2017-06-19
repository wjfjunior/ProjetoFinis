using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Finis.Models
{
    public enum Conservacao
    {
        [Display(Name = "Novo")]
        NOVO = 1,

        [Display(Name = "Usado")]
        USADO = 2,
    }
    public enum VendaOnline
    {
        [Display(Name = "Sim")]
        SIM = 1,

        [Display(Name = "Não")]
        NAO = 2,
    }

    public class Autor : Pessoa { }

    public class Editora : Fornecedor
    {
        public Editora()
        {
            this.tipoFornecedor = TipoFornecedor.EDITORA;
        }
    }

    public class Idioma : EntidadeAbstrata
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Por favor insira um nome")]
        [StringLength(20, ErrorMessage = "O nome é muito longo")]
        public string nome { get; set; }
        
        [Display(Name = "País")]
        [Required(ErrorMessage = "Por favor selecione um país")]
        public int paisId { get; set; }
        [ForeignKey("paisId")]
        public virtual Pais pais { get; set; }
    }

    public class Sessao : EntidadeAbstrata
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Por favor insira um nome")]
        [StringLength(20, ErrorMessage = "O nome é muito longo")]
        public string nome { get; set; }
    }

    public class Exemplar : EntidadeAbstrata
    {
        public Exemplar()
        {
            
        }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "Por favor insira um título")]
        [StringLength(30, ErrorMessage = "O título é muito longo")]
        public string titulo { get; set; }

        [Display(Name = "Conservação")]
        [Required(ErrorMessage = "Por favor selecione uma opção")]
        public Conservacao conservacao { get; set; }

        [Display(Name = "ISBN")]
        [StringLength(32, ErrorMessage = "O valor é muito longo")]
        public string isbn { get; set; }

        [Display(Name = "Ano")]
        [Range(1000, 2017, ErrorMessage = "Ano inválido")]
        public int ano { get; set; }

        [Display(Name = "Edição")]
        [DisplayFormat(DataFormatString = "{0}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem edição")]
        [Range(0, 100, ErrorMessage = "Valor inválido")]
        public int edicao { get; set; }

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

        [Display(Name = "Descrição")]
        [StringLength(200, ErrorMessage = "A descrição é muito longa")]
        [DataType(DataType.MultilineText)]
        public string descricao { get; set; }

        [Display(Name = "Peso (Kg)")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem peso")]
        [Range(0, 100, ErrorMessage = "O peso deve ser entre 0 e 100")]
        public decimal peso { get; set; }

        [Display(Name = "Disponibilizar para venda na internet?")]
        public VendaOnline vendaOnline { get; set; }

        [Display(Name = "Quantidade")]
        [DisplayFormat(DataFormatString = "{0}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Estoque vazio")]
        [Required(ErrorMessage = "Por favor insira a quantidade disponível")]
        [Range(0, 5000, ErrorMessage = "A quantidade deverá ser entre 0 e 5000")]
        public int quantidade { get; set; }

        //[InverseProperty("Exemplar")]
        [ScriptIgnore]
        public virtual ICollection<Autor> Autores { get; set; }
        
        [Display(Name = "Editora")]
        [Required(ErrorMessage = "Por favor selecione uma editora")]
        public int? editoraId { get; set; }
        [ForeignKey("editoraId")]
        public virtual Editora editora { get; set; }
        
        [Display(Name = "Idioma")]
        [Required(ErrorMessage = "Por favor selecione um idioma")]
        public int? idiomaId { get; set; }
        [ForeignKey("idiomaId")]
        public virtual Idioma idioma { get; set; }
        
        [Display(Name = "Sessão")]
        [Required(ErrorMessage = "Por favor selecione uma sessão")]
        public int? sessaoId { get; set; }
        [ForeignKey("sessaoId")]
        public virtual Sessao sessao { get; set; }

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

        [NotMapped]
        public String vendaOnlineString
        {
            get
            {
                if (this.vendaOnline == VendaOnline.SIM)
                    return "Sim";
                else if (this.vendaOnline == VendaOnline.NAO)
                    return "Não";

                else return "";
            }
        }
    }
}