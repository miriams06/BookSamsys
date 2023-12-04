using BookSamsys.Models;

namespace BookSamsys.Repository
{
    public interface ILivroRepository
    {
        Task<IEnumerable<livro>> ObterTodos();
        Task<livro> ObterPorIsbn(string isbn);
        Task<livro> AdicionarLivro(livro livro);
        Task AtualizarLivro(livro livro);
        Task RemoverLivro(string isbn);
        Task AdicionarLivro();
    }
}