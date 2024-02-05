using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AGH_movie_rent.Data;
using AGH_movie_rent.Models;
using Microsoft.AspNetCore.Authorization;
using AGH_movie_rent.Services;
using AGH_movie_rent.Interfaces;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace AGH_movie_rent.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AGH_movie_rentContext _context;
        private readonly IRentalService _movieRentalService;
        private readonly IMovieService _movieService;
       

        public MoviesController( AGH_movie_rentContext context, IRentalService rentalService , IMovieService movieService )
        {
            _context = context;
            _movieRentalService = rentalService;
            _movieService = movieService;

        }


        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movie.ToListAsync());
        }

        public Task<List<Movie>> GetMovies()
        {
            var list = _context.Movie.ToListAsync();
            return list;
        }

        public async Task<Movie> GetMovie(int? id)
        {
            var movie = await _movieService.GetMovie(id); 
            return movie;
        }


        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }


        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,Title,Description,ReleaseYear,PricePerDay,Producer")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }



        // POST: Movies/Rent/5
        [Authorize]
        public async Task<IActionResult> Rent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }
           
            string currentUserId = User.Identity.GetUserId();
            User currentUser =  _context.User.FirstOrDefault(x => x.Email == currentUserId);


            var movieRental = new MovieRental();
            movieRental.MovieId = movie.MovieId.ToString();
            movieRental.MovieName = movie.Title;

            movieRental.UserId = currentUser.UserId.ToString();
            movieRental.UserName = currentUser.Name + " " + currentUser.Surname;

            movieRental.StartDate = DateTime.UtcNow;
            movieRental.Status = "Not paid";

            var newRental = await _movieRentalService.Create(movieRental);

          //  _context.Add(movieRental);
           // await _context.SaveChangesAsync();

            return RedirectToAction("Index"); 
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,Title,Description,ReleaseYear,PricePerDay,Producer")] Movie movie)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
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
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.MovieId == id);
        }
    }
}
