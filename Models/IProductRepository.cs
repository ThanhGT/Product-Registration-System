using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TT_LAB2.Models
{
    public interface IProductRepository
    {
        //pure getter method
        IEnumerable<Product> Products { get; }

        //getting product and category by id's
        Product this[int id] { get; }

        //add method
        Product AddProduct(Product product);
        //update method
        Product UpdateProduct(Product product);
        //delete method
        void DeleteProduct(int id);
    }
}
