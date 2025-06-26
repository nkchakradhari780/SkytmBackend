using Microsoft.AspNetCore.Mvc;
using SkytmBackend.Data;

namespace SkytmBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }
    }
}
