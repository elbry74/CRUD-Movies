using Microsoft.EntityFrameworkCore;
using MoviesWebApi.Models;

namespace MoviesWebApi.Services
{
    public class MoviesServices : IMoviesServices
    {

        private readonly ApplicationDbContext _context;

        public MoviesServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<movie> Add(movie movie)
        {
            await _context.Movies.AddAsync(movie);
            _context.SaveChanges();
            return movie;
        }

        public movie Delete(movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();
            return movie;
        }

        public async Task<IEnumerable<movie>> GetAll(byte genreId = 0)
        {
            var movies = await _context.Movies
                .Where(m => m.GenreId == genreId || genreId == 0)
                .OrderByDescending(m => m.Rate)
                .Include(m => m.Genre)
                .ToListAsync();
            return movies;
        }

        public async Task<movie> GetById(int id)
        {
            var movie = await _context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);
            return movie;
        }

        public movie Update(movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();
            return movie;
        }
    }
}
