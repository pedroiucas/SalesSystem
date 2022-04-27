using Aplicacao.Helpers;
using Aplicacao.Servico.Interfaces;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Dominio.Entidades;
using SistemaVenda.Models;
using System.Collections.Generic;
using System.Linq;

namespace Aplicacao.Servico
{
    public class ServicoAplicacaoProduto : IServicoAplicacaoProduto
    {
        private IServicoProduto ServicoProduto;
        private IServicoVenda ServicoVenda;

        public ServicoAplicacaoProduto(IServicoProduto ServicoProduto, IServicoVenda ServicoVenda)
        {
            this.ServicoProduto = ServicoProduto;
            this.ServicoVenda = ServicoVenda;
        }

        public void Cadastrar(ProdutoViewModel Produto)
        {
            Produto ProdutoEnti = new Produto()
            {
                Codigo = Produto.Codigo,
                Descricao = Produto.Descricao,
                Quantidade = Produto.Quantidade,
                Valor = (decimal)Produto.Valor,
                CodigoCategoria = (int)Produto.CodigoCategoria,
                CodigoUsuario = Produto.CodigoUsuario
            };

            ServicoProduto.Cadastrar(ProdutoEnti);
        }

        public void Excluir(int codigo)
        {
            if (VerificaVinculoComVenda(codigo))
            {
                throw new MensagemErroException("Não é possível deletar o produto pois ele está vínculado a uma venda.");
            }
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
                CodigoUsuario = registro.CodigoUsuario
            };
           
            return Produto;
        }

        public IEnumerable<ProdutoViewModel> Listagem(int CodigoUsuario)
        {
            var lista = ServicoProduto.Listagem().Where(e => e.CodigoUsuario == CodigoUsuario);
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

        public IEnumerable<SelectListItem> ListagemSelectList(int CodigoUsuario)
        {
            List<SelectListItem> listaSelect = new List<SelectListItem>();
            var listaCategorias = Listagem(CodigoUsuario);
            listaSelect.Add(new SelectListItem() { Value = "0", Text = "Selecione" });

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

        public bool VerificaVinculoComVenda(int codigo)
        {
            var produto = ServicoVenda.CarregarRegistroPorProduto(codigo);

            if (produto.Count() > 0)
            {
                return true;
            }

            return false;
        }
    }
}