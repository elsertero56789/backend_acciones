using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "El símbolo no puede tener más de 10 caracteres")]
        public string Simbolo { get; set; } = string.Empty;
        [Required]
        [MaxLength(10, ErrorMessage = "El nombre de la compañia no puede tener más de 10 caracteres")]
        public string NombreCompania {get; set; } = string.Empty;
        [Required]
        [Range(1, 10000000000)]
        public decimal Compra { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal UltimoDividendo { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "La Industria no puede tener mas de 10 carácteres")]
        public string Industria { get; set; } = string.Empty;

        [Range(1, 5000000000)]
        public long CapitalizdeMercado { get; set; }
    }
}