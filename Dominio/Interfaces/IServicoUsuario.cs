using SistemaVenda.Dominio.DTO;
using SistemaVenda.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Interfaces
{
    public interface IServicoUsuario : IServicoCRUD<Usuario>
    {
        bool ValidarLogin(string email, string senha);
        UsuarioViewModel CarregarRegistro(string email, string senha);
    }
}