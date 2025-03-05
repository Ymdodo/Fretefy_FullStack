using System.Collections.Generic;
using System.Threading.Tasks;
using Fretefy.Test.Domain.Entities;
using System.IO;

namespace Fretefy.Test.Domain.Interfaces.Application
{
    public interface IRegiaoAppService
    {
        Task<List<Regiao>> GetAllAsync();
        Task<bool> CreateAsync(Regiao regiao);
        MemoryStream ExportToExcel();
    }
}
