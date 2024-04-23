using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSamsys.Models
{
    public class livro
    {
        [Key]
        public string ISBN { get; set; }
        public string Nome { get; set; }
        public int IdAutor { get; set; }
        public decimal Preco { get; set; }
        public bool Estado { get; set; }
       
    }
    
}
