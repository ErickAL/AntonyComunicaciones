using App.Common.Enums;
using App.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext dataContext)
        {
            _context = dataContext;
        }
        public IEnumerable<SelectListItem> GetComboBrands()
        {
            List<SelectListItem> bandsList = new List<SelectListItem>();
            bandsList.Add(new SelectListItem { Value = "0", Text = "[Seleccione una marca...]" });
           var items= _context.Brands.ToList();
             foreach (var item in items)
            {
                bandsList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
            }

            return bandsList;

        }

        public IEnumerable<SelectListItem> GetComboItemType()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "[Seleccione un tipo...]" });
            var items = _context.ItemTypes.ToList();
            foreach (var item in items)
            {  
                list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name});
            }
            
            return list;

        }

        public IEnumerable<SelectListItem> GetComboRoles()
        {
            List<SelectListItem> list = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "[Seleccione un rol...]" },
                new SelectListItem { Value = "1", Text = UserType.Admin.ToString() },
                new SelectListItem { Value = "2", Text = UserType.Register.ToString()},
                new SelectListItem { Value = "3", Text = UserType.Cashier.ToString(),Selected=true}
            };

            return list;
        }
    }

}
