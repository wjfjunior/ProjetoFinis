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

    public class Autor : Pessoa
    {
        public Nullable<int> exemplar_id { get; set; }

        [ForeignKey("exemplar_id")]
        public virtual Exemplar Exemplar { get; set; }
    }

    public class Editora : Fornecedor { }

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

        
        [Display(Name = "Subsessao")]
        public Nullable<int> subsessaoId { get; set; }
        [ForeignKey("subsessaoId")]
        public virtual Sessao sessao { get; set; }
    }

    public class Exemplar : EntidadeAbstrata
    {
        public Exemplar()
        {
            this.vendaOnline = true;
        }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "Por favor insira um título")]
        [StringLength(30, ErrorMessage = "O título é muito longo")]
        public string titulo { get; set; }

        [Display(Name = "Conservação")]
        [Required(ErrorMessage = "Por favor selecione uma opção")]
        public Conservacao conservacao { get; set; }

        [Display(Name = "ISBN")]
        public int isbn { get; set; }

        [Display(Name = "Ano")]
        [DisplayFormat(DataFormatString = "yyyy")]
        public DateTime ano { get; set; }

        [Display(Name = "Edição")]
        public int edicao { get; set; }

        [Display(Name = "Preço de Compra")]
        [Required(ErrorMessage = "Por favor insira um valor")]
        public decimal precoCompra { get; set; }

        [Display(Name = "Preço de Venda")]
        [Required(ErrorMessage = "Por favor insira um valor")]
        public decimal precoVenda { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(200, ErrorMessage = "A descrição é muito longa")]
        [DataType(DataType.MultilineText)]
        public string descricao { get; set; }

        [Display(Name = "Peso")]
        public decimal peso { get; set; }

        [Display(Name = "Disponibilizar para venda na internet")]
        public bool vendaOnline { get; set; }

        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "Por favor insira a quantidade disponível")]
        public int quantidade { get; set; }

        //[InverseProperty("Exemplar")]
        [InverseProperty("Exemplar")]
        [ScriptIgnore]
        public virtual ICollection<Autor> Autores { get; set; }
        
        [Display(Name = "Editora")]
        [Required(ErrorMessage = "Por favor selecione uma editora")]
        public int editoraId { get; set; }
        [ForeignKey("editoraId")]
        public virtual Editora editora { get; set; }
        
        [Display(Name = "Idioma")]
        [Required(ErrorMessage = "Por favor selecione um idioma")]
        public int idiomaId { get; set; }
        [ForeignKey("idiomaId")]
        public virtual Idioma idioma { get; set; }
        
        [Display(Name = "Sessão")]
        [Required(ErrorMessage = "Por favor selecione uma sessão")]
        public int sessaoId { get; set; }
        [ForeignKey("sessaoId")]
        public virtual Sessao sessao { get; set; }

        [NotMapped]
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