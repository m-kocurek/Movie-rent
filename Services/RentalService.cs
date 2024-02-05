using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AGH_movie_rent.Data;
using AGH_movie_rent.Models;
using Microsoft.AspNetCore.Authorization;
using AGH_movie_rent.Interfaces;
using System.Web.Mvc;

namespace AGH_movie_rent.Services
{
    public class RentalService: IRentalService
        {

        private readonly AGH_movie_rentContext _context;

        public RentalService(AGH_movie_rentContext context) { 
         _context = context;
        }


        // GET: MovieRentals
        public async Task<List<MovieRental>> ListAll()
        {
            var list =   await _context.MovieRental.ToListAsync();
            return list;
        }


        // GET: MovieRentals/2
        public async Task<List<MovieRental>> List(int? id)
        {
           
            var list = await  _context.MovieRental.Where(a => a.UserId == id.ToString()).ToListAsync();
            
            return list;
        }

        // GET: MovieRentals/Details/5
        public async Task<MovieRental> Details(int? id)
        {
            var movieRental = await _context.MovieRental
                .FirstOrDefaultAsync(m => m.MovieRentalId == id);
            if (movieRental == null)
            {
                return null;
            }

            return movieRental;
        }

        // POST: MovieRentals/Create
        
        public async Task<MovieRental> Create( MovieRental movieRental)
        { 
           _context.Add(movieRental);
           await _context.SaveChangesAsync();
           return movieRental;
        }

        // GET: MovieRentals/Edit/5
        public async Task<MovieRental> Edit(int? id)
        {
            var movieRental = await _context.MovieRental.FindAsync(id);
            return movieRental;
        }

        // POST: MovieRentals/Edit/5
      
        public async void Edit(int? id,  MovieRental movieRental)
        {
            if (id != null) {
                _context.Update(movieRental);
                await _context.SaveChangesAsync();
                
            }
        }

        // GET: MovieRentals/Delete/5
        public async Task<MovieRental> Delete(int? id)
        {

            var movieRental = await _context.MovieRental
                .FirstOrDefaultAsync(m => m.MovieRentalId == id);
            if (movieRental == null)
            {
                throw NotImplementedException();
            }

            return movieRental;
        }

        private Exception NotImplementedException()
        {
            throw new NotImplementedException();
        }

        // POST: MovieRentals/Delete/5

        public async Task<MovieRental> DeleteConfirmed(int id)
        {
            var movieRental = await _context.MovieRental.FindAsync(id);
            _context.MovieRental.Remove(movieRental);
            await _context.SaveChangesAsync();
            return movieRental ;
        }

        public bool MovieRentalExists(int id)
        {
            return _context.MovieRental.Any(e => e.MovieRentalId == id);
        }


        public async Task UpdateMovieRental(MovieRental movieRental) 
        {
            _context.MovieRental.Update(movieRental);
            await _context.SaveChangesAsync();
            return ;
        }


    }
}