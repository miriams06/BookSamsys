﻿// <auto-generated />
using BookSamsys.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookSamsys.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20231221152150_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookSamsys.Models.autor", b =>
                {
                    b.Property<int>("idAutor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idAutor"));

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idAutor");

                    b.ToTable("Autores");
                });

            modelBuilder.Entity("BookSamsys.Models.livro", b =>
                {
                    b.Property<string>("isbn")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("idAutor")
                        .HasColumnType("int");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("preco")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("isbn");

                    b.HasIndex("idAutor");

                    b.ToTable("Livros");
                });

            modelBuilder.Entity("BookSamsys.Models.livro", b =>
                {
                    b.HasOne("BookSamsys.Models.autor", "autor")
                        .WithMany("Livros")
                        .HasForeignKey("idAutor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("autor");
                });

            modelBuilder.Entity("BookSamsys.Models.autor", b =>
                {
                    b.Navigation("Livros");
                });
#pragma warning restore 612, 618
        }
    }
}