using BookSamsys.Models;
using BookSamsys.Repository;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using BookSamsys.Models.DTOs;

namespace BookSamsys.Services
{
    public class AutorService : ControllerBase, IAutorService
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IMapper _mapper;

        public AutorService(IMapper mapper, IAutorRepository autorRepository)
        {
            _mapper = mapper;
            _autorRepository = autorRepository;
        }

        public async Task<MessagingHelper<List<autorDTO>>> ObterTodosAutores()
        {
            var resposta = new MessagingHelper<List<autorDTO>>();
            string mensagemErro = "Erro ao obter os autores.";

            var autores = await _autorRepository.ObterTodos();

            if (autores == null)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = mensagemErro;
                return resposta;
            }

            var autoresDTO = _mapper.Map<List<autorDTO>>(autores);

            resposta.Sucesso = true;
            resposta.Mensagem = "Autores obtidos com sucesso.";
            resposta.Obj = autoresDTO;
            return resposta;
        }

        public async Task<MessagingHelper<autorDTO>> ObterAutorPorId(int id)
        {
            var resposta = new MessagingHelper<autorDTO>();
            string mensagemErro = $"Autor com ID {id} não encontrado.";

            if (id <= 0)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = "O ID do autor deve ser maior que zero.";
                return resposta;
            }

            var autor = await _autorRepository.ObterPorId(id);

            if (autor == null)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = mensagemErro;
                return resposta;
            }

            var autorDTO = _mapper.Map<autorDTO>(autor);

            resposta.Sucesso = true;
            resposta.Mensagem = "Autor obtido com sucesso.";
            resposta.Obj = autorDTO;
            return resposta;
        }

        public async Task<MessagingHelper<autorDTO>> AdicionarAutor(autorDTO autorDto)
        {
            var resposta = new MessagingHelper<autorDTO>();
            string mensagemErro = "Erro ao adicionar o autor.";

            if (autorDto == null || string.IsNullOrEmpty(autorDto.nome))
            {
                resposta.Sucesso = false;
                resposta.Mensagem = "Informações do autor inválidas.";
                return resposta;
            }

            var autor = _mapper.Map<autor>(autorDto);

            try
            {
                await _autorRepository.AdicionarAutor(autor);
                resposta.Sucesso = true;
                resposta.Mensagem = "Autor adicionado com sucesso.";
                resposta.Obj = autorDto;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = $"{mensagemErro} {ex.Message}";
                return resposta;
            }
        }

        public async Task<MessagingHelper<autorDTO>> AtualizarAutor(int id, autorDTO autorDto)
        {
            var resposta = new MessagingHelper<autorDTO>();
            string mensagemErro = "Erro ao atualizar o autor.";

            if (id <= 0 || autorDto == null || string.IsNullOrEmpty(autorDto.nome))
            {
                resposta.Sucesso = false;
                resposta.Mensagem = "Informações do autor inválidas.";
                return resposta;
            }

            var autor = await _autorRepository.ObterPorId(id);

            if (autor == null)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = $"Autor com ID {id} não encontrado.";
                return resposta;
            }

            autor.nome = autorDto.nome;

            try
            {
                await _autorRepository.AtualizarAutor(autor);
                resposta.Sucesso = true;
                resposta.Mensagem = "Autor atualizado com sucesso.";
                resposta.Obj = autorDto;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = $"{mensagemErro} {ex.Message}";
                return resposta;
            }
        }

        public async Task<MessagingHelper<autorDTO>> RemoverAutor(int id)
        {
            var resposta = new MessagingHelper<autorDTO>();
            string mensagemErro = "Erro ao remover o autor.";

            if (id <= 0)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = "ID do autor inválido.";
                return resposta;
            }

            var autor = await _autorRepository.ObterPorId(id);

            if (autor == null)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = $"Autor com ID {id} não encontrado.";
                return resposta;
            }

            await _autorRepository.RemoverAutor(id);
            resposta.Sucesso = true;
            resposta.Mensagem = "Autor removido com sucesso.";
            resposta.Obj = _mapper.Map<autorDTO>(autor);
            return resposta;
            
        }
    }
}
