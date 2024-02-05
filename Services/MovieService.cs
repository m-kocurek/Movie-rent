using AGH_movie_rent.Data;
using AGH_movie_rent.Interfaces;
using AGH_movie_rent.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AGH_movie_rent.Services
{
    public class MovieService : IMovieService
    {
        private readonly AGH_movie_rentContext _context;

        public MovieService(AGH_movie_rentContext context)
        {
            _context = context;
        }


        public async Task<Movie> GetMovie(int? id)
        {
            var movie = await  _context.Movie
            .FirstOrDefaultAsync(m => m.MovieId == id);
            
            return movie;
        }


    }
}
