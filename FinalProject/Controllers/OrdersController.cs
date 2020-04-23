using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models.Entities;
using FinalProject.Core;
using Microsoft.AspNetCore.Identity;
using FinalProject.Models.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public OrdersController(IBasketRepository basketRepository, IOrderRepository orderRepository, SignInManager<ApplicationUser> signInManager)
        {
            _basketRepository = basketRepository;
            _orderRepository = orderRepository;
            _signInManager = signInManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }

            var order = _orderRepository.GetAll()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult CheckoutBasket(string BuyerId, Dictionary<string, int> items)
        {
            if (ModelState.IsValid)
            {
                var basket = _basketRepository.GetAll()
                    .Include(basket => basket.Items)
                    .ThenInclude(item => item.Product)
                    .FirstOrDefault(basket => basket.BuyerId == BuyerId);

                var OrderItems = new List<OrderItem>();
                foreach (var item in basket.Items)
                {
                    if (items.TryGetValue(item.Id.ToString(), out var quantity))
                    {
                        OrderItems.Add(new OrderItem(item.Product.Id, item.Price, quantity));
                    }
                }
                var order = _orderRepository.Add(new Order()
                {
                    BuyerId = BuyerId,
                    Items = OrderItems,
                    Status = OrderStatus.Pending,
                    CheckoutDate = DateTime.Now
                });
                _orderRepository.Add(order);
                return View(order);
            }
            return BadRequest();
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BuyerId,Status,CheckoutDate")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View();// order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var order = await _context.Orders.FindAsync(id);
            //_context.Orders.Remove(order);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return true;// _context.Orders.Any(e => e.Id == id);
        }
    }
}
