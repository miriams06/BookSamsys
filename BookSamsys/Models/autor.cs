using System.ComponentModel.DataAnnotations;

namespace BookSamsys.Models
{
    public class autor
    {
        [Key]
        public int idAutor { get; set; }
        public required string nome { get; set; }
        public List<livro> Livros { get; set; } = new List<livro>();
    }
}
