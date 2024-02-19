using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookSamsys.Models;
using BookSamsys.Models.DTOs;
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
    public async Task<MessagingHelper<List<livroDTO>>> GetLivros()
    {
        return await _livroService.ObterTodosLivros();
    }


    [HttpGet]
    [Route("livro/{isbn}")]
    public async Task<MessagingHelper<livroDTO>> GetLivro(string isbn)
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
    public async Task<MessagingHelper<livroDTO>> AddLivro(livroDTO livroDto)
    {
        return await _livroService.AdicionarLivro(livroDto);
    }

    [HttpPatch]
    [Route("atualizarLivro")]
    public async Task<MessagingHelper<livroDTO>> UpdateLivro(livroDTO livroDto)
    {
        return await _livroService.AtualizarLivro(livroDto);
    }

    [HttpDelete]
    [Route("apagarLivro/{isbn}")]
    public async Task<MessagingHelper<livroDTO>> DeleteLivro(string isbn)
    {
        return await _livroService.RemoverLivro(isbn);
    }
}
