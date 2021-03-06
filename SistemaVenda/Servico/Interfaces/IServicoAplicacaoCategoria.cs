using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Models;
using System.Collections.Generic;

namespace Aplicacao.Servico.Interfaces
{
    public interface IServicoAplicacaoCategoria
    {
        IEnumerable<CategoriaViewModel> Listagem(int CodigoUsuario);
        IEnumerable<SelectListItem> ListagemSelectList(int CodigoUsuario);
        CategoriaViewModel CarregarRegistro(int codigo);
        void Cadastrar(CategoriaViewModel categoria);
        void Excluir(int codigo);
    }
}