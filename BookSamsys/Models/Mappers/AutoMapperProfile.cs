using AutoMapper;
using BookSamsys.Models.DTOs;
namespace BookSamsys.Models.Mappers;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<livro, livroDTO>();
        CreateMap<livroDTO, livro>();
    }
}
