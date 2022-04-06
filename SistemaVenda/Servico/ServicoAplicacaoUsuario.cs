using Aplicacao.Models;
using Aplicacao.Servico.Interfaces;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Dominio.Entidades;
using SistemaVenda.Models;
using System.Collections.Generic;

namespace Aplicacao.Servico
{
    public class ServicoAplicacaoUsuario : IServicoAplicacaoUsuario
    {
        private IServicoUsuario ServicoUsuario;

        public ServicoAplicacaoUsuario(IServicoUsuario ServicoUsuario)
        {
            this.ServicoUsuario = ServicoUsuario;
        }

        public void Cadastrar(UsuarioViewModel Usuario)
        {
            Usuario UsuarioEnti = new Usuario()
            {
                Nome = Usuario.Nome,
                Email = Usuario.Email,
                Senha = Usuario.Senha,
            };

            ServicoUsuario.Cadastrar(UsuarioEnti);
        }

        public void Excluir(int codigo)
        {
            ServicoUsuario.Excluir(codigo);
        }

        public UsuarioViewModel CarregarRegistro(int codigo)
        {
            if (codigo == 0)
            {
                return new UsuarioViewModel();
            }
            var registro = ServicoUsuario.CarregarRegistro(codigo);

            UsuarioViewModel Usuario = new UsuarioViewModel()
            {
                Nome = registro.Nome,
                Email = registro.Email,
                Senha = registro.Senha,
            };
           
            return Usuario;
        }

        public IEnumerable<UsuarioViewModel> Listagem()
        {
            var lista = ServicoUsuario.Listagem();
            List<UsuarioViewModel> listaUsuario = new List<UsuarioViewModel>();
            foreach (var item in lista)
            {
                UsuarioViewModel Usuario = new UsuarioViewModel()
                {
                    Nome = item.Nome,
                    Email = item.Email,
                    Senha = item.Senha,
                };

                listaUsuario.Add(Usuario);
            }

            return listaUsuario;
        }

        public bool ValidarLogin(string email, string senha)
        {
            return ServicoUsuario.ValidarLogin(email, senha);
        }

        public UsuarioViewModel CarregarRegistro(string email, string senha)
        {
            var registro = ServicoUsuario.CarregarRegistro(email,senha);

            UsuarioViewModel Usuario = new UsuarioViewModel()
            {
                Nome = registro.Nome,
                Email = registro.Email,
                Senha = registro.Senha,
                Codigo = registro.Codigo
            };

            return Usuario;
        }
    }
}