using BookSamsys.Models;

namespace BookSamsys.Repository
{
        public interface IAutorRepository
        {
            Task<IEnumerable<autor>> ObterTodos();
            Task<autor> ObterPorId(int id);
            Task AdicionarAutor(autor autor);
            Task AtualizarAutor(autor autor);
            Task RemoverAutor(int id);
        }
}
