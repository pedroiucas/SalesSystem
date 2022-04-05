using Dominio.Interfaces;
using Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Servicos.Cliente
{
    public class ServicoCliente : IServicoCliente
    {
        private IRepositorioCliente RepositorioCliente;

        public ServicoCliente(IRepositorioCliente RepositorioCliente)
        {
            this.RepositorioCliente = RepositorioCliente;
        }

        public void Cadastrar(SistemaVenda.Dominio.Entidades.Cliente Cliente)
        {
            RepositorioCliente.Create(Cliente);
        }

        public SistemaVenda.Dominio.Entidades.Cliente CarregarRegistro(int id)
        {
            return RepositorioCliente.Read(id);
        }

        public void Excluir(int id)
        {
            RepositorioCliente.Delete(id);
        }

        public IEnumerable<SistemaVenda.Dominio.Entidades.Cliente> Listagem()
        {
            return RepositorioCliente.Read();
        }
    }
}