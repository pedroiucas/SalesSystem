using Dominio.Repositorio;
using Microsoft.EntityFrameworkCore;
using Repositorio.Contexto;
using Repositorio.Interfaces;
using SistemaVenda.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio.Entidades
{
    public class RepositorioCliente : Repositorio<Cliente>, IRepositorioCliente
    {
        public RepositorioCliente(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}