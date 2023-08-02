using Microsoft.EntityFrameworkCore;
using MoviesWebApi.Models;

namespace MoviesWebApi.Services
{
    public class GenresServices : IGenresServices
    {

        private readonly ApplicationDbContext _context;

        public GenresServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Genre> Add(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            _context.SaveChanges();
            return genre;
        }

        public Genre Delete(Genre genre)
        {
            _context.Remove(genre);
            _context.SaveChanges();
            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            var genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();

            return genres;
        }

        public async Task<Genre> getById(byte id)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
            return genre;
        }

        public Task<bool> IsValidGenre(byte id)
        {
            return _context.Genres.AnyAsync(g => g.Id == id);
        }

        public Genre Update(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChanges();
            return genre;
        }
    }
}
