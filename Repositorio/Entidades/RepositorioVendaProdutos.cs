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

        public IEnumerable<GraficoViewModel> ListaGrafico(int CodigoUsuario)
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
    }
}
