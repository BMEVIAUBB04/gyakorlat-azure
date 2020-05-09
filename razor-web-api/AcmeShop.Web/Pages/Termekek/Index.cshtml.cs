using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AcmeShop.Data;

namespace AcmeShop.Web.Pages.Termekek
{
    public class IndexModel : PageModel
    {
        private readonly AcmeShop.Data.AcmeShopContext _context;

        public IndexModel(AcmeShop.Data.AcmeShopContext context)
        {
            _context = context;
        }

        public IList<Termek> Termek { get;set; }

        public async Task OnGetAsync()
        {
            Termek = await _context.Termek
                .Include(t => t.Afa)
                .Include(t => t.Kategoria).ToListAsync();
        }
    }
}
