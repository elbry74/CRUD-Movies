using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesWebApi.Dtos;
using MoviesWebApi.Models;
using MoviesWebApi.Services;

namespace MoviesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesServices _MoviesServices;
        private readonly IGenresServices _GenresServices;


        private new List<string> _allowedExtenstions = new List<string>() { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576;


        public MoviesController(IMoviesServices moviesServices, IGenresServices genresServices)
        {
            _MoviesServices = moviesServices;
            _GenresServices = genresServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _MoviesServices.GetAll();
            return Ok(movies);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var movie = await _MoviesServices.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }


        [HttpGet("GetByGenreId")]
        public async Task<IActionResult> GetByGenreIdAsync(byte genreId)
        {
            var movie = await _MoviesServices.GetAll(genreId);

            return Ok(movie);
        }



        [HttpPost]
        public async Task<IActionResult> CreateAsync( [FromForm] MovieDto dto)
        {

            if (dto.Poster == null)
            {
                return BadRequest("Poster is required");
            }

            if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
            {
                return BadRequest("only .png and .jpg images are allowed !");
            }

            if(dto.Poster.Length > _maxAllowedPosterSize)
            {
                return BadRequest("max allwed size for poster  is 1MB !");
            }

            var isValidGenre = await _GenresServices.IsValidGenre(dto.GenreId);

            if(!isValidGenre)
            {
                return BadRequest("Invalid Genre Id");
            }

            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);

            var movie = new movie
            { 
                GenreId = dto.GenreId,
                Title = dto.Title,
                Poster = dataStream.ToArray(),
                Rate = dto.Rate,
                StoryLine = dto.StoryLine,
                Year = dto.Year
            
            };

            _MoviesServices.Add(movie);
            return Ok(movie);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id , [FromForm] MovieDto dto)
        {
            var movie = await _MoviesServices.GetById(id);

            if (movie == null)
            {
                return NotFound($"no movie was found with id : {id}");
            }

            var isValidGenre = await _GenresServices.IsValidGenre(dto.GenreId);

            if (!isValidGenre)
            {
                return BadRequest("Invalid Genre Id");
            }

            if (dto.Poster != null )
            {
                if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                {
                    return BadRequest("only .png and .jpg images are allowed !");
                }

                if (dto.Poster.Length > _maxAllowedPosterSize)
                {
                    return BadRequest("max allwed size for poster  is 1MB !");
                }

                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);
                movie.Poster = dataStream.ToArray();
            }

            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.GenreId = dto.GenreId;
            movie.StoryLine = dto.StoryLine;
            movie.Rate = dto.Rate;

            _MoviesServices.Update(movie);  
            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _MoviesServices.GetById(id);

            if (movie == null)
            {
                return NotFound($"no movie was found with id : {id}");
            }

           _MoviesServices.Delete(movie);
            return Ok(movie);
        }

    }
}
