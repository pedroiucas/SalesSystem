using Aplicacao.Servico.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SistemaVenda.Helpers;

namespace SistemaVenda.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IServicoAplicacaoCliente ServicoAplicacaoCliente;

        public ClienteController(IServicoAplicacaoCliente ServicoAplicacaoCliente)
        {
            this.ServicoAplicacaoCliente = ServicoAplicacaoCliente;
        }

        public IActionResult Index()
        {
            return View(ServicoAplicacaoCliente.Listagem((int)HttpContext.Session.GetInt32(Sessao.CodigoUsuario)));
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            var viewModel = ServicoAplicacaoCliente.CarregarRegistro(id is null? 0: (int)id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(ClienteViewModel entidade)
        {
            if (ModelState.IsValid)
            {
                entidade.CodigoUsuario = HttpContext.Session.GetInt32(Sessao.CodigoUsuario);
                ServicoAplicacaoCliente.Cadastrar(entidade);
            }
            else
            {
                return View(entidade);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Deletar(int? id)
        {
            var viewModel = ServicoAplicacaoCliente.CarregarRegistro(id is null ? 0 : (int)id);
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Deletar(int id)
        {
            ServicoAplicacaoCliente.Excluir(id);
            return RedirectToAction("Index");
        }
    }
}