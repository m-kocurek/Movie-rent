using AGH_movie_rent.Models;
using System.Threading.Tasks;

namespace AGH_movie_rent.Interfaces
{
    public interface IMovieService
    {

        public Task<Movie> GetMovie(int? id);

    }
}
