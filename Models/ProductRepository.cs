using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TT_LAB2.Models
{
    public class ProductRepository : IProductRepository
    {
        private Dictionary<int, Product> items;
        public ProductRepository()
        {
            items = new Dictionary<int, Product>();

            //sample data
            new List<Product>
            {
                new Product{ ProductId=001, ProductName="ROG Zephyrus GX501", Price=1799.00M, CategoryId = 101 },
                new Product{ ProductId=002, ProductName="iWALK Ultra Slim Power Bank", Price=124.24M, CategoryId = 101 },
                new Product{ ProductId=003, ProductName="Campfire Audio Andromeda", Price=1253.50M, CategoryId = 201 },
                new Product{ ProductId=004, ProductName="ASUS ROG 5 Ultimate", Price=2999.99M, CategoryId = 101 },
                new Product{ ProductId=005, ProductName="Fiio M11", Price=810.67M, CategoryId = 201 }
            }.ForEach(p => AddProduct(p));
        }

        public Product this[int id] => items.ContainsKey(id) ? items[id] : null;
        public IEnumerable<Product> Products => items.Values;
        public Product AddProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                int key = items.Count;
                while (items.ContainsKey(key))
                { key++; }
            }
            items[product.ProductId] = product;
            return product;
        }

        public void DeleteProduct(int id) => items.Remove(id);

        public Product UpdateProduct(Product product) =>
            AddProduct(product);
    }
}
