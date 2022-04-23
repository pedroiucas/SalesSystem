﻿using Aplicacao.Servico.Interfaces;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Dominio.Entidades;
using SistemaVenda.Helpers;
using SistemaVenda.Models;
using System.Collections.Generic;
using System.Linq;

namespace Aplicacao.Servico
{
    public class ServicoAplicacaoCategoria : IServicoAplicacaoCategoria
    {
        private IServicoCategoria ServicoCategoria;

        public ServicoAplicacaoCategoria(IServicoCategoria ServicoCategoria)
        {
            this.ServicoCategoria = ServicoCategoria;
        }

        public void Cadastrar(CategoriaViewModel categoria)
        {
            Categoria categoriaEnti = new Categoria()
            {
                Codigo = categoria.Codigo,
                Descricao = categoria.Descricao,
                CodigoUsuario = categoria.CodigoUsuario
            };
            ServicoCategoria.Cadastrar(categoriaEnti);
        }

        public void Excluir(int codigo)
        {
            ServicoCategoria.Excluir(codigo);
        }

        public CategoriaViewModel CarregarRegistro(int codigo)
        {
            if (codigo == 0)
            {
                return new CategoriaViewModel();
            }
            var registro = ServicoCategoria.CarregarRegistro(codigo);

            CategoriaViewModel categoria = new CategoriaViewModel()
            {
                Codigo = registro.Codigo,
                Descricao = registro.Descricao 
            };
           
            return categoria;
        }

        public IEnumerable<CategoriaViewModel> Listagem(int CodigoUsuario)
        {
            var lista = ServicoCategoria.Listagem().Where(e => e.CodigoUsuario == CodigoUsuario);
            List<CategoriaViewModel> listaCategoria = new List<CategoriaViewModel>();
            foreach (var item in lista)
            {
                CategoriaViewModel categoria = new CategoriaViewModel()
                {
                    Codigo = item.Codigo,
                    Descricao = item.Descricao,
                };

                listaCategoria.Add(categoria);
            }

            return listaCategoria;
        }

        public IEnumerable<SelectListItem> ListagemSelectList(int CodigoUsuario)
        {
            List<SelectListItem> listaSelect = new List<SelectListItem>();
            var listaCategorias = Listagem(CodigoUsuario);
            listaSelect.Add(new SelectListItem() { Value = "0", Text = "Selecione" });

            foreach (var item in listaCategorias)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Descricao
                };

                listaSelect.Add(selectListItem);
            }

            return listaSelect;
        }
    }
}