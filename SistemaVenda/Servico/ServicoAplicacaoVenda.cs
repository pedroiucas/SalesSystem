using Aplicacao.Models;
using Aplicacao.Servico.Interfaces;
using Dominio.Interfaces;
using Dominio.Repositorio;
using Newtonsoft.Json;
using SistemaVenda.Dominio.Entidades;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aplicacao.Servico
{
    public class ServicoAplicacaoVenda : IServicoAplicacaoVenda
    {
        private IServicoVenda ServicoVenda;
        private IServicoCliente ServicoCliente;
        private IServicoProduto ServicoProduto;
        private IRepositorioVendaProdutos RepositorioVendaProdutos;
        public ServicoAplicacaoVenda(IServicoVenda ServicoVenda, IServicoCliente ServicoCliente, IRepositorioVendaProdutos RepositorioVendaProdutos, IServicoProduto ServicoProduto)
        {
            this.ServicoVenda = ServicoVenda;
            this.ServicoCliente = ServicoCliente;
            this.RepositorioVendaProdutos = RepositorioVendaProdutos;
            this.ServicoProduto = ServicoProduto;
        }

        public void Cadastrar(VendaViewModel Venda)
        {
            Venda VendaEnti = new Venda()
            {
                Codigo = Venda.Codigo,
                CodigoCliente = (int)Venda.CodigoCliente,
                Data = (DateTime)Venda.Data,
                Total = decimal.Parse(Venda.Total.ToString().Substring(0, Venda.Total.ToString().Length - 2)),
                Produtos = JsonConvert.DeserializeObject<ICollection<VendaProdutos>>(Venda.JsonProdutos),
                CodigoUsuario = Venda.CodigoUsuario
            };

            ServicoVenda.Cadastrar(VendaEnti);
        }

        public void Excluir(int codigo)
        {
            ServicoVenda.Excluir(codigo);
        }

        public VendaViewModel CarregarRegistro(int codigo)
        {
            if (codigo == 0)
            {
                return new VendaViewModel();
            }
            var venda = ServicoVenda.CarregarRegistro(codigo);
            var vendaProduto = RepositorioVendaProdutos.CarregarProdutos((int)venda.Codigo);
            
            List<VendaProdutoViewModel> produtos = new List<VendaProdutoViewModel>();

            foreach (var item in vendaProduto)
            {
                var produto = ServicoProduto.CarregarRegistro(item.CodigoProduto);

                VendaProdutoViewModel vendaP = new VendaProdutoViewModel()
                {
                    CodigoProduto = item.CodigoProduto,
                    CodigoVenda = item.CodigoVenda,
                    Quantidade = item.Quantidade,
                    ValorTotal = item.ValorTotal,
                    ValorUnitario = item.ValorUnitario,
                    Venda = new VendaViewModel()
                    {
                        Codigo = venda.Codigo,
                        Total = venda.Total,
                    },
                    Produto = new ProdutoViewModel()
                    {
                        Codigo = produto.Codigo,
                        CodigoCategoria = produto.CodigoCategoria,
                        CodigoUsuario = produto.CodigoUsuario,
                        Descricao = produto.Descricao,
                        Valor = produto.Valor,
                    },
                };

                produtos.Add(vendaP);
            }

            VendaViewModel vendaView = new VendaViewModel()
            {
                Codigo = venda.Codigo,
                CodigoCliente = (int)venda.CodigoCliente,
                Data = (DateTime)venda.Data,
                Total = (decimal)venda.Total,
                VendaProduto = produtos
            };
           
            return vendaView;
        }

        public IEnumerable<VendaViewModel> Listagem(int CodigoUsuario)
        {
            var lista = ServicoVenda.Listagem().Where(e =>  e.CodigoUsuario == CodigoUsuario);
            List<VendaViewModel> listaVenda = new List<VendaViewModel>();
            foreach (var item in lista)
            {
                VendaViewModel Venda = new VendaViewModel()
                {
                    Codigo = item.Codigo,
                    CodigoCliente = (int)item.CodigoCliente,
                    Data = (DateTime)item.Data,
                    Total = (decimal)item.Total,
                    NomeCliente = ServicoCliente.CarregarRegistro((int)item.CodigoCliente).Nome
                };

                listaVenda.Add(Venda);
            }

            return listaVenda;
        }

        public IEnumerable<GraficoViewModel> ListaGrafico(int CodigoUsuario)
        {
            List<GraficoViewModel> lista = new List<GraficoViewModel>();

            foreach (var item in ServicoVenda.ListaGrafico(CodigoUsuario))
            {
                GraficoViewModel grafico = new GraficoViewModel()
                {
                    CodigoProduto = item.CodigoProduto,
                    Descricao = item.Descricao,
                    TotalVendido = item.TotalVendido,
                };

                lista.Add(grafico);
            }

            return lista;
        }
    }
}