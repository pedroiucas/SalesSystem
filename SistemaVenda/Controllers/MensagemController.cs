using Microsoft.AspNetCore.Mvc;

namespace Aplicacao.Controllers
{
    public class MensagemController : Controller
    {
        public void MensagemSucesso(string mensagem)
        {
            TempData["MensagemSucesso"] = mensagem;
        }

        public void MensagemErro(string mensagem)
        {
            TempData["MensagemErro"] = mensagem;
        }
    }
}
