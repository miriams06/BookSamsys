using BookSamsys.Models;
using BookSamsys.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using NuGet.LibraryModel;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Azure.Messaging;
using System.Web;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Threading.Tasks;
using BookSamsys.Models.DTOs;

namespace BookSamsys.Services;

public class LivroService : ControllerBase, ILivroService
{
    private readonly ILivroRepository _livroRepository; IAutorRepository _autorRepository;
    private readonly IMapper _mapper;
    private List<livro> livros;
    public LivroService(IMapper mapper, ILivroRepository livroRepository, IAutorRepository autorRepository)
    {
        _mapper = mapper;
        _livroRepository = livroRepository;
        livros = new List<livro>();
        _autorRepository = autorRepository;
    }


    public async Task<MessagingHelper<List<livroDTO>>> ObterTodosLivros()
    {
        var resposta = new MessagingHelper<List<livroDTO>>();
        string mensagemErro = "Ocorreu um erro ao obter os livros.";

        try
        {
            var livros = await _livroRepository.ObterTodos();

            if (livros == null)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = mensagemErro;
                return resposta;
            }

            var livrosDTO = _mapper.Map<List<livroDTO>>(livros);

            resposta.Obj = livrosDTO;
            resposta.Sucesso = true;
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Sucesso = false;
            resposta.Mensagem = mensagemErro + " Detalhes: " + ex.Message;
            return resposta;
        }
    }


    public async Task<MessagingHelper<livroDTO>> ObterLivroPorIsbn(string isbn)
    {
        var resposta = new MessagingHelper<livroDTO>();
        string mensagemErro = "Erro ao obter livro por ISBN.";

        try
        {
            if (isbn.Length != 13)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = "O ISBN deve ter 13 caracteres.";
                return resposta;
            }

            var livro = await _livroRepository.ObterPorIsbn(isbn);

            if (livro == null)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = $"Livro com ISBN {isbn} não encontrado.";
                return resposta;
            }

            var livroDTO = _mapper.Map<livroDTO>(livro);

            resposta.Obj = livroDTO;
            resposta.Sucesso = true;
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Sucesso = false;
            resposta.Mensagem = mensagemErro + " Detalhes: " + ex.Message;
            return resposta;
        }
    }


    public async Task<MessagingHelper<livroDTO>> AdicionarLivro(livroDTO livroDto)
    {
        var resposta = new MessagingHelper<livroDTO>();
        string mensagemErro = "Erro ao adicionar o livro.";

        try
        {
            if (livroDto == null || livroDto.ISBN.Length != 13 || livroDto.Preco < 0)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = "Ocorreu um erro ao adicionar as informações.";
                return resposta;
            }

            var livroExistente = await _livroRepository.ObterPorIsbn(livroDto.ISBN);

            if (livroExistente != null)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = $"O ISBN {livroDto.ISBN} já existe.";
                return resposta;
            }

            var autorExistente = await _autorRepository.ObterPorId(livroDto.IdAutor);
            if (autorExistente == null)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = $"Autor com o ID {livroDto.IdAutor} não encontrado.";
                return resposta;
            }

            var livro = _mapper.Map<livro>(livroDto);
            await _livroRepository.AdicionarLivro(livro);

            resposta.Sucesso = true;
            resposta.Mensagem = "Livro inserido com sucesso.";
            resposta.Obj = livroDto;
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Sucesso = false;
            resposta.Mensagem = mensagemErro + " Detalhes: " + ex.Message;
            return resposta;
        }
    }

    public async Task<MessagingHelper<livroDTO>> AtualizarLivro(livroDTO livroDto)
    {
        var resposta = new MessagingHelper<livroDTO>();
        string mensagemErro = "Erro ao atualizar o livro.";

        try
        {
            if (livroDto == null)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = "O objeto livroDTO não pode ser nulo.";
                return resposta;
            }

            var livro = _mapper.Map<livro>(livroDto);

            if (livro == null)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = "Ocorreu um erro ao converter o livroDTO para livro.";
                return resposta;
            }

            if (livro.ISBN.Length != 13 || livro.Preco < 0)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = "Os dados do livro são inválidos.";
                return resposta;
            }

            var livroObtido = await _livroRepository.ObterPorIsbn(livro.ISBN);

            if (livroObtido == null)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = $"Livro com o ISBN {livro.ISBN} não encontrado.";
                return resposta;
            }

            var autorExistente = await _autorRepository.ObterPorId(livro.IdAutor);

            if (autorExistente == null)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = $"Autor com o ID {livro.IdAutor} associado ao livro não encontrado.";
                return resposta;
            }

            await _livroRepository.AtualizarLivro(livro);

            resposta.Sucesso = true;
            resposta.Mensagem = "Livro atualizado com sucesso.";
            resposta.Obj = livroDto;
            return resposta;
        }
        catch (DbUpdateException ex)
        {
            resposta.Sucesso = false;
            resposta.Mensagem = mensagemErro + ex.Message;
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Sucesso = false;
            resposta.Mensagem = mensagemErro + ex.Message;
            return resposta;
        }
    }

    public async Task<MessagingHelper<livroDTO>> AtivarLivro(string isbn)
    {
        var resposta = new MessagingHelper<livroDTO>();

        var livro = await _livroRepository.ObterPorIsbn(isbn);

        if (livro == null)
        {
            resposta.Sucesso = false;
            resposta.Mensagem = "Livro não encontrado.";
            return resposta;
        }

        livro.Estado = true;

        await _livroRepository.AtivarLivro(isbn);

        var livroDTO = _mapper.Map<livroDTO>(livro);
        resposta.Sucesso = true;
        resposta.Mensagem = "Livro ativado com sucesso.";
        resposta.Obj = livroDTO;

        return resposta;
    }


    public async Task<MessagingHelper<livroDTO>> RemoverLivro(string isbn)
    {
        var resposta = new MessagingHelper<livroDTO>();

        var livro = await _livroRepository.ObterPorIsbn(isbn);

        if (livro == null)
        {
            resposta.Sucesso = false;
            resposta.Mensagem = "Livro não encontrado.";
            return resposta;
        }

        livro.Estado = false;

        await _livroRepository.RemoverLivro(isbn);

        var livroDTO = _mapper.Map<livroDTO>(livro);
        resposta.Sucesso = true;
        resposta.Mensagem = "Estado do livro alterado para inativo.";
        resposta.Obj = livroDTO;

        return resposta;
    }



}
