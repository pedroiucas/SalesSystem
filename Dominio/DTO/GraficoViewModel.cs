using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Dominio.DTO
{
    public class GraficoViewModel
    {
        public int CodigoProduto { get; set; }
        public int CodigoCategoria { get; set; }
        public int CodigoCliente { get; set; }
        public string Descricao { get; set; }
        public double TotalVendido { get; set; }
        public string ValorVendido { get; set; }
    }
}