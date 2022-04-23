using SistemaVenda.Models;

namespace Aplicacao.Models
{
    public class VendaProdutoViewModel
    {
        public int CodigoVenda { get; set; }
        public int CodigoProduto { get; set; }
        public double Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public ProdutoViewModel Produto { get; set; }
        public VendaViewModel Venda { get; set; }
    }
}
