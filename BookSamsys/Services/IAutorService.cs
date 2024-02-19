using BookSamsys.Models;
using BookSamsys.Models.DTOs;
using BookSamsys.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookSamsys.Services;

public interface IAutorService
{
    Task<MessagingHelper<List<autorDTO>>> ObterTodosAutores();
    Task<MessagingHelper<autorDTO>> ObterAutorPorId(int id);
    Task<MessagingHelper<autorDTO>> AdicionarAutor(autorDTO autorDto);
    Task<MessagingHelper<autorDTO>> AtualizarAutor(int id, autorDTO autorDto);
    Task<MessagingHelper<autorDTO>> RemoverAutor(int id);
}
