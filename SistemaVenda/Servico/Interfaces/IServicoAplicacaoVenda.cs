using SistemaVenda.Models;
using System.Collections.Generic;

namespace Aplicacao.Servico.Interfaces
{
    public interface IServicoAplicacaoVenda
    {
        IEnumerable<VendaViewModel> Listagem(int CodigoUsuario);
        VendaViewModel CarregarRegistro(int codigo);
        void Cadastrar(VendaViewModel Venda);
        void Excluir(int codigo);

        IEnumerable<GraficoViewModel> ListaTotalVendasPorProduto(int CodigoUsuario);
        IEnumerable<GraficoViewModel> ListaTotalVendasPorCategoria(int CodigoUsuario);
        IEnumerable<GraficoViewModel> ListaTotalVendasPorCliente(int CodigoUsuario);

        IEnumerable<GraficoViewModel> ListaTotalValorPorProduto(int CodigoUsuario);
        IEnumerable<GraficoViewModel> ListaTotalValorPorCategoria(int CodigoUsuario);
        IEnumerable<GraficoViewModel> ListaTotalValorPorCliente(int CodigoUsuario);
    }
}