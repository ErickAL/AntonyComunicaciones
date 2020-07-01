using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Web.Data;
using App.Web.Data.Entity;
using App.Web.Helpers;
using App.Web.Models;

namespace App.Web.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public PurchasesController(DataContext context,
            IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        // GET: Purchases
        public async Task<IActionResult> Index()
        {
            return View(await _context.Purchases
                .Include(x=>x.User)
                .Include(x=>x.PurchaseItemDetails)
                .Include(x=>x.PurchaseServiceDetails)
                .ToListAsync());
        }

        // GET: Purchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseEntity = await _context.Purchases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseEntity == null)
            {
                return NotFound();
            }

            return View(purchaseEntity);
        }
        public async Task<IActionResult> Facturar()
        {
            PurchaseEntity model = new PurchaseEntity();
            model.Date = DateTime.Now;
            model.PurchaseItemDetails = new List<PurchaseItemDetailEntity>();
            model.PurchaseServiceDetails = new List<PurchaseServiceDetailEntity>();
            FacturaViewModel viewmodel = new FacturaViewModel();
            viewmodel.Factura = model;
            viewmodel.Articulo = await _context.Items.Where(x => x.Stock > 0).ToListAsync();
            viewmodel.Servicios = await _context.Services.ToListAsync();

            return View(viewmodel);
        }
        // GET: Purchases/Create
        public async  Task<IActionResult> Create()
        {
            PurchaseEntity model = new PurchaseEntity();
            model.Date = DateTime.Now;
            model.PurchaseItemDetails = new List<PurchaseItemDetailEntity>();
            model.PurchaseServiceDetails= new List<PurchaseServiceDetailEntity>();
            return  View(model);
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseEntity purchaseEntity)
        {
            if (ModelState.IsValid)
            {
                purchaseEntity.User = await _userHelper.GetUserAsync(this.User.Identity.Name);
                _context.Add(purchaseEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchaseEntity);
        }

        // GET: Purchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseEntity = await _context.Purchases.FindAsync(id);
            if (purchaseEntity == null)
            {
                return NotFound();
            }
            return View(purchaseEntity);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date")] PurchaseEntity purchaseEntity)
        {
            if (id != purchaseEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseEntityExists(purchaseEntity.Id))
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
            return View(purchaseEntity);
        }

        // GET: Purchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseEntity = await _context.Purchases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseEntity == null)
            {
                return NotFound();
            }

            return View(purchaseEntity);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseEntity = await _context.Purchases.FindAsync(id);
            _context.Purchases.Remove(purchaseEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseEntityExists(int id)
        {
            return _context.Purchases.Any(e => e.Id == id);
        }
    }
}
