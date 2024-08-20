using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;

namespace api.Dtos.Stock
{
    public class StockDTO
    {
        public int Id { get; set; }
        public string Simbolo { get; set; } = string.Empty;

        public string NombreCompania {get; set; } = string.Empty;
        public decimal Compra { get; set; }
        public decimal UltimoDividendo { get; set; }

        public string Industria { get; set; } = string.Empty;
        public long CapitalizdeMercado { get; set; }
        public List<CommentDTO> Comentarios {get; set; }
    }
}