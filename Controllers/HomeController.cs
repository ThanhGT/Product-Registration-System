using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TT_LAB2.Models;
using TT_LAB2.Models.ViewModels;

namespace TT_LAB2.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository productRepository;
        private ICategoryRepository categoryRepository;

        public int PageSize = 4;    //displays how many items on each page

        public HomeController(IProductRepository pRepository, ICategoryRepository cRepository)
        {
            productRepository = pRepository;
            categoryRepository = cRepository;
        }

        public ViewResult CategoryForm()
        {
            return View();
        }

        public ViewResult ProductForm()
        {
            return View();
        }

        public ViewResult ProductSummary(int prodPage = 1) => View(new ProductListViewModel
        {
                                                            Products = productRepository.Products 
                                                            .OrderBy(p => p.ProductId) // print the valued order by product id
                                                            .Skip((prodPage - 1) * PageSize) // clear all the products displayed on prev page
                                                            .Take(PageSize), //display again 4 books
                                                            PageInfo = new PageInfo
                                                            {
                                                                CurrentPage = prodPage,
                                                                ItemsPerPage = PageSize,
                                                                TotalItems = productRepository.Products.Count()
                                                            }
        });

        public IActionResult Index()
        {
            ViewBag.Title = "Product Registration System";
            return View("Index");
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            productRepository.AddProduct(product);
            return RedirectToAction("ProductSummary");
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            categoryRepository.AddCategory(category);
            return RedirectToAction("ProductSummary");
        }

    }
}
