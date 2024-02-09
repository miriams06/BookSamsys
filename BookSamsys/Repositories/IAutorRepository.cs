using BookSamsys.Models;

namespace BookSamsys.Repository
{
        public interface IAutorRepository
        {
            Task<IEnumerable<autor>> ObterTodos();
            Task<autor> ObterPorId(int id);
            Task<autor> AdicionarAutor(autor autor);
            Task<autor> AtualizarAutor(autor autor);
            Task<autor> RemoverAutor(int id);
        }
}
