using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Data.Entity
{
    public class ItemEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemTypeEntity ItemType { get; set; }
        public Brand Brand { get; set; }
        [Display(Name = "Picture")]
        public string PhotoUrl { get; set; }
        public double Price { get; set; }
       
        public int Stock { get; set; }
    }
}
