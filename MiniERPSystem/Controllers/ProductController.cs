using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniERPSystem.Data;
using MiniERPSystem.Models;

namespace MiniERPSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly ERPDbContext _context;
        public ProductController(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            return await Task.FromResult(View());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if(ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}
