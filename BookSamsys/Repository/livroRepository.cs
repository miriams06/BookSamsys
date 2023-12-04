using BookSamsys.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSamsys.Repository;

public class LivroRepository : ILivroRepository
    {
        private readonly AppDBContext _context;

        public LivroRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<livro>> ObterTodos()
        {
            return await _context.Livros.ToListAsync();
        }

        public async Task<livro> ObterPorIsbn(int isbn)
        {
            return await _context.Livros.FindAsync(isbn);
        }

        public async Task AdicionarLivro(livro livro)
        {
            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarLivro(livro livro)
        {
            _context.Entry(livro).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task RemoverLivro(int isbn)
        {
            var livro = await _context.Livros.FindAsync(isbn);
            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
        }
    }
