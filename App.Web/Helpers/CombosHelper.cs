using App.Common.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
      
        public IEnumerable<SelectListItem> GetComboRoles()
        {
            List<SelectListItem> list = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "[Select a role...]" },
                new SelectListItem { Value = "1", Text = UserType.Admin.ToString() },
                new SelectListItem { Value = "2", Text = UserType.Register.ToString()},
                new SelectListItem { Value = "3", Text = UserType.Cashier.ToString(),Selected=true}
            };

            return list;
        }
    }

}
