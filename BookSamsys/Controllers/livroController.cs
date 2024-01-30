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
    [Route("livro/{isbn}")]
    public async Task<ActionResult<livro>>GetLivro(string isbn)
    {
       return await _livroService.ObterLivroPorIsbn(isbn);
    }

    //[HttpGet]
    //[Route("livros/{ordenacao}")]
    //public async Task GetLivro(string ordenacao, int paginaAtual, int itensPorPagina)
    //{
    //    await _livroService.ListarLivros(ordenacao, paginaAtual, itensPorPagina);
    //}

    [HttpPost]
    [Route("criarLivro")]
    public async Task<ActionResult<livro>> AddLivro(livro livro)
    {
        return await _livroService.AdicionarLivro(livro);
    }

    [HttpPatch]
    [Route("atualizarLivro")]
    public async Task<ActionResult<livro>> UpdateLivro(livro livro)
    {
        return await _livroService.AtualizarLivro(livro);
    }

    [HttpDelete]
    [Route("apagarLivro/{isbn}")]
    public async Task<ActionResult<livro>> DeleteLivro(string isbn)
    {
        return await _livroService.RemoverLivro(isbn);
    }
}
