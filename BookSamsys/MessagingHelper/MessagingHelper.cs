using BookSamsys.Controllers;
using BookSamsys.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using BookSamsys.Models;
using BookSamsys.Repository;
using BookSamsys.Services;

public class MessagingHelper<T>
{
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; }
    public T Obj { get; set; }
}