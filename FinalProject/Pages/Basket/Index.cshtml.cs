using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Core;
using FinalProject.Models.Identity;
using FinalProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using FinalProject.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.Pages.Basket
{
    //[AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IBasketRepository _basketRepository;
        private string _username = null;


        public IndexModel(SignInManager<ApplicationUser> signInManager, IBasketRepository basketRepository)
        {
            _signInManager = signInManager;
            _basketRepository = basketRepository;
        }
        public BasketViewModel BasketModel { get; set; }

        public void OnGet()
        {
            SetBasketModel();
        }

        public async Task<IActionResult> OnPost(Product product)
        {
            if (product?.Id == null)
            {
                return RedirectToPage("/Index");
            }
            SetBasketModel();
            var basket = _basketRepository.GetById(BasketModel.Id);
            basket.AddItem(product.Id, product.Price);
            _basketRepository.Update(basket);
            return RedirectToPage();

        }

        public void OnPostUpdate(Dictionary<string, int> items)
        {
            SetBasketModel();
            var basket = _basketRepository.GetById(BasketModel.Id);
            foreach (var item in basket.Items)
            {
                if (items.TryGetValue(item.Id.ToString(), out var quantity))
                {
                    item.SetNewQuantity(quantity);
                }
            }
            basket.RemoveEmptyItems();
            _basketRepository.Update(basket);
            SetBasketModel();
        }

        private void SetBasketModel()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                BasketModel = GetOrCreateBasketForUser(User.Identity.Name);
            }
            else
            {
                GetOrSetBasketCookieAndUserName();
                BasketModel = GetOrCreateBasketForUser(_username);
            }
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

        private BasketViewModel GetOrCreateBasketForUser(string userId)
        {
            var baskets = _basketRepository.GetAll()
                .Include(b => b.Items)
                .ThenInclude(item => item.Product);
            var basket = baskets.FirstOrDefault(basket => basket.BuyerId == userId);

            if (basket == null)
            {
                basket = new Models.Entities.Basket(userId);
                _basketRepository.Add(basket);

                return new BasketViewModel()
                {
                    BuyerId = basket.BuyerId,
                    Id = basket.Id,
                    Items = new List<BasketItem>()
                };
            }
            return new BasketViewModel()
            {
                Id = basket.Id,
                Items = basket.Items.ToList(),
                BuyerId = basket.BuyerId
            };
        }

    }
}