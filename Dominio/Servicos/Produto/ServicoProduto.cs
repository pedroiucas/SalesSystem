using Dominio.Interfaces;
using Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Servicos.Produto
{
    public class ServicoProduto : IServicoProduto
    {
        private IRepositorioProduto RepositorioProduto;

        public ServicoProduto(IRepositorioProduto RepositorioProduto)
        {
            this.RepositorioProduto = RepositorioProduto;
        }

        public void Cadastrar(SistemaVenda.Dominio.Entidades.Produto Produto)
        {
            RepositorioProduto.Create(Produto);
        }

        public IEnumerable<SistemaVenda.Dominio.Entidades.Produto> CarregarRegistroPorCategoria(int codigo)
        {
            return RepositorioProduto.Read().Where(e => e.CodigoCategoria == codigo);
        }

        public SistemaVenda.Dominio.Entidades.Produto CarregarRegistro(int id)
        {
            return RepositorioProduto.Read(id);
        }

        public void Excluir(int id)
        {
            RepositorioProduto.Delete(id);
        }

        public IEnumerable<SistemaVenda.Dominio.Entidades.Produto> Listagem()
        {
            return RepositorioProduto.Read();
        }
    }
}