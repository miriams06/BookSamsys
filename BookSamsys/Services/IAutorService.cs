using BookSamsys.Models;
using BookSamsys.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookSamsys.Services;

public interface IAutorService
{
    Task<IEnumerable<autor>> ObterTodosAutores();
    Task<ActionResult<autor>> ObterAutorPorId(int id);
    Task<ActionResult<autor>> AdicionarAutor(autor autor);
    Task<ActionResult<autor>> AtualizarAutor(autor id);
    Task<ActionResult<autor>> RemoverAutor(int id);
}
