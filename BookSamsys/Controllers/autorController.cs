using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookSamsys.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSamsys.Services;
using BookSamsys.Repository;

namespace BookSamsys.Controllers;

[Route("api/autores")]
[ApiController]
public class AutorController : ControllerBase
{
    private readonly IAutorService _autorService;

    public AutorController(IAutorService autorService)
    {
        _autorService = autorService;
    }

    [HttpGet]
    [Route("autores")]
    public async Task<IEnumerable<autor>> GetAutores()
    {
        return await _autorService.ObterTodosAutores();
    }


    [HttpGet]
    [Route("autores/{idAutor}")]
    public async Task<ActionResult<autor>> GetAutor(int idAutor)
    {
        return await _autorService.ObterAutorPorId(idAutor);

    }

    [HttpPost]
    [Route("criarAutor")]
    public async Task PostAutor(autor autor) => await _autorService.AdicionarAutor(autor);

    [HttpPatch]
    [Route("atualizarAutor/{idAutor}")]
    public async Task UpdateAutor(autor idAutor) => await _autorService.AtualizarAutor(idAutor);

    [HttpDelete]
    [Route("apagarAutor/{idAutor}")]
    public async Task DeleteAutor(int idAutor) => await _autorService.RemoverAutor(idAutor);
}