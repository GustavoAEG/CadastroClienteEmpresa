//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CadastroClienteEmpresa
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cliente
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public string Endereco { get; set; }
        public Nullable<System.DateTime> DataNascimento { get; set; }
    }
}