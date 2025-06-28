using Microsoft.AspNetCore.Mvc;
using MusicStore.Repositories;

namespace MusicStore.Api.Controllers;

[ApiController]
[Route("api/genres")]
public class GenresController : ControllerBase
{

    private readonly GenreRepository repository;

    public GenresController(GenreRepository repository)
    {

        this.repository = repository;
        
    }

}

