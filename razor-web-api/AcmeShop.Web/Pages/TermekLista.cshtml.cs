using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AcmeShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AcmeShop.Web.Pages.Shared
{
    public class TermekListaModel : PageModel
    {
        private readonly AcmeShopContext _dbContext;

        public TermekListaModel(AcmeShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Termek> Termekek { get; private set; }

        public Kategoria Kategoria { get; private set; }

        public async Task<IActionResult> OnGetAsync([FromRoute]int kategoriaId)
        {
            var osszesKategoria = await _dbContext.Kategoria.ToDictionaryAsync(k => k.Id);
            if (!osszesKategoria.TryGetValue(kategoriaId, out var kategoria))
                return NotFound();

            Kategoria = kategoria;

            var kategoriaIdk = new HashSet<int>();
            var kategoriaSor = new Queue<Kategoria>(new[] { Kategoria });
            while (kategoriaSor.Count > 0)
            {
                var kat = kategoriaSor.Dequeue();
                if (!kategoriaIdk.Add(kat.Id))
                    return StatusCode((int)HttpStatusCode.InternalServerError, "Kör volt a kategóriafában!");
                foreach (var gyerek in kat.GyerekKategoriak ?? Enumerable.Empty<Kategoria>())
                    kategoriaSor.Enqueue(gyerek);
            }

            Termekek = await _dbContext.Termek.Include(t => t.Afa).Where(t => t.KategoriaId != null && kategoriaIdk.Contains(t.KategoriaId.Value)).ToListAsync();
            return Page();
        }
    }
}