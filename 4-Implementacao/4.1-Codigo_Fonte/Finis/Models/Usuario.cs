using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Finis.Models
{
    public enum Perfil
    { 
        [Display(Name = "Administrador")]
        ADMINISTRADOR = 1,

        [Display(Name = "Funcionário")]
        FUNCIONARIO = 2,

        [Display(Name = "Cliente")]
        CLIENTE = 3,
    }

    public class Usuario : EntidadeAbstrata
    {
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Por favor insira um e-mail válido")]
        [StringLength(50, ErrorMessage = "O e-mail é muito longo")]
        public string email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Senha não informada!")]
        [StringLength(8, ErrorMessage = "Senha senha precisa conter entre 3 e 8 caracteres", MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string senha { get; set; }

        [Display(Name = "Confirme a senha")]
        [Required(ErrorMessage = "Confirme sua senha")]
        [StringLength(8, ErrorMessage = "Senha senha precisa conter entre 3 e 8 caracteres", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Compare("senha")]
        public string confirmaSenha { get; set; }

        [Display(Name = "Ativo")]
        public bool ativo { get; set; }

        [Display(Name = "Perfil")]
        [Required(ErrorMessage = "Selecione um perfil")]
        public Perfil pefil { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Insira um nome")]
        public string nome { get; set; }

        [Display(Name = "Sobrenome")]
        [Required(ErrorMessage = "Insira um sobrenome")]
        public string sobrenome { get; set; }

        [NotMapped]
        public String perfilString
        {
            get
            {
                if (this.pefil == Perfil.ADMINISTRADOR)
                    return "Administrador";
                if (this.pefil == Perfil.CLIENTE)
                    return "Cliente";
                if (this.pefil == Perfil.FUNCIONARIO)
                    return "Funcionário";

                else return "";
            }
        }

        [NotMapped]
        public String ativoString
        {
            get
            {
                if (this.ativo)
                    return "Sim";
                else 
                    return "Não";
            }
        }
    }
}