using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AGH_movie_rent.Data;
using AGH_movie_rent.Models;
using AGH_movie_rent.Services;
using AGH_movie_rent.Interfaces;

namespace AGH_movie_rent.Controllers
{
    public class MovieRentalsController : Controller
    {
        private readonly AGH_movie_rentContext _context;
        private readonly IRentalService _movieRentalService;
        private readonly IMovieService _movieService;   
        

        public MovieRentalsController(AGH_movie_rentContext context, IRentalService rentalService, IMovieService movieService  )
        {
            _context = context;
            _movieRentalService = rentalService;
            _movieService = movieService;
        }

        // GET: MovieRentals
        public async Task<IActionResult> Index()
        {
            var list = await _movieRentalService.ListAll();
            return View(list);
        }

        // GET: MovieRentals/2
        public async Task<IActionResult> List(int? id)
        {
            var list =  await _movieRentalService.List(id);
            return View(list);
        }

        // GET: MovieRentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieRental = await _movieRentalService.Details(id);
            return View(movieRental);
        }

        // POST: MovieRentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,UserId,MovieId,MovieRentalId,StartDate,EndDate,MovieName,ChargedFee,Status")] MovieRental movieRental)
        {

            if (ModelState.IsValid)
            {
                await _movieRentalService.Create(movieRental);
 
                return RedirectToAction(nameof(Index));
            }
            return View(movieRental);
        }

        // GET: MovieRentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieRental = await _movieRentalService.Edit(id);

            if (movieRental == null)
            {
                return NotFound();
            }
            return View(movieRental);
        }

        // POST: MovieRentals/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieRentalId,StartDate,EndDate")] MovieRental movieRental)
        {
            if (id != movieRental.MovieRentalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                     _movieRentalService.Edit(id, movieRental);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieRentalExists(movieRental.MovieRentalId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movieRental);
        }

        // GET: MovieRentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieRental = await _movieRentalService.Delete(id);

            return View(movieRental);
        }

        // POST: MovieRentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieRental = await _movieRentalService.DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MovieRentalExists(int id)
        {
            return _movieRentalService.MovieRentalExists(id);
        }


        public async Task<RedirectToActionResult> ReturnMovie(int id) {

            var movieRental = await _movieRentalService.Details(id);

            DateTime todaysDate = DateTime.Now;

            var movie =  await _movieService.GetMovie(Int32.Parse( movieRental.MovieId));

            var payment = Int32.Parse(movie.PricePerDay) * (todaysDate - movieRental.StartDate).TotalDays;

            movieRental.EndDate = todaysDate;   
            movieRental.ChargedFee = Convert.ToInt32(payment);

            movieRental.Status = "Paid";

            await _movieRentalService.UpdateMovieRental(movieRental);

            return RedirectToAction("PayFee", "MovieRentals", new { @id = movieRental.MovieRentalId });
        }


        public async Task<IActionResult> PayFee(string id)
        {
            var movieRental = await _movieRentalService.Details(Convert.ToInt32(id));

            ViewBag.Message = movieRental;
            return View();
        }


    }
}
