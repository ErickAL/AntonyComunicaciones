using App.Web.Data;
using App.Web.Data.Entity;
using App.Web.Helpers;
using App.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Controllers
{
    [Authorize(Roles = "Admin,Register")]
    public class ItemsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IUserHelper _userHelper;

        public ItemsController(DataContext context,
            ICombosHelper combosHelper,
            IImageHelper imageHelper,
            IUserHelper userHelper
            )
        {
            _context = context;
            _combosHelper = combosHelper;
            _imageHelper = imageHelper;
            _userHelper = userHelper;
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

            ItemEntity itemEntity = await _context.Items
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
        public async Task<IActionResult> Create(ItemEntity itemEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemEntity);
        }

        public async Task<IActionResult> Register()
        {
            AddItemViewModel model = new AddItemViewModel
            {
                ItemTypes = _combosHelper.GetComboItemType(),
                Brands = _combosHelper.GetComboBrands()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(AddItemViewModel model)
        {

            if (ModelState.IsValid)
            {



                if (model.PictureFile != null)
                {
                    model.PicturePath = await _imageHelper.UploadImageAsync(model.PictureFile, "Items", model.PictureName);
                }
                UserEntity user = await _userHelper.GetUserAsync(User.Identity.Name);
                ItemEntity item = new ItemEntity
                {
                    Id=model.Id,
                    Name = model.Name,
                    Price = model.Precio,
                    PhotoUrl = model.PicturePath,
                    Brand = _context.Brands.Find(model.MarcaId),
                    ItemType = _context.ItemTypes.Find(model.ItemTypeId),
                    Stock = model.Inventario,
                    User = user

                };
                _context.Add(item);
                int success = await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Items");
            }
            model.ItemTypes = _combosHelper.GetComboItemType();
            model.Brands = _combosHelper.GetComboBrands();
            return View(model);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ItemEntity itemEntity = await _context.Items.FindAsync(id);
            AddItemViewModel model = new AddItemViewModel
            {
                Id = itemEntity.Id,
                Name = itemEntity.Name,
                Brands = _combosHelper.GetComboBrands(), 
                MarcaId = itemEntity.Brand.Id,
                PicturePath = itemEntity.PhotoUrl,
                ItemTypes = _combosHelper.GetComboItemType(),
                ItemTypeId = itemEntity.ItemType.Id,
                Precio = itemEntity.Price,
                Inventario = itemEntity.Stock,   
            };
            if (itemEntity == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AddItemViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {   UserEntity user = await _userHelper.GetUserAsync(User.Identity.Name);
                    ItemEntity item = new ItemEntity
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Price = model.Precio,
                        PhotoUrl = model.PicturePath,
                        Brand = _context.Brands.Find(model.MarcaId),
                        ItemType = _context.ItemTypes.Find(model.ItemTypeId),
                        Stock = model.Inventario,
                        User = user

                    };
                try
                {

                    if (model.PictureFile != null)
                    {
                        model.PicturePath = await _imageHelper.UploadImageAsync(model.PictureFile, "Items", model.PictureName);
                    }
                 
                    _context.Update(item);
                    int success = await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemEntityExists(item.Id))
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
            return View(model);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ItemEntity itemEntity = await _context.Items
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
            ItemEntity itemEntity = await _context.Items.FindAsync(id);
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
