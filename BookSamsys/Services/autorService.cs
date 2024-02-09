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
public class AutorService : ControllerBase,  IAutorService
{
    private readonly AppDBContext _context; IAutorRepository _autorRepository;

    public AutorService(IAutorRepository autorRepository)
    {
        _autorRepository = autorRepository;
    }

    public async Task<IEnumerable<autor>> ObterTodosAutores()
    {
        var autores = await _autorRepository.ObterTodos();

        if (autores == null)
        {
            NotFound();
        }

        return autores;
    }

    public async Task<ActionResult<autor>> ObterAutorPorId(int id)
    {
        var autor = await _autorRepository.ObterPorId(id);

        if (autor == null)
        {
            throw new InvalidOperationException($"Autor com id {id} não encontrado.");
        }

        return Ok(autor);
    }

    public async Task<ActionResult<autor>> AdicionarAutor(autor addAutor)
    {
        var autorTask = _autorRepository.ObterPorId(addAutor.idAutor);

        if (autorTask != null && autorTask.Id == addAutor.idAutor)
        {
            return BadRequest($"O autor com id {addAutor.idAutor} já existe.");
        }

        if (autorTask == null)
        {
            await _autorRepository.AdicionarAutor(addAutor);
            return Ok("Autor inserido com sucesso.");
        }

        return BadRequest($"Erro desconhecido ao adicionar o autor.");

    }

    public async Task<ActionResult<autor>> AtualizarAutor(autor editarAutor)
    {
        var autorObtido = await _autorRepository.ObterPorId(editarAutor.idAutor);

        if (autorObtido == null)
        {
            return NotFound($"Autor com o id {editarAutor.idAutor} não encontrado.");
        }

        await _autorRepository.AtualizarAutor(editarAutor);
        return Ok("Autor atualizado com sucesso.");
    }

    public async Task<ActionResult<autor>> RemoverAutor(int id)
    {
        var autor = _autorRepository.ObterPorId(id);

        if (autor == null)
        {
            return NotFound();
        }

        await _autorRepository.RemoverAutor(id);

        return Ok("Autor removido com sucesso.");
    }
}