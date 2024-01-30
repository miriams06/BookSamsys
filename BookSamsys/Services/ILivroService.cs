using BookSamsys.Models;
using BookSamsys.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookSamsys.Services;

public interface ILivroService
{
    Task<IEnumerable<livro>> ObterTodosLivros();
    Task<ActionResult<livro>> ObterLivroPorIsbn(string isbn);

    //Task<ActionResult<livro>> ListarLivros(string ordenacao, int paginaAtual, int itensPorPagina);
    Task<ActionResult<livro>> AdicionarLivro(livro addLivro);
    Task<ActionResult<livro>> AtualizarLivro(livro editarLivro);
    Task<ActionResult<livro>> RemoverLivro(string isbn);
}
