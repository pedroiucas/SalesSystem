using SistemaVenda.Dominio.DTO;
using SistemaVenda.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Repositorio
{
    public interface IRepositorioVendaProdutos 
    {
        IEnumerable<GraficoViewModel> ListaTotalVendasPorProduto(int CodigoUsuario);
        IEnumerable<GraficoViewModel> ListaTotalVendasPorCategoria(int CodigoUsuario);
        IEnumerable<GraficoViewModel> ListaTotalVendasPorCliente(int CodigoUsuario);
        IEnumerable<VendaProdutos> CarregarProdutos(int CodigoVenda);
    }
}