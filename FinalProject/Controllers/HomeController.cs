using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FinalProject.Models;
using FinalProject.Core;
using FinalProject.Models.Entities;
using FinalProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FinalProject.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinalProject.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        string _username = null;

        public HomeController(ILogger<HomeController> logger,
                              IProductRepository productRepository,
                              ICategoryRepository categoryRepository,
                              IBasketRepository basketRepository,
                              SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _basketRepository = basketRepository;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index(int? category, int? pageNumber, string search)
        {
            IQueryable<Product> products;
            if (category != null)
            {
                products = _productRepository.GetAll()
                                             .Where(p => p.Name.StartsWith(search) || search == null)
                                             .Where(p => p.FK_Category_Id == category);
            }
            else
            {
                products = _productRepository.GetAll()
                                             .Where(p => p.Name.StartsWith(search) || search == null);
            }
            
            List<Category> cats = _categoryRepository.GetAll().ToList();
            HomeViewModel hvm = new HomeViewModel()
            {
                Products = await PaginatedList<Product>.CreateAsync(products, pageNumber ?? 1, 6),
                Categories = cats
                
            };

            return View(hvm);
        }
        public async Task<IActionResult> Index2(string sortBy, int? pageNumber)
        {
            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "NameD" : "";

            HomeViewModel IndexProdCatVM = new HomeViewModel { };
            switch (sortBy)
            {
                case "NameD":
                    IndexProdCatVM = new HomeViewModel
                    {

                        Products = await PaginatedList<Product>.CreateAsync(_productRepository.GetAll().OrderByDescending(p => p.Name), pageNumber ?? 1, 6),
                        Categories = _categoryRepository.GetAll().ToList(),
                    };
                    break;

                default:

                    IndexProdCatVM = new HomeViewModel
                    {

                        Products = await PaginatedList<Product>.CreateAsync(_productRepository.GetAll().OrderBy(p => p.Name), pageNumber ?? 1, 6),
                        Categories = _categoryRepository.GetAll().ToList(),
                    };
                    break;
            }
            return View("Index", IndexProdCatVM);
        }

        public async Task<IActionResult> Index3(string sortBy2, int? pageNumber)
        {
            ViewBag.SortPriceParameter = string.IsNullOrEmpty(sortBy2) ? "PriceD" : "";

            HomeViewModel IndexProdCatVM = new HomeViewModel { };
            switch (sortBy2)
            {
                case "PriceD":
                    IndexProdCatVM = new HomeViewModel
                    {
                        Products = await PaginatedList<Product>.CreateAsync(_productRepository.GetAll().OrderByDescending(p => p.Price), pageNumber ?? 1, 6),
                        Categories = _categoryRepository.GetAll().ToList(),
                    };
                    break;
                default:

                    IndexProdCatVM = new HomeViewModel
                    {
                        Products = await PaginatedList<Product>.CreateAsync(_productRepository.GetAll().OrderBy(p => p.Price), pageNumber ?? 1, 6),
                        Categories = _categoryRepository.GetAll().ToList(),
                    };
                    break;
            }
            return View("Index", IndexProdCatVM);
        }
        public IActionResult Basket()
        {
            Basket basket;

            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                basket = _basketRepository.GetAll()
                                    .Include(b => b.Items)
                                    .ThenInclude(item => item.Product)
                                    .FirstOrDefault(b => b.BuyerId == User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            else
            {
                GetOrSetBasketCookieAndUserName();
                basket = _basketRepository.GetAll()
                                    .Include(b => b.Items)
                                    .ThenInclude(item => item.Product)
                                    .FirstOrDefault(b => b.BuyerId == _username);
            }

            return PartialView("_basket", basket);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void GetOrSetBasketCookieAndUserName()
        {
            if (Request.Cookies.ContainsKey("BASKET_COOKIENAME"))
            {
                _username = Request.Cookies["BASKET_COOKIENAME"];
            }
            if (_username != null) return;

            _username = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions { IsEssential = true };
            cookieOptions.Expires = DateTime.Today.AddYears(10);
            Response.Cookies.Append("BASKET_COOKIENAME", _username, cookieOptions);
        }
    }
}
