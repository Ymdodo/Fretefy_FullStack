using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Application;
using Fretefy.Test.Domain.Interfaces.Services;

namespace Fretefy.Test.Application.Services
{
    public class RegiaoAppService : IRegiaoAppService
    {
        private readonly IRegiaoService _regiaoService;

        public RegiaoAppService(IRegiaoService regiaoService)
        {
            _regiaoService = regiaoService;
        }

        public async Task<List<Regiao>> GetAllAsync()
        {

            return await _regiaoService.GetAllAsync();
        }

        public async Task<bool> CreateAsync(Regiao regiao)
        {
            return await _regiaoService.CreateAsync(regiao);
        }

        public MemoryStream ExportToExcel()
        {
            var regioes = _regiaoService.GetAllAsync().Result;

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
                worksheet.Cell(row, 4).Value = string.Join(", ", regiao.Cidades.Select(c => c.Cidade?.Nome));
                row++;
            }

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;
            return stream;
        }
    }
}
