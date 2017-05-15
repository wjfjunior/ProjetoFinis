﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Finis.Models
{
    public class Pessoa : EntidadeAbstrata
    {
        public Pessoa()
        {
            this.endereco = new Endereco();
        }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Por favor insira um nome")]
        [StringLength(50, ErrorMessage = "O nome é muito longo")]
        public string nome { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Por favor insira um e-mail válido")]
        [StringLength(50, ErrorMessage = "O e-mail é muito longo")]
        public string email { get; set; }

        [Display(Name = "Telefone")]
        public string telefone { get; set; }

        [Display(Name = "Celular")]
        public string celular { get; set; }

        [ForeignKey("Endereco")]
        [Display(Name = "Endereço")]
        public int enderecoId;

        public virtual Endereco endereco { get; set; }
    }
}