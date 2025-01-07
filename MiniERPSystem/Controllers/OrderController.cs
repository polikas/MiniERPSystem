using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniERPSystem.Data;
using MiniERPSystem.Models;

namespace MiniERPSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly ERPDbContext _context;
        public OrderController(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Products)
                .ToListAsync();
            return View(orders);
        }

        public IActionResult Create()
        {
            var customers = _context.Customers.ToList();
            ViewData["Customers"] = new SelectList(customers, "Id", "Name");
            return View(new Order());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var customers = _context.Customers.ToList();
            ViewData["Customers"] = new SelectList(customers, "Id", "Name", order.CustomerId);
            return View(order);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["Customers"] = new SelectList(_context.Customers, "Id", "Name", order?.Customer?.Id);
            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Orders.Any(o => o.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Customers"] = new SelectList(_context.Customers, "Id", "Name", order.Customer?.Id);
            return View(order);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == id);
            if(order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if(order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
