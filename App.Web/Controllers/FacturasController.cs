using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Web.Data;
using App.Web.Data.Entity;

namespace App.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly DataContext _context;

        public FacturasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Facturas
        [HttpGet]
        public IEnumerable<PurchaseEntity> GetPurchases()
        {
            return _context.Purchases;
        }

        // GET: api/Facturas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var purchaseEntity = await _context.Purchases.FindAsync(id);

            if (purchaseEntity == null)
            {
                return NotFound();
            }

            return Ok(purchaseEntity);
        }

        // PUT: api/Facturas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseEntity([FromRoute] int id, [FromBody] PurchaseEntity purchaseEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchaseEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(purchaseEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Facturas
        [HttpPost]
        public async Task<IActionResult> PostPurchaseEntity([FromBody] PurchaseEntity purchaseEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Purchases.Add(purchaseEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchaseEntity", new { id = purchaseEntity.Id }, purchaseEntity);
        }

        // DELETE: api/Facturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var purchaseEntity = await _context.Purchases.FindAsync(id);
            if (purchaseEntity == null)
            {
                return NotFound();
            }

            _context.Purchases.Remove(purchaseEntity);
            await _context.SaveChangesAsync();

            return Ok(purchaseEntity);
        }

        private bool PurchaseEntityExists(int id)
        {
            return _context.Purchases.Any(e => e.Id == id);
        }
    }
}