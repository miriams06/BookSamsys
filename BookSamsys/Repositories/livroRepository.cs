using BookSamsys.Models;
using Microsoft.AspNetCore.Mvc;
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

    public async Task<livro> ObterPorIsbn(string isbn)
    {
        var livro = _context.Livros.FirstOrDefault(l => l.ISBN == isbn);
        return livro;
    }

    public async Task<livro> AdicionarLivro(livro livro)
    {
        await _context.Livros.AddAsync(livro);
        await _context.SaveChangesAsync();
        return livro;
    }

    public async Task<livro> AtualizarLivro(livro livro)    
    {
        //await _context.Livros.AddAsync(livro);
        await _context.SaveChangesAsync();
        return livro;
    }

    public async Task<livro> RemoverLivro(string isbn)
    {
        var livro = await _context.Livros.FindAsync(isbn);
        _context.Livros.Remove(livro);
        await _context.SaveChangesAsync();
        return livro;
    }
}
