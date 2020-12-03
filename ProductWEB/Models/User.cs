using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWEB.Models
{
    public class User
    {
        public User()
        {
            Errors = new List<Errors>();
        }
        [Required(ErrorMessage = "El correo es requerido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; }
        public List<Errors> Errors { get; set; }
    }
}
