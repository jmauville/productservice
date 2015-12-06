using productservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace productservice.Infrastructure
{
   public interface IProductRepository
    {
        IEnumerable<Product> AllProducts();
        Product GetById(long id);
    }
}
