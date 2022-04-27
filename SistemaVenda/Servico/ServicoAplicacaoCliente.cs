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
    public class ServicoAplicacaoCliente : IServicoAplicacaoCliente
    {
        private IServicoCliente ServicoCliente;
        private IServicoVenda ServicoVenda;

        public ServicoAplicacaoCliente(IServicoCliente ServicoCliente, IServicoVenda ServicoVenda)
        {
            this.ServicoCliente = ServicoCliente;
            this.ServicoVenda = ServicoVenda;
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
                CodigoUsuario = cliente.CodigoUsuario
            };

            ServicoCliente.Cadastrar(clienteEnti);
        }

        public void Excluir(int codigo)
        {
            if (VerificaVinculoVenda(codigo))
            {
                throw new MensagemErroException("Não é possível deletar o cliente pois ele está vínculado a uma venda.");
            }

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

        public IEnumerable<ClienteViewModel> Listagem(int CodigoUsuario)
        {
            var lista = ServicoCliente.Listagem().Where(e => e.CodigoUsuario == CodigoUsuario);
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

        public IEnumerable<SelectListItem> ListagemSelectList(int CodigoUsuario)
        {
            List<SelectListItem> listaSelect = new List<SelectListItem>();
            var listaCategorias = Listagem(CodigoUsuario);
            listaSelect.Add(new SelectListItem() { Value = "0", Text = "Selecione"});

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

        public bool VerificaVinculoVenda(int id)
        {
            var venda = ServicoVenda.CarregarRegistroPorCliente(id).ToList();

            if (venda.Count() > 0)
            {
                return true;
            }

            return false;
        }
    }
}