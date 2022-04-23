using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Dominio.Entidades
{
    public class Venda : EntityBase
    {
        public DateTime Data { get; set; }

        [ForeignKey("Cliente")]
        public int CodigoCliente { get; set; }

        public Cliente Cliente { get; set; }
        public decimal Total { get; set; }
        public ICollection<VendaProdutos> Produtos { get; set; }
        [ForeignKey("Usuario")]
        public int? CodigoUsuario { get; set; }
    }
}