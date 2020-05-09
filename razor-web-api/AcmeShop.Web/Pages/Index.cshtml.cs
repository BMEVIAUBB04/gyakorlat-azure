using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AcmeShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AcmeShop.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AcmeShopContext _dbContext;

        public IndexModel(AcmeShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ILookup<int?, Kategoria> Kategoriak { get; private set; }

        public void OnGet()
        {
            Kategoriak = _dbContext.Kategoria.ToLookup(k => k.SzuloKategoriaId);
        }
    }
}
