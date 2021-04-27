using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AcmeShop.Data;

namespace AcmeShop.Web.Pages.Termekek
{
    public class CreateModel : PageModel
    {
        private readonly AcmeShop.Data.AcmeShopContext _context;

        public CreateModel(AcmeShop.Data.AcmeShopContext context)
        {
            _context = context;
        }

        public List<SelectListItem> AfaKulcsok { get; private set; }
        public List<SelectListItem> Kategoriak { get; private set; }

        public IActionResult OnGet()
        {
            AfaKulcsok = _context.Afa.Select(a => new SelectListItem(a.Kulcs.ToString(), a.Id.ToString())).ToList();
            Kategoriak = _context.Kategoria.Select(k => new SelectListItem(k.Nev, k.Id.ToString())).ToList();
            return Page();
        }

        [BindProperty]
        public Termek Termek { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Termek.Add(Termek);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
