using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi2_Produtos.Controllers;

namespace WebApi2_Produtos.Controllers
{
    public class ProdutosController : ApiController
    {
        static readonly IProdutoRepositorio repositorio = new ProdutoRepositorio();

        public IEnumerable<Produto> GetAllProdutos()
        {
            return repositorio.GetAll();
        }

        public Produto GetProduto(int id) 
        {
            Produto item = repositorio.GetProduto(id);
            if(item == null)
            {
                throw new HttpResponseException(httpStatusCode.NotFound);
            }
            return item;
        }

        public IEnumerable<Produto> GetProdutosPorCategoria(string Categoria)
        {
            return repositorio.getAll().where(
                p => string.Equals(p.Categoria, Categoria, StringComparison, OrdinalIgnoreCase));
        }

        public HttpResponseMessage PostProduto(Produto item)
        {
            item = repositorio.Add(item);
            var response = Request.CreateResponse<Produto>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new {id = item.Id});
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PurProduto(int id, Produto produto)
        {
            produto.Id = id;
            if(!repositorio.Update(produto))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteProduto(int id)
        {
            Produto item = respositorio.Get(id);

            if(item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repositorio.Remove(id);
        }
    }
}