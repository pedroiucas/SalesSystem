using SistemaVenda.Models;
using System.Collections.Generic;

namespace Aplicacao.Servico.Interfaces
{
    public interface IServicoAplicacaoVenda
    {
        IEnumerable<VendaViewModel> Listagem();
        VendaViewModel CarregarRegistro(int codigo);
        void Cadastrar(VendaViewModel Venda);
        void Excluir(int codigo);

        IEnumerable<GraficoViewModel> ListaGrafico();
    }
}