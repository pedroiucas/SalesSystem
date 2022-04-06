using Aplicacao.Servico.Interfaces;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Dominio.Entidades;
using SistemaVenda.Models;
using System.Collections.Generic;

namespace Aplicacao.Servico
{
    public class ServicoAplicacaoProduto : IServicoAplicacaoProduto
    {
        private IServicoProduto ServicoProduto;

        public ServicoAplicacaoProduto(IServicoProduto ServicoProduto)
        {
            this.ServicoProduto = ServicoProduto;
        }

        public void Cadastrar(ProdutoViewModel Produto)
        {
            Produto ProdutoEnti = new Produto()
            {
                Codigo = Produto.Codigo,
                Descricao = Produto.Descricao,
                Quantidade = Produto.Quantidade,
                Valor = (decimal)Produto.Valor,
                CodigoCategoria = (int)Produto.CodigoCategoria
            };

            ServicoProduto.Cadastrar(ProdutoEnti);
        }

        public void Excluir(int codigo)
        {
            ServicoProduto.Excluir(codigo);
        }

        public ProdutoViewModel CarregarRegistro(int codigo)
        {
            if (codigo == 0)
            {
                return new ProdutoViewModel();
            }
            var registro = ServicoProduto.CarregarRegistro(codigo);

            ProdutoViewModel Produto = new ProdutoViewModel()
            {
                Codigo = registro.Codigo,
                Descricao = registro.Descricao,
                Quantidade = registro.Quantidade,
                Valor = registro.Valor,
                CodigoCategoria = registro.CodigoCategoria,
            };
           
            return Produto;
        }

        public IEnumerable<ProdutoViewModel> Listagem()
        {
            var lista = ServicoProduto.Listagem();
            List<ProdutoViewModel> listaProduto = new List<ProdutoViewModel>();
            foreach (var item in lista)
            {
                ProdutoViewModel Produto = new ProdutoViewModel()
                {
                    Codigo = item.Codigo,
                    Descricao = item.Descricao,
                    Quantidade = item.Quantidade,
                    Valor = item.Valor,
                    CodigoCategoria = item.CodigoCategoria,
                    DescricaoCategoria = item.Categoria.Descricao
                };

                listaProduto.Add(Produto);
            }

            return listaProduto;
        }

        public IEnumerable<SelectListItem> ListagemSelectList()
        {
            List<SelectListItem> listaSelect = new List<SelectListItem>();
            var listaCategorias = Listagem();

            foreach (var item in listaCategorias)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Descricao
                };

                listaSelect.Add(selectListItem);
            }

            return listaSelect;
        }
    }
}