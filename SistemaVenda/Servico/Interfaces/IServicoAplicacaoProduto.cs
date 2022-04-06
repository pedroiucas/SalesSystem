using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Models;
using System.Collections.Generic;

namespace Aplicacao.Servico.Interfaces
{
    public interface IServicoAplicacaoProduto
    {
        IEnumerable<ProdutoViewModel> Listagem();
        ProdutoViewModel CarregarRegistro(int codigo);
        void Cadastrar(ProdutoViewModel produto);
        void Excluir(int codigo);
        IEnumerable<SelectListItem> ListagemSelectList();
    }
}