using Dominio.Repositorio;
using Microsoft.EntityFrameworkCore;
using Repositorio.Contexto;
using SistemaVenda.Dominio.DTO;
using SistemaVenda.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositorio.Entidades
{
    public class RepositorioVendaProdutos : IRepositorioVendaProdutos
    {
        protected ApplicationDbContext DbSetContext;
        public RepositorioVendaProdutos(ApplicationDbContext mContext)
        {
            DbSetContext = mContext;
        }

        public IEnumerable<VendaProdutos> CarregarProdutos(int CodigoVenda)
        {
            return DbSetContext.VendaProdutos.OrderBy(y => y.CodigoProduto).Where(e => e.Venda.Codigo == CodigoVenda);
        }

        public IEnumerable<GraficoViewModel> ListaTotalValorPorProduto(int CodigoUsuario)
        {
            var lista = DbSetContext.VendaProdutos.OrderBy(y => y.CodigoProduto).Where(e => e.Produto.CodigoUsuario == CodigoUsuario)
               .Select(y => new GraficoViewModel
               {
                   CodigoProduto = y.CodigoProduto,
                   Descricao = y.Produto.Descricao + " R$",
                   ValorVendido = DbSetContext.VendaProdutos.Where(e => e.CodigoProduto == y.CodigoProduto).Sum(z => z.ValorTotal).ToString().Replace(',', '.')
               }).Distinct().ToList();

            return lista;
        }

        public IEnumerable<GraficoViewModel> ListaTotalValorPorCategoria(int CodigoUsuario)
        {
            var lista = DbSetContext.VendaProdutos.OrderBy(y => y.CodigoProduto).Where(e => e.Produto.CodigoUsuario == CodigoUsuario).Distinct()
               .Select(y => new GraficoViewModel
               {
                   CodigoCategoria = y.Produto.CodigoCategoria,
                   Descricao = y.Produto.Categoria.Descricao + " R$",
                   ValorVendido = DbSetContext.VendaProdutos.Where(e => e.Produto.CodigoCategoria == y.Produto.CodigoCategoria).Sum(z => z.ValorTotal).ToString().Replace(',', '.')
               }).Distinct().ToList();

            return lista;
        }

        public IEnumerable<GraficoViewModel> ListaTotalValorPorCliente(int CodigoUsuario)
        {
            var lista = DbSetContext.VendaProdutos.OrderBy(y => y.CodigoProduto).Where(e => e.Produto.CodigoUsuario == CodigoUsuario).Distinct()
               .Select(y => new GraficoViewModel
               {
                   CodigoCliente = y.Venda.CodigoCliente,
                   Descricao = y.Venda.Cliente.Nome + " R$",
                   ValorVendido = DbSetContext.VendaProdutos.Where(e => e.Venda.CodigoCliente == y.Venda.CodigoCliente).Sum(z => z.ValorTotal).ToString().Replace(',', '.')
               }).Distinct().ToList();

            return lista;
        }

        public IEnumerable<GraficoViewModel> ListaTotalVendasPorProduto(int CodigoUsuario)
        {
            var lista = DbSetContext.VendaProdutos.OrderBy(y => y.CodigoProduto).Where(e => e.Produto.CodigoUsuario == CodigoUsuario)
               .Select(y => new GraficoViewModel
               {
                   CodigoProduto = y.CodigoProduto,
                   Descricao = y.Produto.Descricao,
                   TotalVendido = DbSetContext.VendaProdutos.Where(e => e.CodigoProduto == y.CodigoProduto).Sum(z => z.Quantidade)
               }).Distinct().ToList();

            return lista;
        }
        public IEnumerable<GraficoViewModel> ListaTotalVendasPorCategoria(int CodigoUsuario)
        {
            var lista = DbSetContext.VendaProdutos.OrderBy(y => y.CodigoProduto).Where(e => e.Produto.CodigoUsuario == CodigoUsuario)
               .Select(y => new GraficoViewModel
               {
                   CodigoCategoria = y.Produto.CodigoCategoria,
                   Descricao = y.Produto.Categoria.Descricao,
                   TotalVendido = DbSetContext.VendaProdutos.Where(e => e.Produto.CodigoCategoria == y.Produto.CodigoCategoria).Sum(z => z.Quantidade)
               }).Distinct().ToList();

            return lista;
        }
        public IEnumerable<GraficoViewModel> ListaTotalVendasPorCliente(int CodigoUsuario)
        {
            var lista = DbSetContext.VendaProdutos.OrderBy(y => y.CodigoProduto).Where(e => e.Produto.CodigoUsuario == CodigoUsuario)
               .Select(y => new GraficoViewModel
               {
                   CodigoCliente = y.Venda.CodigoCliente,
                   Descricao = y.Venda.Cliente.Nome,
                   TotalVendido = DbSetContext.VendaProdutos.Where(e => e.Venda.CodigoCliente == y.Venda.CodigoCliente).Sum(z => z.Quantidade)
               }).Distinct().ToList();

            return lista;
        }
    }
}
