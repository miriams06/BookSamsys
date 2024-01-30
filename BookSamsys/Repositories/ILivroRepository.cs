using BookSamsys.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookSamsys.Repository
{
    public interface ILivroRepository
    {
        Task<IEnumerable<livro>> ObterTodos();
        Task<livro> ObterPorIsbn(string isbn);
        Task<livro> AdicionarLivro(livro livro);
        Task<livro> AtualizarLivro(livro livro);
        Task<livro> RemoverLivro(string isbn);
    }
}