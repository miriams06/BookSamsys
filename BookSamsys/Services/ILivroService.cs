using BookSamsys.Models;
using BookSamsys.Repository;

namespace BookSamsys.Services;

public interface ILivroService
{
    Task<IEnumerable<livro>> ObterTodosLivros();
    Task ObterLivroPorIsbn(string isbn);
    Task AdicionarLivro(livro livro);
    Task AtualizarLivro(livro livro);
    Task RemoverLivro(string isbn);
    Task PostLivro(livro livro);
    Task<void> PostLivro(livro livro);
}
