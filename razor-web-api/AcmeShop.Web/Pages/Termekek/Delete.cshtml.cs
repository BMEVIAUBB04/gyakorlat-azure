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
    public class DeleteModel : PageModel
    {
        private readonly AcmeShop.Data.AcmeShopContext _context;

        public DeleteModel(AcmeShop.Data.AcmeShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Termek Termek { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Termek = await _context.Termek
                .Include(t => t.Afa)
                .Include(t => t.Kategoria).FirstOrDefaultAsync(m => m.Id == id);

            if (Termek == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Termek = await _context.Termek.FindAsync(id);

            if (Termek != null)
            {
                _context.Termek.Remove(Termek);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
