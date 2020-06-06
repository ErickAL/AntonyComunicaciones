using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Web.Data;
using App.Web.Data.Entity;

namespace App.Web.Controllers
{
    public class ItemsController : Controller
    {
        private readonly DataContext _context;

        public ItemsController(DataContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemEntity = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemEntity == null)
            {
                return NotFound();
            }

            return View(itemEntity);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhotoUrl,Price,Stock")] ItemEntity itemEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemEntity);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemEntity = await _context.Items.FindAsync(id);
            if (itemEntity == null)
            {
                return NotFound();
            }
            return View(itemEntity);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhotoUrl,Price,Stock")] ItemEntity itemEntity)
        {
            if (id != itemEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemEntityExists(itemEntity.Id))
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
            return View(itemEntity);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemEntity = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemEntity == null)
            {
                return NotFound();
            }

            return View(itemEntity);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemEntity = await _context.Items.FindAsync(id);
            _context.Items.Remove(itemEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemEntityExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
