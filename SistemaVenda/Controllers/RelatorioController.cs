using Microsoft.AspNetCore.Mvc;
using SistemaVenda.DAL;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Controllers
{
    public class RelatorioController : Controller
    {
        protected ApplicationDbContext mContext;

        public RelatorioController(ApplicationDbContext context)
        {
            mContext = context;
        }

        public IActionResult Grafico()
        {
            var lista = mContext.VendaProdutos.OrderBy(y => y.CodigoProduto)
            .Select(y => new GraficoViewModel
            {
                CodigoProduto = y.CodigoProduto,
                Descricao = y.Produto.Descricao,
                TotalVendido = mContext.VendaProdutos.Where(e => e.CodigoProduto == y.CodigoProduto).Sum(z => z.Quantidade)
            }).Distinct().ToList();

            string Valores = string.Empty;
            string Labels = string.Empty;
            string Cores = string.Empty;

            var random = new Random();

            for (int i = 0; i < lista.Count(); i++)
            {
                Valores += lista[i].TotalVendido.ToString() + ",";
                Labels += "'" + lista[i].Descricao.ToString() + "',";
                Cores += "'" + string.Format("#{0:X6}", random.Next(0x1000000)) + "',";
            }

            ViewBag.Valores = Valores;
            ViewBag.Labels = Labels;
            ViewBag.Cores = Cores;
            return View();
        }
    }
}