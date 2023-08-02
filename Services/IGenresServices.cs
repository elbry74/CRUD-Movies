using MoviesWebApi.Models;

namespace MoviesWebApi.Services
{
    public interface IGenresServices
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> getById(byte id);
        Task<Genre> Add(Genre genre);
        Genre Update(Genre genre);
        Genre Delete(Genre genre);
        Task <bool> IsValidGenre (byte id);

    }
}
