using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Web.Data.Entity
{
    public class BrandEntity
    {
        public int Id { get; set; }
        [Display(Name = "Nombre ")]
        public string Name { get; set; }
        public ICollection<ItemEntity> Items { get; set; }
    }
}