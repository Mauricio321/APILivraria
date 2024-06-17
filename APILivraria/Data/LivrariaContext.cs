
using APILivraria.Integracao.Response;
using APILivraria.Models;
using Microsoft.EntityFrameworkCore;

namespace APILivraria.Data;

public class LivrariaContext : DbContext
{
    public LivrariaContext(DbContextOptions<LivrariaContext> options)
        : base(options) { }
     

    public DbSet<Livro> Livrarias { get; set; }
    public DbSet<Generos> Generossss { get; set; }
    public DbSet<LivroGenero> LivroGeneros { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Carrinho> Carrinhos {  get; set; }
    public DbSet<CarrinhoItem> CarrinhoItems { get; set; }
    public DbSet<CadastroEnderecoCompra> CadastroCompras { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Haskey = chave primaria 
        modelBuilder.Entity<LivroGenero>().HasKey(lg  => new { lg.LivroId,
        lg.GeneroId});

        modelBuilder.Entity<LivroGenero>()
            .HasOne<Livro>(lg => lg.Livro)
            .WithMany(l => l.Generos)
            .HasForeignKey(lg => lg.LivroId);

        modelBuilder.Entity<LivroGenero>()
          .HasOne<Generos>(lg => lg.Genero)
          .WithMany(l => l.Livrosss)
          .HasForeignKey(lg => lg.GeneroId);

        modelBuilder.Entity<User>().
            HasOne(a => a.Role).
            WithMany(a => a.Users)
            .HasForeignKey(k => k.RoleId);

        modelBuilder.Entity<EnderecoUsuario>().HasKey(eu => new { eu.UserId, eu.EnderecoId });

        modelBuilder.Entity<EnderecoUsuario>()
            .HasOne(eu => eu.Usuario)
            .WithMany(e => e.Enderecos)
            .HasForeignKey(eu => eu.UserId);

        modelBuilder.Entity<EnderecoUsuario>()
            .HasOne(eu => eu.EnderecoCompra)
            .WithMany(u => u.Usuarios)
            .HasForeignKey(eu => eu.EnderecoId);




        modelBuilder.Entity<CarrinhoItem>().HasKey(ci => ci.CarrinhoItemId);
        modelBuilder.Entity<Carrinho>().HasKey(ci => ci.CarrinhoId);

        modelBuilder.Entity<User>().HasOne(U => U.Carrinho).WithOne(c => c.Usuario).HasForeignKey<Carrinho>(c => c.UserId);
        modelBuilder.Entity<CarrinhoItem>().HasOne<Carrinho>(ci => ci.Carrinho).WithMany(c => c.CarrinhoItens).HasForeignKey(u => u.CarrinhoId);

        modelBuilder.Entity<Role>()
            .HasData(new Role
            {
               Id = 1,
               Name = "manager"

            },
            new Role
            {
                Id = 2,
                Name = "authenticated"
            }
            );

        modelBuilder.Entity<User>()
            .HasData(new User { Id = 1, Email = "adminlivraria@gmail.com",Password = "admin123@" , RoleId = 1});
        
        modelBuilder.Entity<Role>()
            .HasMany(role => role.Users)
            .WithOne(user => user.Role)
            .HasForeignKey(k => k.RoleId);

        modelBuilder.Entity<CadastroEnderecoCompra>()
           .HasKey(e => e.Id);
    }
}
