using TRAVELS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TRAVELS.Controllers
{
    public class TravelController : Controller
    {
        private readonly TravelDBcontext _context;

        public TravelController(TravelDBcontext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Wycieczki()
        {
            var travels = await _context.Travels.ToListAsync();
            return View("~/Views/Home/Wycieczki.cshtml", travels);
        }

    }
}
