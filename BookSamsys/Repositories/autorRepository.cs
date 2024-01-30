using BookSamsys.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSamsys.Repository;
public class AutorRepository : IAutorRepository
{
    private readonly AppDBContext _context;

    public AutorRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<autor>> ObterTodos()
    {
        return await _context.Autores.ToListAsync();
    }

    public async Task<autor> ObterPorId(int id)
    {
        return await _context.Autores.FindAsync(id);
    }

    public async Task AdicionarAutor(autor autor)
    {
        _context.Autores.Add(autor);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAutor(autor autor)
    {
        _context.Entry(autor).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAutor(int id)
    {
        var autor = await _context.Autores.FindAsync(id);
        _context.Autores.Remove(autor);
        await _context.SaveChangesAsync();
    }
}
