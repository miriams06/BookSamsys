using BookSamsys.Models;
using BookSamsys.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookSamsys.Services;

public class LivroService : ILivroService
{
    private readonly ILivroRepository _livroRepository;

    public LivroService(ILivroRepository livroRepository)
    {
        _livroRepository = livroRepository;
    }
    public Task<IEnumerable<livro>> ObterTodosLivros()
    {
        throw new NotImplementedException();
    }

    public Task ObterLivroPorIsbn(string isbn)
    {
        throw new NotImplementedException();
    }

    public Task AdicionarLivro(livro livro)
    {
        throw new NotImplementedException();
    }

    public Task AtualizarLivro(livro livro)
    {
        throw new NotImplementedException();
    }

    public Task RemoverLivro(string isbn)
    {
        throw new NotImplementedException();
    }

    public livro NotFound()
    {
        throw new NotImplementedException();
    }

    public async Task GetLivros()
    {
        var livro = await _livroRepository.ObterTodos();
    }


    public async Task<livro> GetLivro(string isbn)
    {
        var livro = await _livroRepository.ObterPorIsbn(isbn);

        if (livro == null)
        {
            return NotFound();
        }

        return livro;
    }

    public async Task<livro> AddLivro(livro livro)
    {
        return await _livroRepository.AdicionarLivro(livro);

        
    }


    public async Task UpdateLivro(livro isbn)
    {
        await _livroRepository.AtualizarLivro(isbn);
    }

    public async Task<livro> DeleteLivro(string isbn, livro livro)
    {
        await _livroRepository.RemoverLivro(isbn);
        if (livro == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    public livro NoContent()
    {
        throw new NotImplementedException();
    }
}
