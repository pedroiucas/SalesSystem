using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenda.DAL;
using SistemaVenda.Helpers;
using SistemaVenda.Models;
using System.Linq;

namespace SistemaVenda.Controllers
{
    public class LoginController : Controller
    {
        protected ApplicationDbContext mContext;
        protected IHttpContextAccessor httpContext;

        public LoginController(ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
            this.mContext = context;
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
                var usuario = mContext.Usuario.Where(x => x.Email == model.Email && x.Senha == Senha).FirstOrDefault();

                if (usuario == null)
                {
                    ViewData["Erro"] = "O Email ou Senha informado não existe no sistema";
                    return View(model);
                }
                else
                {
                    httpContext.HttpContext.Session.SetString(Sessao.NomeUsuario, usuario.Nome);
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