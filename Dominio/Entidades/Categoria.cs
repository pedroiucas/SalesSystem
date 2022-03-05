using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Dominio.Entidades
{
    public class Categoria
    {
        [Key]
        public int? Codigo { get; set; }

        public string Descricao { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}