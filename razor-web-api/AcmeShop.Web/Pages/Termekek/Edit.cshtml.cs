using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcmeShop.Data;

namespace AcmeShop.Web.Pages.Termekek
{
    public class EditModel : PageModel
    {
        private readonly AcmeShop.Data.AcmeShopContext _context;

        public EditModel(AcmeShop.Data.AcmeShopContext context)
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
           ViewData["AfaId"] = new SelectList(_context.Afa, "Id", "Id");
           ViewData["KategoriaId"] = new SelectList(_context.Kategoria, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Termek).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TermekExists(Termek.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TermekExists(int id)
        {
            return _context.Termek.Any(e => e.Id == id);
        }
    }
}
