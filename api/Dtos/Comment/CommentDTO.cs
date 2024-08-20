using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CommentDTO
    {
        public int Id { get; set; } // Identificador único del comentario
        public string Titulo { get; set; } = string.Empty; // Título del comentario
        public string Contenido { get; set; } = string.Empty; // Contenido del comentario
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public int? AccionId { get; set; } // Identificador de la acción (stock) asociada, puede ser nulo
    }
}