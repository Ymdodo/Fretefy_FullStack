using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Infra.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Fretefy.Test.Infra.Repositories
{
    public class RegiaoRepository : IRegiaoRepository
    {
        private readonly TestDbContext _context;

        public RegiaoRepository(TestDbContext context)
        {
            _context = context;
        }

        public async Task<List<Regiao>> GetAllAsync()
        {

            return await _context.Regioes
                .Include(r => r.Cidades) 
                .ThenInclude(rc => rc.Cidade) 
                .ToListAsync();
        }

        public async Task<Regiao> GetByIdAsync(int id)
        {
            return await _context.Regioes
                .Include(r => r.Cidades)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Regiao> GetByNameAsync(string nome)
        {
            return await _context.Regioes
                .FirstOrDefaultAsync(r => r.Nome == nome);
        }

        public async Task<bool> ExistsAsync(string nome)
        {
            return await _context.Regioes.AnyAsync(r => r.Nome == nome);
        }

        public async Task AddAsync(Regiao regiao)
        {
            _context.Regioes.Add(regiao);

            if (regiao.Cidades != null && regiao.Cidades.Any())
            {
                foreach (var cidade in regiao.Cidades)
                {
                    _context.Entry(cidade).State = EntityState.Added; 
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Regiao regiao)
        {
            _context.Regioes.Remove(regiao);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Regiao regiao)
        {
            var regiaoExistente = await _context.Regioes
                .Include(r => r.Cidades)
                .FirstOrDefaultAsync(r => r.Id == regiao.Id);

            if (regiaoExistente == null)
                throw new Exception("Região não encontrada.");

            Console.WriteLine($"Antes da atualização: Região {regiaoExistente.Id} tem {regiaoExistente.Cidades.Count} cidades.");

            regiaoExistente.Nome = regiao.Nome;
            regiaoExistente.Ativo = regiao.Ativo;

            _context.RegiaoCidades.RemoveRange(regiaoExistente.Cidades);
            await _context.SaveChangesAsync(); 

            Console.WriteLine("Cidades removidas. Agora vamos adicionar as novas...");

 
            regiaoExistente.Cidades = regiao.Cidades.Select(c => new RegiaoCidade
            {
                RegiaoId = regiao.Id,
                CidadeId = c.CidadeId
            }).ToList();

            _context.RegiaoCidades.AddRange(regiaoExistente.Cidades);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Depois da atualização: Região {regiaoExistente.Id} tem {regiaoExistente.Cidades.Count} cidades.");
        }



    }
}
