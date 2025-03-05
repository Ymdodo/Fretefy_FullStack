using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.Interfaces.Services;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;


namespace Fretefy.Test.Domain.Services
{
    public class RegiaoService : IRegiaoService
    {
        private readonly IRegiaoRepository _regiaoRepository;

        public RegiaoService(IRegiaoRepository regiaoRepository)
        {
            _regiaoRepository = regiaoRepository;
        }

        public async Task<List<Regiao>> GetAllAsync()
        {
            return await _regiaoRepository.GetAllAsync();
        }

        public async Task<Regiao> GetByIdAsync(int id)
        {
            return await _regiaoRepository.GetByIdAsync(id);
        }

        public async Task<bool> CreateAsync(Regiao regiao)
        {
            if (await _regiaoRepository.ExistsAsync(regiao.Nome))
                throw new Exception("Nome da região já existe!");

            if (regiao.Cidades == null || regiao.Cidades.Count == 0)
                throw new Exception("Uma região deve conter pelo menos uma cidade.");

            foreach (var cidade in regiao.Cidades)
            {
                if (cidade.CidadeId == 0)
                    throw new Exception("Cidade inválida! O ID da cidade não pode ser 0.");

                cidade.RegiaoId = regiao.Id; 
            }

            await _regiaoRepository.AddAsync(regiao);
            return true;
        }



        public async Task DeletarAsync(int id)
        {
            var regiao = await _regiaoRepository.GetByIdAsync(id);
            if (regiao != null)
            {
                await _regiaoRepository.DeleteAsync(regiao);
            }
        }

        public MemoryStream ExportToExcel(List<Regiao> regioes)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Regiões");

            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Nome";
            worksheet.Cell(1, 3).Value = "Ativo";
            worksheet.Cell(1, 4).Value = "Cidades";

            int row = 2;
            foreach (var regiao in regioes)
            {
                worksheet.Cell(row, 1).Value = regiao.Id;
                worksheet.Cell(row, 2).Value = regiao.Nome;
                worksheet.Cell(row, 3).Value = regiao.Ativo ? "Sim" : "Não";
                worksheet.Cell(row, 4).Value = string.Join(", ",
                regiao.Cidades
                    .Where(c => c.Cidade != null)
                    .Select(c => c.Cidade.Nome));
                row++;
            }

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;
            return stream;
        }
       
        public async Task AtualizarAsync(Regiao regiao)
        {
            var regiaoExistente = await _regiaoRepository.GetByIdAsync(regiao.Id);
            if (regiaoExistente == null)
                throw new Exception("Região não encontrada.");

            regiaoExistente.Nome = regiao.Nome;
            regiaoExistente.Ativo = regiao.Ativo;

            regiaoExistente.Cidades.Clear();
            await _regiaoRepository.UpdateAsync(regiaoExistente);

            regiaoExistente.Cidades = regiao.Cidades.Select(c => new RegiaoCidade
            {
                RegiaoId = regiao.Id,
                CidadeId = c.CidadeId
            }).ToList();

            await _regiaoRepository.UpdateAsync(regiaoExistente);
        }








    }
}
