using Dominio.Repositorio;
using Microsoft.EntityFrameworkCore;
using Repositorio.Contexto;
using Repositorio.Interfaces;
using SistemaVenda.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositorio.Entidades
{
    public class RepositorioVenda : Repositorio<Venda>, IRepositorioVenda
    {
        public RepositorioVenda(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override void Delete(int id)
        {
            var listaProdutos = DbSetContext.Include(x => x.Produtos).Where(y => y.Codigo == id).AsNoTracking().ToList();

            foreach (var item in listaProdutos[0].Produtos)
            {
                VendaProdutos vendaProdutos = new VendaProdutos()
                {
                    CodigoVenda = id,
                    CodigoProduto = (int)item.CodigoProduto
                };

                DbSet<VendaProdutos> DbSetAux = Db.Set<VendaProdutos>();

                DbSetAux.Attach(vendaProdutos);
                DbSetAux.Remove(vendaProdutos);
            }
            Db.SaveChanges();

            base.Delete(id);
        }
    }
}