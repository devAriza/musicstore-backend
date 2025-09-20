using System.Net;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Repositories;

namespace MusicStore.Api.Controllers;

[ApiController]
[Route("api/genres")]
public class GenresController : ControllerBase
{

    private readonly IGenreRepository repository;
    private readonly ILogger<GenresController> logger;

    public GenresController(IGenreRepository repository, ILogger<GenresController> logger)
    {

        this.repository = repository;
        this.logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {

        var response = new BaseResponseGeneric<ICollection<GenreResponseDto>>();

        try
        {
            response.Data = await repository.GetAsync();
            response.Success = true;
            logger.LogInformation($"Obteniendo todos los music genre");
            return Ok(response);

        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al obtener the information.";
            logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            return BadRequest(response);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {   

        var response = new BaseResponseGeneric<GenreResponseDto>();

        try
        {
            response.Data = await repository.GetAsync(id);
            response.Success = true;
            logger.LogInformation($"Obteniendo el music genre con id {id}");
            return response.Data is not null ? Ok(response) : NotFound(response);
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al obtener la información del music genre.";
            logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            return BadRequest(response);
        }

    }

    [HttpPost]
    public async Task<IActionResult> Post(GenreRequestDto genre)
    {
        var response = new BaseResponseGeneric<int>();
        try
        {
            var genreId = await repository.AddAsync(genre);
            response.Data = genreId;
            response.Success = true;
            logger.LogInformation($"Creando un nuevo music genre con id {genreId}");
            return StatusCode((int)HttpStatusCode.Created, response);
        }
        catch(Exception ex)
        {
            response.ErrorMessage = "Error al crear el music genre.";
            logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            return BadRequest(response);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, GenreRequestDto genre)
    {
        var response = new BaseResponse();
        try
        {
            await repository.UpdateAsync(id, genre);
            response.Success = true;
            logger.LogInformation($"Actualizando el music genre con id {id}");
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al actualizar el music genre.";
            logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            return BadRequest(response);
        }

    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = new BaseResponse();
        try
        {
            await repository.DeleteAsync(id);
            response.Success = true;
            logger.LogInformation($"Eliminando el music genre con id {id}");
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al eliminar el music genre.";
            logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            return BadRequest(response);
        }
    }

}

