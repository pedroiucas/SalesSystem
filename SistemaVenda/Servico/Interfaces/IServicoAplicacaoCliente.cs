using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Models;
using System.Collections.Generic;

namespace Aplicacao.Servico.Interfaces
{
    public interface IServicoAplicacaoCliente
    {
        IEnumerable<ClienteViewModel> Listagem();
        IEnumerable<SelectListItem> ListagemSelectList();
        ClienteViewModel CarregarRegistro(int codigo);
        void Cadastrar(ClienteViewModel cliente);
        void Excluir(int codigo);
    }
}