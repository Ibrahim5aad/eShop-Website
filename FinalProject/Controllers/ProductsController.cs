using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models.Entities;
using FinalProject.Models.ViewModels;
using FinalProject.Core;
using Microsoft.AspNetCore.Http;
using FinalProject.Models;
using System.IO;

namespace FinalProject.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;

        public ProductsController(IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            return View(productRepository.GetAll().ToList());
        }

        #region Details    
        public IActionResult Details(int id)
        {
            var prod = productRepository.GetById(id);
            ProdCatViewModel DetailsProdCatVM = new ProdCatViewModel
            {
                Product = prod,
                Categories = categoryRepository.GetAll().ToList(),
                Products = productRepository.GetAll().Where(e => e.Category.Id == prod.Category.Id).ToList()
            };
            return View("Detail", DetailsProdCatVM);
        }

        #endregion

        #region Create  
        // GET: Products/Create
        public IActionResult Create()
        {
            ProdCatViewModel ProdCatVM = new ProdCatViewModel
            {
                Categories = categoryRepository.GetAll().ToList()
            };
            return View("productForm", ProdCatVM);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.Add(product);
                TempData["Message"] = "Product Added successfully";

                return RedirectToAction(nameof(Index));
            }
            ProdCatViewModel ProdCatVM = new ProdCatViewModel
            {
                Categories = categoryRepository.GetAll().ToList()
            };
            return View("productForm", ProdCatVM);
        }
        #endregion

        #region Edit  
        public IActionResult Edit(int id)

        {
            ProdCatViewModel ProdCatVM = new ProdCatViewModel
            {
                Categories = categoryRepository.GetAll().ToList(),
                Product = productRepository.GetById(id)
            };
            return View("productForm", ProdCatVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, Product product)
        {
            if (id != product.Id)
            {
                NotFound();
            }
            if (ModelState.IsValid)
            {
                productRepository.Update(product);
                TempData["Message"] = "Product edited successfully";

                return RedirectToAction(nameof(Index));
            }
            ProdCatViewModel ProdCatVM = new ProdCatViewModel
            {
                Categories = categoryRepository.GetAll().ToList(),
                Product = product
            };
            return View("productForm", ProdCatVM);
        }
        #endregion

        #region Delect
        public IActionResult Delete(int id)
        {
            Product product = productRepository.GetById(id);
            if (product == null)
            {
                ErrorViewModel ErrorVM = new ErrorViewModel
                {
                    RequestId = "Product not found"
                };
                return View("Error", ErrorVM);
            }


            productRepository.Delete(product);
            TempData["Message"] = "Product deleted successfully";

            return RedirectToAction(nameof(Index));
        }
        #endregion

    }

}
