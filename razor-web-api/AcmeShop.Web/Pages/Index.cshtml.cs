using AcmeShop.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

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
