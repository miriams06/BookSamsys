namespace BookSamsys.Models.DTOs
{
    public class livroDTO
    {
            public string ISBN { get; set; }
            public string Nome { get; set; }
            public int IdAutor { get; set; }
            public decimal Preco { get; set; }
    }
}