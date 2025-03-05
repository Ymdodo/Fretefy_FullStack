using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Services;

namespace Fretefy.Test.Domain.Interfaces.Repositories
{
    public interface ICidadeRepository
    {
        Task<bool> ExistsAsync(string nome, string uf);
        Task<Cidade> GetByIdAsync(int id);
        Task AddAsync(Cidade cidade);
        IQueryable<Cidade> List();
        IEnumerable<Cidade> ListByUf(string uf);
        IEnumerable<Cidade> Query(string terms);
        Task DeleteAsync(Cidade cidade);

        Task<int> GetUltimoIdAsync();

    }
}
