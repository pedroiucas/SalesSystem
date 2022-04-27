using System;

namespace Aplicacao.Helpers
{
    public class MensagemErroException: Exception
    {
        public MensagemErroException(string message): base(message) { 
        }
    }
}
