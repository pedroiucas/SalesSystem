using Dominio.Repositorio;
using Microsoft.EntityFrameworkCore;
using Repositorio.Contexto;
using Repositorio.Interfaces;
using SistemaVenda.Dominio.DTO;
using SistemaVenda.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositorio.Entidades
{
    public class RepositorioUsuario : Repositorio<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public UsuarioViewModel CarregarRegistro(string email, string senha)
        {
            var usuario = DbSetContext.Where(x => x.Email == email && x.Senha == senha).FirstOrDefault();

            UsuarioViewModel usuarioVM = new UsuarioViewModel()
            {
                Senha = usuario.Senha,
                Email = usuario.Email,
                Nome = usuario.Nome,
                Codigo = usuario.Codigo,
            };

            return usuarioVM;
        }

        public bool ValidarLogin(string email, string senha)
        {
            var usuario = DbSetContext.Where(x => x.Email == email && x.Senha == senha).FirstOrDefault();

            return usuario == null ? false : true;
        }
    }
}