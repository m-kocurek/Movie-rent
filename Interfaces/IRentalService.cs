using AGH_movie_rent.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AGH_movie_rent.Interfaces
{
    public interface IRentalService
    {
        
        Task<List<MovieRental>> ListAll();

        Task<List<MovieRental>> List(int? id);

        Task<MovieRental> Details(int? id);

        Task<MovieRental> Create(MovieRental movieRental);


        Task<MovieRental> Edit(int? id);

        void Edit(int? id, MovieRental movieRental);

        Task<MovieRental> Delete(int? id);

        Task<MovieRental> DeleteConfirmed(int id);

        bool MovieRentalExists(int id);

        Task UpdateMovieRental(MovieRental movieRental);

    }
}
