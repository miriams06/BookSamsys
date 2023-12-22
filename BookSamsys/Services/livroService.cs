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

namespace BookSamsys.Services;

public class LivroService : ControllerBase, ILivroService
{
    private readonly AppDBContext _context; ILivroRepository _livroRepository;

    public LivroService(ILivroRepository livroRepository)
    {
        _livroRepository = livroRepository;
    }


    public async Task<IEnumerable<livro>> ObterTodosLivros()
    {
        var livros = _livroRepository.ObterTodos();

        if (livros == null)
        {
            NoContent();
        }

        return await livros;
    }


    public async Task ObterLivroPorIsbn(string isbn)
    {
        var livro = _livroRepository.ObterPorIsbn(isbn);


        if (livro == null)
        {
            NotFound();
        }

        Ok(livro);
    }


    public async Task AdicionarLivro(livro addLivro)
    {
        var livroTask = _livroRepository.ObterPorIsbn(addLivro.isbn);
        await livroTask;

        var livro = livroTask.Result;

        if (livro != null && livro.isbn == addLivro.isbn)
        {
            BadRequest("O ISBN já está em uso por outro livro.");
        }

        if (addLivro.isbn.Length != 13 || addLivro.preco < 0 || addLivro == null)
        {
            BadRequest(new { message = "Ocorreu um erro ao adicionar as informações" });
        }


        _livroRepository.AdicionarLivro(addLivro);

        Ok("Livro inserido com sucesso.");


    }


    public async Task AtualizarLivro(livro editarLivro)
    {
        var livro = _livroRepository.ObterPorIsbn(editarLivro.isbn);

        if (livro == null)
        {
            NotFound();
        }

        _livroRepository.AtualizarLivro(editarLivro);

        Ok("Livro atualizado com sucesso.");

    }

    public async Task RemoverLivro(string isbn)
    {
        var livro = _livroRepository.ObterPorIsbn(isbn);

        if (livro == null)
        {
            NotFound();
        }

        _livroRepository.RemoverLivro(isbn);

        Ok("Livro removido com sucesso.");

    }

}
