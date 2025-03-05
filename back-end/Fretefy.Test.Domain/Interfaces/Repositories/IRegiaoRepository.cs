using System.Collections.Generic;
using System.Threading.Tasks;
using Fretefy.Test.Domain.Entities;

namespace Fretefy.Test.Domain.Interfaces.Repositories
{
    public interface IRegiaoRepository
    {
        Task<List<Regiao>> GetAllAsync();
        Task<Regiao> GetByIdAsync(int id);
        Task<Regiao> GetByNameAsync(string nome);
        Task AddAsync(Regiao regiao);
        Task<bool> ExistsAsync(string nome);
        Task DeleteAsync(Regiao regiao);
        Task UpdateAsync(Regiao regiao);


    }
}
