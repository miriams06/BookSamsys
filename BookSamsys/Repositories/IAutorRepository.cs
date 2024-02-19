using BookSamsys.Models;

namespace BookSamsys.Repository
{
        public interface IAutorRepository
        {
            Task<IEnumerable<autor>> ObterTodos();
            Task<autor> ObterPorId(int id);
            Task<autor> AdicionarAutor(autor nome);
            Task<autor> AtualizarAutor(autor nome);
            Task<autor> RemoverAutor(int id);
        }
}
