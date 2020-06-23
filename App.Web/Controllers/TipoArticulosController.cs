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
    public class TipoArticulosController : Controller
    {
        private readonly DataContext _context;

        public TipoArticulosController(DataContext context)
        {
            _context = context;
        }

        // GET: TipoArticulos
        public async Task<IActionResult> Index()
        {
            return View(await _context.ItemTypes.ToListAsync());
        }

        // GET: TipoArticulos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemTypeEntity = await _context.ItemTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemTypeEntity == null)
            {
                return NotFound();
            }

            return View(itemTypeEntity);
        }

        // GET: TipoArticulos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoArticulos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ItemTypeEntity itemTypeEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemTypeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemTypeEntity);
        }

        // GET: TipoArticulos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemTypeEntity = await _context.ItemTypes.FindAsync(id);
            if (itemTypeEntity == null)
            {
                return NotFound();
            }
            return View(itemTypeEntity);
        }

        // POST: TipoArticulos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ItemTypeEntity itemTypeEntity)
        {
            if (id != itemTypeEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemTypeEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemTypeEntityExists(itemTypeEntity.Id))
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
            return View(itemTypeEntity);
        }

        // GET: TipoArticulos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemTypeEntity = await _context.ItemTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemTypeEntity == null)
            {
                return NotFound();
            }

            return View(itemTypeEntity);
        }

        // POST: TipoArticulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemTypeEntity = await _context.ItemTypes.FindAsync(id);
            _context.ItemTypes.Remove(itemTypeEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemTypeEntityExists(int id)
        {
            return _context.ItemTypes.Any(e => e.Id == id);
        }
    }
}
