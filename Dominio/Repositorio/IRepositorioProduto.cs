using SistemaVenda.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Repositorio
{
    public interface IRepositorioProduto : IRepositorio<Produto>
    {
        new IEnumerable<Produto> Read();
    }
}