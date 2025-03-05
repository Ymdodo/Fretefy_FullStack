using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretefy.Test.Infra.EntityFramework.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly TestDbContext _context; 
        private readonly DbSet<Cidade> _dbSet;

        public CidadeRepository(TestDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Cidade>(); 
        }

        public async Task<bool> ExistsAsync(string nome, string uf)
        {
            return await _dbSet.AnyAsync(c => c.Nome == nome && c.UF == uf);
        }

        public async Task<Cidade> GetByIdAsync(int id)
        {
            return await _context.Cidades.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int> GetUltimoIdAsync()
        {
            var ultimaCidade = await _context.Cidades
                .OrderByDescending(c => c.Id)
                .FirstOrDefaultAsync();

            return ultimaCidade?.Id ?? 0; 
        }


        public async Task AddAsync(Cidade cidade)
        {
            _context.Cidades.Add(cidade); 
            await _context.SaveChangesAsync(); 
        }


        public IQueryable<Cidade> List()
        {
            return _dbSet.AsQueryable();
        }

        public IEnumerable<Cidade> ListByUf(string uf)
        {
            return _dbSet.Where(w => EF.Functions.Like(w.UF, $"%{uf}%")).ToList();
        }

        public IEnumerable<Cidade> Query(string terms)
        {
            return _dbSet.Where(w => EF.Functions.Like(w.Nome, $"%{terms}%") || EF.Functions.Like(w.UF, $"%{terms}%")).ToList();
        }

        public async Task DeleteAsync(Cidade cidade)
        {
            _context.Cidades.Remove(cidade);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarTodasAsync()
        {
            var cidades = await _context.Cidades.ToListAsync();

            if (!cidades.Any())
            {
                return; 
            }

            _context.Cidades.RemoveRange(cidades);
            await _context.SaveChangesAsync();
        }

    }
}
