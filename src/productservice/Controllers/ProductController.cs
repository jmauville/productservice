
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using productservice.Infrastructure;
using productservice.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace productservice.Controllers
{
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly IProductRepository repository;

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return this.repository.AllProducts();
        }

        // GET api/values/5
        [HttpGet("{id:length(24)}", Name = "GetByIdRoute")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
