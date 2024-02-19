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

    public async Task<autor> AdicionarAutor(autor nome)
    {
        await _context.Autores.AddAsync(nome);
        await _context.SaveChangesAsync();
        return nome;
    }

    public async Task<autor> AtualizarAutor(autor autor)
    {
        //var autorExiste = await _context.Autores.FindAsync(autor.idAutor);
        //_context.Entry(autorExiste).CurrentValues.SetValues(autor);
        await _context.SaveChangesAsync();
        return autor;
    }

    public async Task<autor> RemoverAutor(int id)
    {
        var autor = await _context.Autores.FindAsync(id);
        _context.Autores.Remove(autor);
        await _context.SaveChangesAsync();
        return autor;
    }
}
