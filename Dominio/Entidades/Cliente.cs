using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Dominio.Entidades
{
    public class Cliente : EntityBase
    {
        public string Nome { get; set; }
        public string CNPJ_CPF { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public ICollection<Venda> Vendas { get; set; }
        public Usuario Usuario { get; set; }
        [ForeignKey("Usuario")]
        public int? CodigoUsuario { get; set; }
    }
}