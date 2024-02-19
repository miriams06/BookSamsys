using BookSamsys.Models;
using BookSamsys.Models.DTOs;
using BookSamsys.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookSamsys.Services;

public interface ILivroService
{
    Task<MessagingHelper<List<livroDTO>>> ObterTodosLivros();
    Task<MessagingHelper<livroDTO>> ObterLivroPorIsbn(string isbn);

    //Task<ActionResult<livro>> ListarLivros(string ordenacao, int paginaAtual, int itensPorPagina);
    Task<MessagingHelper<livroDTO>> AdicionarLivro(livroDTO livroDto);
    Task<MessagingHelper<livroDTO>> AtualizarLivro(livroDTO livroDto);
    Task<MessagingHelper<livroDTO>> RemoverLivro(string isbn);
}
