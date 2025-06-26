using Microsoft.AspNetCore.Mvc;
using SkytmBackend.Data;

namespace SkytmBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : Controller
    {
        private readonly AppDbContext _context;

        public WalletController(AppDbContext context)
        {
            _context = context;
        }
    }
}
