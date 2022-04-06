using Dominio.Interfaces;
using Dominio.Repositorio;
using SistemaVenda.Dominio.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Servicos.Usuario
{
    public class ServicoUsuario : IServicoUsuario
    {
        private IRepositorioUsuario RepositorioUsuario;

        public ServicoUsuario(IRepositorioUsuario RepositorioUsuario)
        {
            this.RepositorioUsuario = RepositorioUsuario;
        }

        public void Cadastrar(SistemaVenda.Dominio.Entidades.Usuario Usuario)
        {
            RepositorioUsuario.Create(Usuario);
        }

        public SistemaVenda.Dominio.Entidades.Usuario CarregarRegistro(int id)
        {
            return RepositorioUsuario.Read(id);
        }

        public UsuarioViewModel CarregarRegistro(string email, string senha)
        {
            return RepositorioUsuario.CarregarRegistro(email, senha);
        }

        public void Excluir(int id)
        {
            RepositorioUsuario.Delete(id);
        }

        public IEnumerable<SistemaVenda.Dominio.Entidades.Usuario> Listagem()
        {
            return RepositorioUsuario.Read();
        }

        public bool ValidarLogin(string email, string senha)
        {
            return RepositorioUsuario.ValidarLogin(email, senha);
        }
    }
}