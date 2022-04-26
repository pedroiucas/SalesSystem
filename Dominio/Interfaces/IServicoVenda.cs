using SistemaVenda.Dominio.DTO;
using SistemaVenda.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Interfaces
{
    public interface IServicoVenda : IServicoCRUD<Venda>
    {
        IEnumerable<GraficoViewModel> ListaTotalVendasPorProduto(int CodigoUsuario);
        IEnumerable<GraficoViewModel> ListaTotalVendasPorCategoria(int CodigoUsuario);
        IEnumerable<GraficoViewModel> ListaTotalVendasPorCliente(int CodigoUsuario);
        IEnumerable<GraficoViewModel> ListaTotalValorPorProduto(int CodigoUsuario);
        IEnumerable<GraficoViewModel> ListaTotalValorPorCategoria(int CodigoUsuario);
        IEnumerable<GraficoViewModel> ListaTotalValorPorCliente(int CodigoUsuario);
    }
}