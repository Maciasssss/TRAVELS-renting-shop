using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TRAVELS.Models;
using System.Diagnostics;

namespace TRAVELS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly List<Travel> _travels = new List<Travel>
        {
            new Travel
            {
                TravelId = 1,
                Destination = "Paryż",
                DepartureDate = new DateTime(2024, 3, 15),
                ReturnDate = new DateTime(2024, 3, 20),
                Price = 1500.00m,
                Description = "Zwiedzanie Paryża przez 5 dni"
            },
            new Travel
            {
                TravelId = 2,
                Destination = "Rzym",
                DepartureDate = new DateTime(2024, 4, 10),
                ReturnDate = new DateTime(2024, 4, 17),
                Price = 2000.00m,
                Description = "Wycieczka do Rzymu na 7 dni"
            },

        };
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View(_travels);
        }

        public IActionResult Details(int id)
        {
            var travel = _travels.FirstOrDefault(t => t.TravelId == id);
            if (travel == null)
            {
                return NotFound();
            }
            return View(travel);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
