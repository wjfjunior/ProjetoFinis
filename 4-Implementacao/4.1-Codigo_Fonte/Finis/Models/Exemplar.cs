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
        NOVO = 1,
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
        string nome { get; set; }

        [ForeignKey("Pais")]
        [Display(Name = "País")]
        [Required(ErrorMessage = "Por favor selecione um país")]
        int paisId;

        public virtual Pais pais { get; set; }
    }

    public class Sessao : EntidadeAbstrata
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Por favor insira um nome")]
        [StringLength(20, ErrorMessage = "O nome é muito longo")]
        string nome { get; set; }

        [Display(Name = "Prateleira")]
        [Required(ErrorMessage = "Por favor insira o número da prateleira")]
        int prateleira;
    }

    public class Exemplar : EntidadeAbstrata
    {
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
        
        [InverseProperty("Exemplar")]
        [ScriptIgnore]
        public virtual ICollection<Autor> autor { get; set; }

        [ForeignKey("Editora")]
        [Display(Name = "Editora")]
        [Required(ErrorMessage = "Por favor selecione uma editora")]
        int editoraId;

        public virtual Editora editora { get; set; }

        [ForeignKey("Idioma")]
        [Display(Name = "Idioma")]
        [Required(ErrorMessage = "Por favor selecione um idioma")]
        int idiomaId;

        public virtual Idioma idioma { get; set; }

        [ForeignKey("Sessao")]
        [Display(Name = "Sessão")]
        [Required(ErrorMessage = "Por favor selecione uma sessão")]
        int sessaoId;

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
    }
}