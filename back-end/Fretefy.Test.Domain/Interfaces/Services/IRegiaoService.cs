using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Fretefy.Test.Domain.Entities;

namespace Fretefy.Test.Domain.Interfaces.Services
{
    public interface IRegiaoService
    {
        Task<List<Regiao>> GetAllAsync();
        Task<Regiao> GetByIdAsync(int id);
        Task<bool> CreateAsync(Regiao regiao);
        MemoryStream ExportToExcel(List<Regiao> regioes);
        Task DeletarAsync(int id);
        Task AtualizarAsync(Regiao regiao);

    }
}
