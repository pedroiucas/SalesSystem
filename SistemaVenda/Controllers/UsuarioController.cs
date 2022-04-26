using Aplicacao.Models;
using Aplicacao.Servico.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SistemaVenda.Helpers;

namespace Aplicacao.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IServicoAplicacaoUsuario ServicoAplicacaoUsuario;

        public UsuarioController(IServicoAplicacaoUsuario ServicoAplicacaoUsuario)
        {
            this.ServicoAplicacaoUsuario = ServicoAplicacaoUsuario;
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            UsuarioViewModel viewModel = ServicoAplicacaoUsuario.CarregarRegistro(id is null ? 0 : (int)id);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(UsuarioViewModel entidade)
        {
            if (ModelState.IsValid)
            {
                entidade.Senha = Criptografia.GerarMD5Hash(entidade.Senha);
                ServicoAplicacaoUsuario.Cadastrar(entidade);
            }
            else
            {
                return View(entidade);
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public IActionResult Deletar(int? id)
        {
            UsuarioViewModel viewModel = ServicoAplicacaoUsuario.CarregarRegistro(id is null ? 0 : (int)id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Deletar(int id)
        {
            ServicoAplicacaoUsuario.Excluir(id);
            return RedirectToAction("Index");
        }
    }
}
