using BookSamsys.Models;
using BookSamsys.Repository;

namespace BookSamsys.Services;

public interface IAutorService
{
    Task<IEnumerable<autor>> ObterTodosAutores();
    Task<autor> ObterAutorPorId(int id);
    Task AdicionarAutor(autor autor);
    Task AtualizarAutor(autor autor);
    Task RemoverAutor(int id);
}
