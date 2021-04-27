using AcmeShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeShop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermekController : ControllerBase
    {
        private readonly AcmeShopContext _dbContext;

        public TermekController(AcmeShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Termek>>> GetTermekek(int? termekId = null, double? minimumNettoAr = null, double? maximumNettoAr = null)
        {
            IQueryable<Termek> results = _dbContext.Termek;

            if (termekId != null)
                results = results.Where(t => t.Id == termekId);

            if (minimumNettoAr != null)
                results = results.Where(t => t.NettoAr >= minimumNettoAr);

            if (maximumNettoAr != null)
                results = results.Where(t => t.NettoAr <= maximumNettoAr);

            return await results.ToListAsync();
        }
    }
}
