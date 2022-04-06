using Aplicacao.Servico.Interfaces;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Dominio.Entidades;
using SistemaVenda.Models;
using System.Collections.Generic;

namespace Aplicacao.Servico
{
    public class ServicoAplicacaoCliente : IServicoAplicacaoCliente
    {
        private IServicoCliente ServicoCliente;

        public ServicoAplicacaoCliente(IServicoCliente ServicoCliente)
        {
            this.ServicoCliente = ServicoCliente;
        }

        public void Cadastrar(ClienteViewModel cliente)
        {
            Cliente clienteEnti = new Cliente()
            {
                Codigo = cliente.Codigo,
                Nome = cliente.Nome,
                CNPJ_CPF = cliente.CNPJ_CPF,
                Celular = cliente.Celular,
                Email = cliente.Email,
            };

            ServicoCliente.Cadastrar(clienteEnti);
        }

        public void Excluir(int codigo)
        {
            ServicoCliente.Excluir(codigo);
        }

        public ClienteViewModel CarregarRegistro(int codigo)
        {
            if (codigo == 0)
            {
                return new ClienteViewModel();
            }
            var registro = ServicoCliente.CarregarRegistro(codigo);

            ClienteViewModel cliente = new ClienteViewModel()
            {
                Codigo = registro.Codigo,
                Nome = registro.Nome,
                CNPJ_CPF = registro.CNPJ_CPF,
                Celular = registro.Celular,
                Email = registro.Email,
            };
           
            return cliente;
        }

        public IEnumerable<ClienteViewModel> Listagem()
        {
            var lista = ServicoCliente.Listagem();
            List<ClienteViewModel> listaCliente = new List<ClienteViewModel>();
            foreach (var item in lista)
            {
                ClienteViewModel cliente = new ClienteViewModel()
                {
                    Codigo = item.Codigo,
                    Nome = item.Nome,
                    CNPJ_CPF = item.CNPJ_CPF,
                    Celular = item.Celular,
                    Email = item.Email,
                };

                listaCliente.Add(cliente);
            }

            return listaCliente;
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
                    Text = item.Nome
                };

                listaSelect.Add(selectListItem);
            }

            return listaSelect;
        }
    }
}