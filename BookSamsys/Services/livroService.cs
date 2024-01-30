using BookSamsys.Models;
using BookSamsys.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using NuGet.LibraryModel;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Azure.Messaging;
using System.Web;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Threading.Tasks;

namespace BookSamsys.Services;

public class LivroService : ControllerBase, ILivroService
{
    private readonly AppDBContext _context; ILivroRepository _livroRepository;
    private List<livro> livros;
    public LivroService(ILivroRepository livroRepository)
    {
        _livroRepository = livroRepository;
        livros = new List<livro>();
    }

    public async Task<IEnumerable<livro>> ObterTodosLivros()
    {
        var livros = await _livroRepository.ObterTodos();

        if (livros == null || !livros.Any())
        {
            return null;
        }

        return livros;
    }


    public async Task<ActionResult<livro>> ObterLivroPorIsbn(string isbn)
    {
        var livro = await _livroRepository.ObterPorIsbn(isbn);

        if (isbn.Length != 13)
        {
            return BadRequest();
        }

        if (livro == null)
        {
            throw new InvalidOperationException($"Livro com ISBN {isbn} não encontrado.");
        }
       

        return Ok(livro);

    }

    //public async Task<ActionResult<livro>> ListarLivros(string ordenacao, int paginaAtual, int itensPorPagina)
    //{
    //    IEnumerable<livro> livrosOrdenados;
    //    switch (ordenacao.ToLower())
    //    {
    //        case "isbn":
    //            livrosOrdenados = livros.OrderBy(l => l.isbn);
    //            break;
    //        case "nome":
    //            livrosOrdenados = livros.OrderBy(l => l.nome);
    //            break;
    //        case "autor":
    //            livrosOrdenados = livros.OrderBy(l => l.idAutor);
    //            break;
    //        case "preco":
    //            livrosOrdenados = livros.OrderBy(l => l.preco);
    //            break;
    //        default:
    //            livrosOrdenados = livros.OrderBy(l => l.isbn);
    //            break;
    //    }
        
    //    var livrosPaginados = livrosOrdenados
    //        .Skip((paginaAtual - 1) * itensPorPagina)
    //        .Take(itensPorPagina);

    //    Ok(livrosPaginados);
    //}

    public async Task<ActionResult<livro>> AdicionarLivro(livro addLivro)
    {
        var livroTask = await _livroRepository.ObterPorIsbn(addLivro.ISBN);
        //return livroTask;


        //var livro = livroTask.Result;

        if (addLivro != null && addLivro.ISBN == livroTask.ISBN)
        {
            return BadRequest("O ISBN já está em uso por outro livro.");
        }


        if (addLivro.ISBN.Length != 13 || addLivro.Preco < 0 || addLivro == null)
        {
            return BadRequest("Ocorreu um erro ao adicionar as informações.");
        }

        var livroAdd = await _livroRepository.AdicionarLivro(addLivro);
        //return livroAdd;

        return Ok("Livro inserido com sucesso.");

    }


    public async Task<ActionResult<livro>> AtualizarLivro(livro editarLivro)
    {
        //if (editarLivro != null && editarLivro.ISBN != livroObtido.ISBN)
        //{
        //    return BadRequest("O ISBN não existe.");
        //}

        if (editarLivro.ISBN.Length != 13 || editarLivro.Preco < 0 || editarLivro == null)
        {
            return BadRequest("Ocorreu um erro ao adicionar as informações.");
        }

        var livroObtido = await _livroRepository.ObterPorIsbn(editarLivro.ISBN);

        if (livroObtido == null)
        {
            return NotFound($"Livro com o ISBN {editarLivro.ISBN} não encontrado.");
        }

        //livroObtido.livroUpdate(editarLivro.Nome, editarLivro.Preco);

        livroObtido.Nome = editarLivro.Nome;
        livroObtido.Preco = editarLivro.Preco;

        try
        {
            await _livroRepository.AtualizarLivro(editarLivro);
            return Ok("Livro atualizado com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Erro interno ao atualizar o livro.");
        }


        //livroObtido.livroUpdate(livroObtido.ISBN, livroObtido.Nome, livroObtido.IdAutor, livroObtido.Preco);
        //await _livroRepository.AtualizarLivro(livroObtido);

        //return Ok("Livro atualizado com sucesso.");

    }

    public async Task<ActionResult<livro>> RemoverLivro(string isbn)
    {
        var livro = _livroRepository.ObterPorIsbn(isbn);

        if (livro == null)
        {
            return NotFound();
        }

        await _livroRepository.RemoverLivro(isbn);

        return Ok("Livro removido com sucesso.");

    }

}
