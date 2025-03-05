using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Services
{
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;

        public CidadeService(ICidadeRepository cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
        }

        public Cidade Get(int id)
        {
            return _cidadeRepository.List().FirstOrDefault(f => f.Id == id);
        }

        public async Task<Cidade> GetByIdAsync(int id)
        {
            return await _cidadeRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Cidade cidade)
        {
            var exists = await _cidadeRepository.ExistsAsync(cidade.Nome, cidade.UF);
            if (exists)
                throw new Exception("Essa cidade já está cadastrada!");

            var ultimoId = await _cidadeRepository.GetUltimoIdAsync();

            cidade.Id = ultimoId + 1;

            await _cidadeRepository.AddAsync(cidade);
        }

        public IEnumerable<Cidade> List()
        {
            return _cidadeRepository.List();
        }

        public IEnumerable<Cidade> ListByUf(string uf)
        {
            return _cidadeRepository.ListByUf(uf);
        }

        public IEnumerable<Cidade> Query(string terms)
        {
            return _cidadeRepository.Query(terms);
        }

        public async Task DeletarAsync(int id)
        {
            var cidade = await _cidadeRepository.GetByIdAsync(id);
            if (cidade != null)
            {
                await _cidadeRepository.DeleteAsync(cidade);
            }
        }


    }
}
