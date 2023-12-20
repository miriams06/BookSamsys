using BookSamsys.Models;
using BookSamsys.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using NuGet.LibraryModel;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Azure.Messaging;

namespace BookSamsys.Services;
public class AutorService : ControllerBase, IAutorService
{
    private readonly IAutorRepository _autorRepository;

    public AutorService(IAutorRepository autorRepository)
    {
        _autorRepository = autorRepository;
    }

    public async Task<IEnumerable<autor>> ObterTodosAutores()
    {
        var autor = await _autorRepository.ObterTodos();

        if (autor == null)
        {
            NotFound();
        }

        return autor;
    }

    public async Task ObterAutorPorId(int id)
    {
        var autor = _autorRepository.ObterPorId(id);

        if (autor == null)
        {
            NotFound();
        }

        Ok(autor);
    }

    public async Task AdicionarAutor(autor autor)
    {
        await _autorRepository.AdicionarAutor(autor);
    }

    public async Task AtualizarAutor(autor id)
    {
        var autor = _autorRepository.AtualizarAutor(id);

        if (autor == null)
        {
            NotFound();
        }

        Ok(autor);
    }

    public async Task RemoverAutor(int id)
    {
        var autor = _autorRepository.RemoverAutor(id);

        if (autor == null)
        {
            NotFound();
        }

        Ok(autor);
    }
}