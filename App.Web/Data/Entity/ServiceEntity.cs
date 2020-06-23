using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Data.Entity
{
    public class ServiceEntity
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="El campo {0} es requerido.")]
     
        [Display(Name="Nombre")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido.")]

        [Display(Name="Descripcion")]
        public string Descripcion { get; set; }
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public double Price { get; set; }
        public ICollection<PurchaseServiceDetailEntity> Purchases { get; set; }
    }
}
