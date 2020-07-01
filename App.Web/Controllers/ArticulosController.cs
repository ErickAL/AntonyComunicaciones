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
    public class ArticulosController : ControllerBase
    {
        private readonly DataContext _context;

        public ArticulosController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Articulos
        [HttpGet]
        public IEnumerable<ItemEntity> GetItems()
        {
            return _context.Items;
        }

        // GET: api/Articulos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemEntity = await _context.Items.FindAsync(id);

            if (itemEntity == null)
            {
                return NotFound();
            }

            return Ok(itemEntity);
        }

        // PUT: api/Articulos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemEntity([FromRoute] int id, [FromBody] ItemEntity itemEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemEntityExists(id))
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

        // POST: api/Articulos
        [HttpPost]
        public async Task<IActionResult> PostItemEntity([FromBody] ItemEntity itemEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Items.Add(itemEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemEntity", new { id = itemEntity.Id }, itemEntity);
        }

        // DELETE: api/Articulos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemEntity = await _context.Items.FindAsync(id);
            if (itemEntity == null)
            {
                return NotFound();
            }

            _context.Items.Remove(itemEntity);
            await _context.SaveChangesAsync();

            return Ok(itemEntity);
        }

        private bool ItemEntityExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}