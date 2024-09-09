using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class ApppUser : IdentityUser
    {
        
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

    }
}