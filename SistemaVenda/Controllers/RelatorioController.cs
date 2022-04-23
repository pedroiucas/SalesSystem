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
            var lista = servicoVenda.ListaGrafico((int)HttpContext.Session.GetInt32(Sessao.CodigoUsuario)).ToList();

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