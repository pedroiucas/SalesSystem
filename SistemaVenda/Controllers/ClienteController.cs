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
    public class ClienteController : MensagemController
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
                try
                {
                    entidade.CodigoUsuario = HttpContext.Session.GetInt32(Sessao.CodigoUsuario);
                    ServicoAplicacaoCliente.Cadastrar(entidade);

                    if (entidade.Codigo != 0 && entidade.Codigo != null)
                    {
                        MensagemSucesso("Cliente editado com sucesso.");
                    }
                    else
                    {
                        MensagemSucesso("Cliente cadastrado com sucesso.");
                    }
                }
                catch (MensagemErroException ex)
                {
                    MensagemErro(ex.Message);
                }
                catch (Exception ex)
                {
                    MensagemErro("Erro ao cadastrar cliente.");
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
            var viewModel = ServicoAplicacaoCliente.CarregarRegistro(id is null ? 0 : (int)id);
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Deletar(int id)
        {
            try
            {
                ServicoAplicacaoCliente.Excluir(id);
                MensagemSucesso("Cliente deletado com sucesso.");
            }
            catch (MensagemErroException ex)
            {
                MensagemErro(ex.Message);
            }catch (Exception ex)
            {
                MensagemErro("Erro ao deletar cliente.");
            }

            return RedirectToAction("Index");
        }
    }
}