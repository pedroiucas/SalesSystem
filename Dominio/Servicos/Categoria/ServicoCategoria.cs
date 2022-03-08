using Dominio.Interfaces;
using Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Servicos.Categoria
{
    public class ServicoCategoria : IServicoCategoria
    {
        private IRepositorioCategoria RepositorioCategoria;

        public ServicoCategoria(IRepositorioCategoria RepositorioCategoria)
        {
            this.RepositorioCategoria = RepositorioCategoria;
        }

        public void Cadastrar(SistemaVenda.Dominio.Entidades.Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public SistemaVenda.Dominio.Entidades.Categoria CarregarRegistro(int id)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SistemaVenda.Dominio.Entidades.Categoria> Listagem()
        {
            return RepositorioCategoria.Read();
        }
    }
}