using Dominio.Interfaces;
using Dominio.Repositorio;
using SistemaVenda.Dominio.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Servicos.Venda
{
    public class ServicoVenda : IServicoVenda
    {
        private IRepositorioVenda RepositorioVenda;
        private IRepositorioVendaProdutos RepositorioVendaProdutos;

        public ServicoVenda(IRepositorioVenda RepositorioVenda, IRepositorioVendaProdutos RepositorioVendaProdutos)
        {
            this.RepositorioVenda = RepositorioVenda;
            this.RepositorioVendaProdutos = RepositorioVendaProdutos;
        }

        public void Cadastrar(SistemaVenda.Dominio.Entidades.Venda Venda)
        {
            RepositorioVenda.Create(Venda);
        }

        public SistemaVenda.Dominio.Entidades.Venda CarregarRegistro(int id)
        {
            return RepositorioVenda.Read(id);
        }

        public void Excluir(int id)
        {
            RepositorioVenda.Delete(id);
        }

        public IEnumerable<SistemaVenda.Dominio.Entidades.Venda> Listagem()
        {
            return RepositorioVenda.Read();
        }

        public IEnumerable<GraficoViewModel> ListaGrafico()
        {
            return RepositorioVendaProdutos.ListaGrafico();
        }
    }
}