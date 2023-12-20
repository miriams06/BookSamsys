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
    private readonly ILivroRepository _livroRepository;

    public LivroService(ILivroRepository livroRepository)
    {
        _livroRepository = livroRepository;
    }


    public async Task<IEnumerable<livro>> ObterTodosLivros()
    {
        var livro = _livroRepository.ObterTodos();

        if (livro == null)
        {
            NotFound();
        }

        return await livro;
    }


    public async Task ObterLivroPorIsbn(string isbn)
    {
        var livro = _livroRepository.ObterPorIsbn(isbn);

        if (isbn.Length != 13)
        {
            BadRequest(new { message = "O isbn tem de ter 13 caracteres" });
        }


        if (livro == null)
        {
            NotFound();
        }

        Ok(livro);
    }


    public async Task AdicionarLivro(livro livro)
    {
        if (livro.isbn.Length != 13 || livro.preco < 0 || livro == null)
        {
            BadRequest(new { message = "Ocorreu um erro ao adicionar as informações" });
        }

        await _livroRepository.AdicionarLivro(livro);

    }


    public async Task AtualizarLivro(livro isbn)
    {
        var livro = _livroRepository.AtualizarLivro(isbn);

        if (livro == null)
        {
            NotFound();
        }

        Ok(livro);

    }

    public async Task RemoverLivro(string isbn)
    {
        var livro = _livroRepository.RemoverLivro(isbn);

        if (livro == null)
        {
            NotFound();
        }

        Ok(livro);
    }

}
