using SistemaVenda.Models;
using System.Collections.Generic;

namespace Aplicacao.Servico.Interfaces
{
    public interface IServicoAplicacaoCategoria
    {
        IEnumerable<CategoriaViewModel> Listagem();
        CategoriaViewModel CarregarRegistro(int codigo);
        void Cadastrar(CategoriaViewModel categoria);
        void Excluir(int codigo);
    }
}