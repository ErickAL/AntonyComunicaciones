using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Correo electronico")]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        [Display(Name = "Recuerda rme?")]
        public bool RememberMe { get; set; }
    }
}
