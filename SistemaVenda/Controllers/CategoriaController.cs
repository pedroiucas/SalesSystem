using Aplicacao.Controllers;
using Aplicacao.Helpers;
using Aplicacao.Servico.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVenda.Helpers;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Controllers
{
    public class CategoriaController : MensagemController
    {
        private readonly IServicoAplicacaoCategoria ServicoAplicacaoCategoria;

        public CategoriaController(IServicoAplicacaoCategoria ServicoAplicacaoCategoria)
        {
            this.ServicoAplicacaoCategoria = ServicoAplicacaoCategoria;
        }

        public IActionResult Index()
        {
            return View(ServicoAplicacaoCategoria.Listagem((int)HttpContext.Session.GetInt32(Sessao.CodigoUsuario)));
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            CategoriaViewModel viewModel = ServicoAplicacaoCategoria.CarregarRegistro(id is null? 0 :(int)id);
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(CategoriaViewModel entidade)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    entidade.CodigoUsuario = HttpContext.Session.GetInt32(Sessao.CodigoUsuario);
                    ServicoAplicacaoCategoria.Cadastrar(entidade);

                    if (entidade.Codigo != 0 && entidade.Codigo != null)
                    {
                        MensagemSucesso("Categoria editada com sucesso.");
                    }
                    else
                    {
                        MensagemSucesso("Categoria cadastrada com sucesso.");
                    }
                }
                catch (MensagemErroException ex)
                {
                    MensagemErro(ex.Message);
                }
                catch (Exception ex)
                {
                    MensagemErro("Erro ao cadastrar categoria.");
                }
            }
            else
            {
                MensagemErro("Necessário preencher todos os campos obrigatórios.");
                return View(entidade);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Deletar(int? id)
        {
            CategoriaViewModel viewModel = ServicoAplicacaoCategoria.CarregarRegistro(id is null ? 0 : (int)id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Deletar(int id)
        {
            try
            {
                ServicoAplicacaoCategoria.Excluir(id);
                MensagemSucesso("Categoria deletada com sucesso.");
            }
            catch (MensagemErroException ex)
            {
                MensagemErro(ex.Message);
            }
            catch (Exception ex)
            {
                MensagemErro("Erro ao deletar categoria.");
            }

            return RedirectToAction("Index");
        }
    }
}