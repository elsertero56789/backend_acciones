using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class UpdateCommentRequestDTO
    {
        [Required]
        [MinLength(5, ErrorMessage = "El título debe tener 5 caracteres.")]
        [MaxLength(125, ErrorMessage = "El título no puede tener más de 125 caracteres" )]
        public string Titulo { get; set; } = string.Empty; // Título del comentario

        [Required]
        [MinLength(5, ErrorMessage = "El Contenido debe tener 5 caracteres.")]
        [MaxLength(255, ErrorMessage = "El Contenido no puede tener más de 255 caracteres" )]
        public string Contenido { get; set; } = string.Empty; // Contenido del comentario
    }
}