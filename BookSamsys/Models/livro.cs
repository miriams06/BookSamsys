using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSamsys.Models
{
    public class livro
    {
        [Key]
        public string isbn { get; set; }
        public string nome { get; set; }
        public int idAutor { get; set; }
        public decimal preco { get; set; }
        public autor autor { get; set; }
    }
}
