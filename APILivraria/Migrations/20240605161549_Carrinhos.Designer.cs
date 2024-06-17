﻿// <auto-generated />
using System;
using APILivraria.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APILivraria.Migrations
{
    [DbContext(typeof(LivrariaContext))]
    [Migration("20240605161549_Carrinhos")]
    partial class Carrinhos
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APILivraria.Models.Carrinho", b =>
                {
                    b.Property<int>("CarrinhoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarrinhoId"));

                    b.Property<int>("QuantidadeItens")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CarrinhoId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Carrinhos");
                });

            modelBuilder.Entity("APILivraria.Models.CarrinhoItem", b =>
                {
                    b.Property<int>("CarrinhoItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarrinhoItemId"));

                    b.Property<int>("CarrinhoId")
                        .HasColumnType("int");

                    b.Property<int>("LivroId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("CarrinhoItemId");

                    b.HasIndex("CarrinhoId");

                    b.HasIndex("LivroId");

                    b.ToTable("CarrinhoItems");
                });

            modelBuilder.Entity("APILivraria.Models.Generos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Generossss");
                });

            modelBuilder.Entity("APILivraria.Models.Livraria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Autor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Livro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Livrarias");
                });

            modelBuilder.Entity("APILivraria.Models.LivroGenero", b =>
                {
                    b.Property<int>("LivroId")
                        .HasColumnType("int");

                    b.Property<int>("GeneroId")
                        .HasColumnType("int");

                    b.HasKey("LivroId", "GeneroId");

                    b.HasIndex("GeneroId");

                    b.ToTable("LivroGeneros");
                });

            modelBuilder.Entity("APILivraria.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "manager"
                        },
                        new
                        {
                            Id = 2,
                            Name = "authenticated"
                        });
                });

            modelBuilder.Entity("APILivraria.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarrinhoId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LivrariaId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LivrariaId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CarrinhoId = 0,
                            Email = "adminlivraria@gmail.com",
                            Password = "admin123@",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("APILivraria.Models.Carrinho", b =>
                {
                    b.HasOne("APILivraria.Models.User", "Usuario")
                        .WithOne("Carrinho")
                        .HasForeignKey("APILivraria.Models.Carrinho", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("APILivraria.Models.CarrinhoItem", b =>
                {
                    b.HasOne("APILivraria.Models.Carrinho", "Carrinho")
                        .WithMany("CarrinhoItems")
                        .HasForeignKey("CarrinhoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APILivraria.Models.Livraria", "Livro")
                        .WithMany()
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrinho");

                    b.Navigation("Livro");
                });

            modelBuilder.Entity("APILivraria.Models.LivroGenero", b =>
                {
                    b.HasOne("APILivraria.Models.Generos", "Genero")
                        .WithMany("Livrosss")
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APILivraria.Models.Livraria", "Livro")
                        .WithMany("Generos")
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genero");

                    b.Navigation("Livro");
                });

            modelBuilder.Entity("APILivraria.Models.User", b =>
                {
                    b.HasOne("APILivraria.Models.Livraria", null)
                        .WithMany("Usuarios")
                        .HasForeignKey("LivrariaId");

                    b.HasOne("APILivraria.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("APILivraria.Models.Carrinho", b =>
                {
                    b.Navigation("CarrinhoItems");
                });

            modelBuilder.Entity("APILivraria.Models.Generos", b =>
                {
                    b.Navigation("Livrosss");
                });

            modelBuilder.Entity("APILivraria.Models.Livraria", b =>
                {
                    b.Navigation("Generos");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("APILivraria.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("APILivraria.Models.User", b =>
                {
                    b.Navigation("Carrinho")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
