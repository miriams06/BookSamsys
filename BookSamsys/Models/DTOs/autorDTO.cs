using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;
namespace BookSamsys.Models.DTOs
{
    public class autorDTO
    {
        [SwaggerSchema(ReadOnly = true)]
        public int idAutor { get; set; }

        public string nome { get; set; }

    }
}
