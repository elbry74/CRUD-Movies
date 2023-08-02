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
    public class GenresController : ControllerBase
    {
        private readonly IGenresServices _GenresServices;

        public GenresController(IGenresServices genresServices)
        {
  
            _GenresServices = genresServices;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var geners = await _GenresServices.GetAll();
            return Ok(geners);
        }



        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateGenreDto dto)
        {
            var genre = new Genre { Name = dto.Name };
            await _GenresServices.Add(genre);
            return Ok(genre);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(byte id , [FromBody] CreateGenreDto dto)
        {
            var genre = await _GenresServices.getById(id);

            if (genre == null)
            {
              return NotFound($"no genre was found with id : {id}");
            }
            genre.Name = dto.Name;
            _GenresServices.Update(genre);
            return Ok(genre);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var genre = await _GenresServices.getById(id);

            if (genre == null)
            {
                return NotFound($"no genre was found with id : {id}");
            }

            _GenresServices.Delete(genre);
            return Ok(genre);
        }
    }
}
