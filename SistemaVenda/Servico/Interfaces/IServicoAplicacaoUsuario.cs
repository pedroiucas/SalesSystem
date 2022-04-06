using Aplicacao.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Models;
using System.Collections.Generic;

namespace Aplicacao.Servico.Interfaces
{
    public interface IServicoAplicacaoUsuario
    {
        IEnumerable<UsuarioViewModel> Listagem();
        UsuarioViewModel CarregarRegistro(int codigo);
        UsuarioViewModel CarregarRegistro(string email, string senha);
        void Cadastrar(UsuarioViewModel Usuario);
        void Excluir(int codigo);
        bool ValidarLogin(string email, string senha);
    }
}