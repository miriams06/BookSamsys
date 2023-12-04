using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookSamsys.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSamsys.Services;
using BookSamsys.Repository;

namespace BookSamsys.Controllers;

[Route("api/livros")]
[ApiController]
public class LivroController : ControllerBase
{
    private readonly ILivroService _livroService;

    public LivroController(ILivroService livroService)
    {
        _livroService = livroService;
    }


    [HttpGet]
    [Route("livros")]
    public async Task<IEnumerable<livro>> GetLivros()
    {
        return await _livroService.ObterTodosLivros();
    }


    [HttpGet]
    [Route("livros/{isbn}")]
    public async Task GetLivro(string isbn)
    {
        await _livroService.ObterLivroPorIsbn(isbn);
    }

    [HttpPost]
    [Route("criarLivro")]
    public async Task AddLivro(livro livro)
    {
        await _livroService.AdicionarLivro(livro);
    }

    [HttpPatch]
    [Route("atualizarLivro/{isbn}")]
    public async Task UpdateLivro(livro isbn)
    {
        await _livroService.AtualizarLivro(isbn);
    }

    [HttpDelete]
    [Route("apagarLivro/{isbn}")]
    public async Task DeleteLivro(string isbn)
    {
        await _livroService.RemoverLivro(isbn);
    }
}
