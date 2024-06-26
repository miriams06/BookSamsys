﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookSamsys.Models;
using BookSamsys.Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSamsys.Services;
using BookSamsys.Repository;

namespace BookSamsys.Controllers;

[Route("api/autores")]
[ApiController]
public class AutorController : ControllerBase
{
    private readonly IAutorService _autorService;

    public AutorController(IAutorService autorService)
    {
        _autorService = autorService;
    }

    [HttpGet]
    public async Task<MessagingHelper<List<autorDTO>>> GetAutores()
    {
        return await _autorService.ObterTodosAutores();
    }


    [HttpGet]
    [Route("{idAutor}")]
    public async Task<MessagingHelper<autorDTO>> GetAutor(int idAutor)
    {
        return await _autorService.ObterAutorPorId(idAutor);
    }

    [HttpPost]
    [Route("criarAutor")]
    public async Task<MessagingHelper<autorDTO>> AddAutor(autorDTO autorDto)
    {
        return await _autorService.AdicionarAutor(autorDto);
    }

    [HttpPatch]
    [Route("atualizarAutor/{idAutor}")]
    public async Task<MessagingHelper<autorDTO>> UpdateAutor(int idAutor, autorDTO autorDto)
    {
        return await _autorService.AtualizarAutor(idAutor, autorDto);
    }

    [HttpDelete]
    [Route("apagarAutor/{idAutor}")]
    public async Task<MessagingHelper<autorDTO>> DeleteAutor(int idAutor)
    {
        return await _autorService.RemoverAutor(idAutor);
    }
}