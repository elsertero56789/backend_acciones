using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class NewUserDTO
    {
        
        public string NombreUsuario { get; set; }

        public string Email { get; set; }
        
        public string token { get; set; }
    }
}