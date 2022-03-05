using Aplicacao.Servico.Interfaces;
using Dominio.Interfaces;
using SistemaVenda.Models;
using System.Collections.Generic;

namespace Aplicacao.Servico
{
    public class ServicoAplicacaoCategoria : IServicoAplicacaoCategoria
    {
        private IServicoCategoria ServicoCategoria;

        public ServicoAplicacaoCategoria(IServicoCategoria ServicoCategoria)
        {
            this.ServicoCategoria = ServicoCategoria;
        }

        public IEnumerable<CategoriaViewModel> Listagem()
        {
            var lista = ServicoCategoria.Listagem();
            List<CategoriaViewModel> listaCategoria = new List<CategoriaViewModel>();
            foreach (var item in lista)
            {
                CategoriaViewModel categoria = new CategoriaViewModel()
                {
                    Codigo = item.Codigo,
                    Descricao = item.Descricao,
                };

                listaCategoria.Add(categoria);
            }

            return listaCategoria;
        }
    }
}