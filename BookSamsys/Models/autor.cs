﻿using System.ComponentModel.DataAnnotations;

namespace BookSamsys.Models
{
    public class autor
    {
        [Key]
        public int idAutor { get; set; }

        [Required]
        public string nome { get; set; }

    }
}
