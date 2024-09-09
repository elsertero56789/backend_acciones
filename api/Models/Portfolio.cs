using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public string AppuserId { get; set; }
        public int StockId { get; set; }

        public ApppUser AppUser { get; set; }

        public Stock Stock { get; set; }



    }
}