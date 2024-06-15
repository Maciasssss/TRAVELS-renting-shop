using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TRAVELS.Iservices;
using TRAVELS.Models;
using TRAVELS.ViewModels;
namespace TRAVELS.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ITravelService _travelService;
        private readonly IUserService _userService;
        private readonly IValidator<ReservationViewModel> _validator;
        private readonly IMapper _mapper;
        public ReservationsController(
            IReservationService reservationService,
            ITravelService travelService,
            IUserService userService,
            IValidator<ReservationViewModel> validator,
             IMapper mapper) 

        {
            _reservationService = reservationService;
            _travelService = travelService;
            _userService = userService;
            _validator = validator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> ShowReservations()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            var reservationViewModels = _mapper.Map<List<ReservationViewModel>>(reservations); 

            return View(reservationViewModels);
        }
        [HttpGet]
        public async Task<IActionResult> Reservations()
        {
            var travels = await _travelService.GetAllAsync();
            ViewBag.TravelDestination = new SelectList(travels, "Destination", "Destination");
            ViewBag.Users = new SelectList(await _userService.GetAllAsync(), "UserName", "UserName");

            return View(new ReservationViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Reservations(ReservationViewModel viewModel)
        {
            var validationResult = _validator.Validate(viewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                var travels = await _travelService.GetAllAsync();
                ViewBag.TravelDestination = new SelectList(travels, "TravelId", "Destination", viewModel.TravelDestination);
                var users = await _userService.GetAllAsync();
                ViewBag.Users = new SelectList(users, "UserName", "UserName", viewModel.Username);

                return View(viewModel);
            }

            var travel = await _travelService.GetByDestinationAsync(viewModel.TravelDestination);
            if (travel == null)
            {
                ModelState.AddModelError("", "Selected destination does not exist.");
                var travels = await _travelService.GetAllAsync();
                ViewBag.TravelDestination = new SelectList(travels, "TravelId", "Destination", viewModel.TravelDestination);
                var users = await _userService.GetAllAsync();
                ViewBag.Users = new SelectList(users, "UserName", "UserName", viewModel.Username);
                return View(viewModel);
            }

            var reservation = _mapper.Map<Reservation>(viewModel);
            reservation.Travel = travel;
            await _reservationService.AddReservationAsync(reservation);
            return RedirectToAction("ShowReservations");
        }






        [HttpGet]
        public async Task<ActionResult> UpdateReservation(int reservationId)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(reservationId);
            if (reservation == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<ReservationViewModel>(reservation); 

            ViewBag.TravelId = new SelectList(await _travelService.GetAllAsync(), "TravelId", "Destination", reservation.Travel.TravelId);
            ViewBag.UserId = new SelectList(await _userService.GetAllAsync(), "UserId", "UserName", reservation.User.UserName);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateReservationAsync(ReservationViewModel viewModel)
        {
            var validationResult = _validator.Validate(viewModel);
            if (!validationResult.IsValid)
            {
                ViewBag.TravelID = new SelectList(await _travelService.GetAllAsync(), "Destination", "Destination", viewModel.TravelDestination);
                ViewBag.UserId = new SelectList(await _userService.GetAllAsync(), "UserId", "UserName", viewModel.Username);
                return View(viewModel);
            }

            var reservation = _mapper.Map<Reservation>(viewModel);
            await _reservationService.UpdateReservationAsync(reservation);
            return RedirectToAction("ShowReservations");
        }


        [HttpGet]
        public async Task<ActionResult> DeleteReservations(int reservationId)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(reservationId);
            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(int reservationId)
        {
            await _reservationService.DeleteReservationAsync(reservationId);
            return RedirectToAction("ShowReservations");
        }
    }
}
