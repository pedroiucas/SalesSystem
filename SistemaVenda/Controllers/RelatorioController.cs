using Aplicacao.Servico.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SistemaVenda.Helpers;

namespace SistemaVenda.Controllers
{
    public class RelatorioController : Controller
    {
        readonly IServicoAplicacaoVenda servicoVenda;
        public RelatorioController(IServicoAplicacaoVenda servicoVenda)
        {
            this.servicoVenda = servicoVenda;
        }

        public IActionResult Grafico()
        {
            var lista = servicoVenda.ListaTotalVendasPorProduto((int)HttpContext.Session.GetInt32(Sessao.CodigoUsuario)).ToList();
            var lista2 = servicoVenda.ListaTotalVendasPorCategoria((int)HttpContext.Session.GetInt32(Sessao.CodigoUsuario)).ToList();
            var lista3 = servicoVenda.ListaTotalVendasPorCliente((int)HttpContext.Session.GetInt32(Sessao.CodigoUsuario)).ToList();

            DefineListaTotalVendasPorProduto(lista);
            DefineListaTotalVendasPorCategoria(lista2);
            DefineListaTotalVendasPorCliente(lista3);

            return View();
        }

        public void DefineListaTotalVendasPorProduto(List<GraficoViewModel> lista)
        {
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

            ViewBag.ValoresProduto = Valores;
            ViewBag.LabelsProduto = Labels;
            ViewBag.CoresProduto = Cores;
        }

        public void DefineListaTotalVendasPorCategoria(List<GraficoViewModel> lista)
        {
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

            ViewBag.ValoresCategoria = Valores;
            ViewBag.LabelsCategoria = Labels;
            ViewBag.CoresCategoria = Cores;
        }

        public void DefineListaTotalVendasPorCliente(List<GraficoViewModel> lista)
        {
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

            ViewBag.ValoresCliente = Valores;
            ViewBag.LabelsCliente = Labels;
            ViewBag.CoresCliente = Cores;
        }
    }
}