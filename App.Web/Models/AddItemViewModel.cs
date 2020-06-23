using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models
{
    public class AddItemViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="El campo {0} es requerido.")]
       
        public string Name { get; set; }
        [Display(Name="Marcas")]
        public IEnumerable<SelectListItem> Brands { get; set; }
        public int MarcaId { get; set; }
        [Display(Name = "Imagen")]
        public IFormFile PictureFile { get; set; }
        [Display(Name = "Picture")]
        public string PicturePath { get; set; }
        public IEnumerable<SelectListItem> ItemTypes { get; set; }
        public int ItemTypeId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido.")]
        public double Precio { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido.")]
        public int Inventario { get; set; }
        private string pictureName;
        public string PictureName { 
            get 
            {
              return  string.IsNullOrEmpty(pictureName) ?Guid.NewGuid().ToString(): pictureName;
            } set
            {
                pictureName = value;
            }
        }
    }
}
