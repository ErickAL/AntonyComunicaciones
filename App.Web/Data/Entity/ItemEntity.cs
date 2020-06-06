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
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Display(Name = "Tipo")]
        public ItemTypeEntity ItemType { get; set; }
        [Display(Name = "Marca")]
        public BrandEntity Brand { get; set; }
        [Display(Name = "Foto")]
        public string PhotoUrl { get; set; }
        [Display(Name = "Precio")]
        public double Price { get; set; }
       
        public int Stock { get; set; }
        public UserEntity User { get; set; }
        public ICollection<PurchaseItemDetailEntity> Purchases  { get; set; }
        
    }
}
