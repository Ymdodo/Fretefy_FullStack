using Fretefy.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces
{
    public interface ICidadeService
    {
        Task CreateAsync(Cidade cidade);
        Cidade Get(int id);
        Task<Cidade> GetByIdAsync(int id);
        IEnumerable<Cidade> List();
        IEnumerable<Cidade> ListByUf(string uf);
        IEnumerable<Cidade> Query(string terms);
        Task DeletarAsync(int id);

    }
}
