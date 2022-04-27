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
        IEnumerable<GraficoViewModel> ListaTotalValorPorProduto(int CodigoUsuario);
        IEnumerable<GraficoViewModel> ListaTotalValorPorCategoria(int CodigoUsuario);
        IEnumerable<GraficoViewModel> ListaTotalValorPorCliente(int CodigoUsuario);
        IEnumerable<VendaProdutos> CarregarProdutos(int CodigoVenda);
        IEnumerable<VendaProdutos> CarregarVendaPorProduto(int codigo);
        void Create(VendaProdutos Entidade);
    }
}