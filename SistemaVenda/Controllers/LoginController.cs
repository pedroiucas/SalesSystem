using Aplicacao.Servico.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenda.Helpers;
using SistemaVenda.Models;
using System.Linq;

namespace SistemaVenda.Controllers
{
    public class LoginController : Controller
    {
        readonly IServicoAplicacaoUsuario servicoAplicacaoUsuario;
        protected IHttpContextAccessor httpContext;

        public LoginController(IHttpContextAccessor httpContext, IServicoAplicacaoUsuario servicoAplicacaoUsuario)
        {
            this.servicoAplicacaoUsuario = servicoAplicacaoUsuario;
            this.httpContext = httpContext;
        }

        [HttpGet]
        public IActionResult Index(int? id)
        {
            if (id != null)
            {
                if (id == 0)
                {
                    httpContext.HttpContext.Session.Clear();
                }
            }

            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            ViewData["Erro"] = string.Empty;
            if (ModelState.IsValid)
            {
                var Senha = Criptografia.GerarMD5Hash(model.Senha);

                bool validacao = servicoAplicacaoUsuario.ValidarLogin(model.Email, Senha);
                var usuario = servicoAplicacaoUsuario.CarregarRegistro(model.Email, Senha);

                if (!validacao)
                {
                    ViewData["Erro"] = "O Email ou Senha informado não existe no sistema";
                    return View(model);
                }
                else
                {
                    httpContext.HttpContext.Session.SetString(Sessao.NomeUsuario, usuario.Nome);
                    httpContext.HttpContext.Session.SetString(Sessao.PrimeiroNome, usuario.Nome.Split(' ')[0]);
                    httpContext.HttpContext.Session.SetString(Sessao.EmailUsuario, usuario.Email);
                    httpContext.HttpContext.Session.SetInt32(Sessao.CodigoUsuario, (int)usuario.Codigo);
                    httpContext.HttpContext.Session.SetInt32(Sessao.Logado, 1);

                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
    }
}