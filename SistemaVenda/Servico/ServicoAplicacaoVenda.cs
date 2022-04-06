using Aplicacao.Servico.Interfaces;
using Dominio.Interfaces;
using Newtonsoft.Json;
using SistemaVenda.Dominio.Entidades;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;

namespace Aplicacao.Servico
{
    public class ServicoAplicacaoVenda : IServicoAplicacaoVenda
    {
        private IServicoVenda ServicoVenda;

        public ServicoAplicacaoVenda(IServicoVenda ServicoVenda)
        {
            this.ServicoVenda = ServicoVenda;
        }

        public void Cadastrar(VendaViewModel Venda)
        {
            Venda VendaEnti = new Venda()
            {
                CodigoCliente = (int)Venda.CodigoCliente,
                Data = (DateTime)Venda.Data,
                Total = (decimal)Venda.Total,
                Produtos = JsonConvert.DeserializeObject<ICollection<VendaProdutos>>(Venda.JsonProdutos),
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
            var registro = ServicoVenda.CarregarRegistro(codigo);

            VendaViewModel Venda = new VendaViewModel()
            {
                Codigo = registro.Codigo,
                CodigoCliente = (int)registro.CodigoCliente,
                Data = (DateTime)registro.Data,
                Total = (decimal)registro.Total,
            };
           
            return Venda;
        }

        public IEnumerable<VendaViewModel> Listagem()
        {
            var lista = ServicoVenda.Listagem();
            List<VendaViewModel> listaVenda = new List<VendaViewModel>();
            foreach (var item in lista)
            {
                VendaViewModel Venda = new VendaViewModel()
                {
                    Codigo = item.Codigo,
                    CodigoCliente = (int)item.CodigoCliente,
                    Data = (DateTime)item.Data,
                    Total = (decimal)item.Total,
                };

                listaVenda.Add(Venda);
            }

            return listaVenda;
        }

        public IEnumerable<GraficoViewModel> ListaGrafico()
        {
            List<GraficoViewModel> lista = new List<GraficoViewModel>();

            foreach (var item in ServicoVenda.ListaGrafico())
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