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
using Aplicacao.Controllers;
using Aplicacao.Helpers;

namespace SistemaVenda.Controllers
{
    public class ProdutoController : MensagemController
    {
        private readonly IServicoAplicacaoProduto ServicoAplicacaoProduto;
        private readonly IServicoAplicacaoCategoria ServicoAplicacaoCategoria;

        public ProdutoController(IServicoAplicacaoProduto ServicoAplicacaoProduto, IServicoAplicacaoCategoria ServicoAplicacaoCategoria)
        {
            this.ServicoAplicacaoProduto = ServicoAplicacaoProduto;
            this.ServicoAplicacaoCategoria = ServicoAplicacaoCategoria;
        }

        public IActionResult Index()
        {
            return View(ServicoAplicacaoProduto.Listagem((int)HttpContext.Session.GetInt32(Sessao.CodigoUsuario)));
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            var viewModel = ServicoAplicacaoProduto.CarregarRegistro(id is null? 0: (int)id);
            viewModel.ListaCategorias = ServicoAplicacaoCategoria.ListagemSelectList((int)HttpContext.Session.GetInt32(Sessao.CodigoUsuario));

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(ProdutoViewModel entidade)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    entidade.CodigoUsuario = HttpContext.Session.GetInt32(Sessao.CodigoUsuario);
                    ServicoAplicacaoProduto.Cadastrar(entidade);

                    if (entidade.Codigo != 0 && entidade.Codigo != null)
                    {
                        MensagemSucesso("Produto editado com sucesso.");
                    }
                    else
                    {
                        MensagemSucesso("Produto cadastrado com sucesso.");
                    }
                }
                catch (MensagemErroException ex)
                {
                    MensagemErro(ex.Message);
                }
                catch (Exception ex)
                {
                    MensagemErro("Erro ao cadastrar produto.");
                }
            }
            else
            {
                entidade.ListaCategorias = ServicoAplicacaoCategoria.ListagemSelectList((int)HttpContext.Session.GetInt32(Sessao.CodigoUsuario));
                MensagemErro("Necessário preencher todos os campos obrigatórios.");
                return View(entidade);
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Deletar(int? id)
        {
            var viewModel = ServicoAplicacaoProduto.CarregarRegistro(id is null ? 0 : (int)id);
            viewModel.ListaCategorias = ServicoAplicacaoCategoria.ListagemSelectList((int)HttpContext.Session.GetInt32(Sessao.CodigoUsuario));

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Deletar(int id)
        {
            try
            {
                ServicoAplicacaoProduto.Excluir(id);
                MensagemSucesso("Produto deletado com sucesso.");
            }
            catch (MensagemErroException ex)
            {
                MensagemErro(ex.Message);
            }
            catch (Exception ex)
            {
                MensagemErro("Erro ao deletar produto.");
            }

            return RedirectToAction("Index");
        }
    }
}