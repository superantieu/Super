using Microsoft.AspNetCore.Mvc;
using Super.Models;

namespace Super.Controllers
{
    public class HangController : Controller
    {
        private readonly QlbhContext _context;
        public HangController(QlbhContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var item = _context.Hangs.ToList();
            return View(item);
        }
    }
}
