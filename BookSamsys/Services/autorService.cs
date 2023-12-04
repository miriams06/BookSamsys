using BookSamsys.Models;
using BookSamsys.Repository;

namespace BookSamsys.Services;
public class AutorService : IAutorService
{
    private readonly IAutorRepository _autorRepository;

    public AutorService(IAutorRepository autorRepository)
    {
        _autorRepository = autorRepository;
    }

    public async Task<IEnumerable<autor>> ObterTodosAutores()
    {
        return await _autorRepository.ObterTodos();
    }

    public async Task<autor> ObterAutorPorId(int id)
    {
        return await _autorRepository.ObterPorId(id);
    }

    public async Task AdicionarAutor(autor autor)
    {
        await _autorRepository.AdicionarAutor(autor);
    }

    public async Task AtualizarAutor(autor autor)
    {
        await _autorRepository.AtualizarAutor(autor);
    }

    public async Task RemoverAutor(int id)
    {
        await _autorRepository.RemoverAutor(id);
    }
}
