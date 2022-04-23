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
    public class VendaController : Controller
    {
        private readonly IServicoAplicacaoVenda ServicoAplicacaoVenda;
        private readonly IServicoAplicacaoProduto ServicoAplicacaoProduto;
        private readonly IServicoAplicacaoCliente ServicoAplicacaoCliente;

        public VendaController(IServicoAplicacaoVenda ServicoAplicacaoVenda, IServicoAplicacaoProduto ServicoAplicacaoProduto, IServicoAplicacaoCliente ServicoAplicacaoCliente)
        {
            this.ServicoAplicacaoVenda = ServicoAplicacaoVenda;
            this.ServicoAplicacaoProduto = ServicoAplicacaoProduto;
            this.ServicoAplicacaoCliente = ServicoAplicacaoCliente;
        }

        public IActionResult Index()
        {
            return View(ServicoAplicacaoVenda.Listagem((int)HttpContext.Session.GetInt32(Sessao.CodigoUsuario)));
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            var viewModel = ServicoAplicacaoVenda.CarregarRegistro(id is null ? 0 : (int)id);

            viewModel.ListaClientes = ServicoAplicacaoCliente.ListagemSelectList((int)HttpContext.Session.GetInt32(Sessao.CodigoUsuario));
            viewModel.ListaProdutos = ServicoAplicacaoProduto.ListagemSelectList((int)HttpContext.Session.GetInt32(Sessao.CodigoUsuario));

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(VendaViewModel entidade)
        {
            if (ModelState.IsValid)
            {
                entidade.CodigoUsuario = HttpContext.Session.GetInt32(Sessao.CodigoUsuario);
                ServicoAplicacaoVenda.Cadastrar(entidade);
            }
            else
            {
                entidade.ListaClientes = ServicoAplicacaoCliente.ListagemSelectList((int)HttpContext.Session.GetInt32(Sessao.CodigoUsuario));
                entidade.ListaProdutos = ServicoAplicacaoProduto.ListagemSelectList((int)HttpContext.Session.GetInt32(Sessao.CodigoUsuario));
                return View(entidade);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Deletar(int id)
        {
            ServicoAplicacaoVenda.Excluir(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Deletar(int? id)
        {
            var viewModel = ServicoAplicacaoVenda.CarregarRegistro(id is null ? 0 : (int)id);

            viewModel.ListaClientes = ServicoAplicacaoCliente.ListagemSelectList((int)HttpContext.Session.GetInt32(Sessao.CodigoUsuario));
            viewModel.ListaProdutos = ServicoAplicacaoProduto.ListagemSelectList((int)HttpContext.Session.GetInt32(Sessao.CodigoUsuario));

            return View(viewModel);
        }

        [HttpGet("LerValorProduto/{CodigoProduto}")]
        public decimal? LerValorProduto(int CodigoProduto)
        {
            if (CodigoProduto != 0)
            {
                return (decimal)ServicoAplicacaoProduto.CarregarRegistro(CodigoProduto).Valor;
            }
            else
            {
                return null;
            }
        }
    }
}