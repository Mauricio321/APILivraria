using APILivraria.Data;
using APILivraria.Models;
using APILivraria.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APILivraria.Repositories
{
    public class GenerosRepositories : IGenerosRepositories
    {
        private readonly LivrariaContext context;

        public GenerosRepositories(LivrariaContext context)
        {
            this.context = context;
        }

        public async Task<string> AdicionarGenero(Generos generos)
        {
            await context.Generossss.AddAsync(generos);
            await context.SaveChangesAsync();

            return $"{generos} adicionado com sucesso";
        }

        public void ApagarGenero(int id)
        {
            var generoId = context.Generossss.Find(id);

            if (generoId != null)
            {
                context.Generossss.Remove(generoId);
            }

            context.SaveChanges();
        }

        public IEnumerable<Generos> GenerosDisponiveis()
        {
            var TodosGenerosDisponiveis = context.Generossss.ToList();

            return TodosGenerosDisponiveis;
        }

        public Task<List<Generos>> ObterGenero(List<int> id, CancellationToken cancellationToken)
        {
           var genero = context.Generossss.Where(g=> id.Contains(g.Id)).ToListAsync(cancellationToken);

           return genero;
        }

        public async Task<Dictionary<int, Generos?>> ObterGeneros(List<int> idFilters, CancellationToken cancellationToken)
        {
            var generos = await context.Generossss.Where(g => idFilters.Contains(g.Id)).ToListAsync(cancellationToken);

            return idFilters.ToDictionary(f => f, f => generos.FirstOrDefault(g => g.Id == f));
        }
    }
} 
