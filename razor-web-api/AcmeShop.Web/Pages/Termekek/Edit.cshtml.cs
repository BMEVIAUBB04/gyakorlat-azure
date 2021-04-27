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
        public List<SelectListItem> AfaKulcsok { get; private set; }
        public List<SelectListItem> Kategoriak { get; private set; }

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
            AfaKulcsok = _context.Afa.Select(a => new SelectListItem(a.Kulcs.ToString(), a.Id.ToString())).ToList();
            Kategoriak = _context.Kategoria.Select(k => new SelectListItem(k.Nev, k.Id.ToString())).ToList();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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
