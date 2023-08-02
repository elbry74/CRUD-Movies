using MoviesWebApi.Models;

namespace MoviesWebApi.Services
{
    public interface IMoviesServices
    {
        Task<IEnumerable<movie>> GetAll(byte genreId = 0 );
        Task<movie> GetById(int id);
        Task<movie> Add(movie movie);
        movie Update(movie movie);
        movie Delete(movie movie);


    }
}
