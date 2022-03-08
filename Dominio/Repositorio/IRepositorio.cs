using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Repositorio
{
    public interface IRepositorio<TEntidade>
        where TEntidade : class
    {
        IEnumerable<TEntidade> Read();

        void Create(TEntidade Entidade);

        TEntidade Read(int id);

        void Delete(int id);
    }
}