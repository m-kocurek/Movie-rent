using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AGH_movie_rent.Models;

namespace AGH_movie_rent.Data
{
    public class AGH_movie_rentContext : DbContext
    {
        public AGH_movie_rentContext (DbContextOptions<AGH_movie_rentContext> options)
            : base(options)
        {
        }

        public DbSet<AGH_movie_rent.Models.User> User { get; set; }

        public DbSet<AGH_movie_rent.Models.Movie> Movie { get; set; }

        public DbSet<AGH_movie_rent.Models.MovieRental> MovieRental { get; set; }
    }
}
