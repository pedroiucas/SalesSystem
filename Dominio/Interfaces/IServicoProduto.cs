using SistemaVenda.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Interfaces
{
    public interface IServicoProduto : IServicoCRUD<Produto>
    {
        IEnumerable<Produto> CarregarRegistroPorCategoria(int codigo);
    }
}