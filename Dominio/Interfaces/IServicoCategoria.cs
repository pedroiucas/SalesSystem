using SistemaVenda.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Interfaces
{
    public interface IServicoCategoria
    {
        IEnumerable<Categoria> Listagem();

        void Cadastrar(Categoria categoria);

        Categoria CarregarRegistro(int id);

        void Excluir(int id);
    }
}