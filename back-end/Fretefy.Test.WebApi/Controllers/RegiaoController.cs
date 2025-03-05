using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fretefy.Test.Application.Services;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Application;
using Fretefy.Test.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fretefy.Test.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegiaoController : ControllerBase
    {
        private readonly IRegiaoService _regiaoService;
        private readonly IRegiaoAppService _regiaoAppService;
        public RegiaoController(IRegiaoService regiaoService, IRegiaoAppService regiaoAppService)
        {
            _regiaoService = regiaoService;
            _regiaoAppService = regiaoAppService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Regiao>>> GetAll()
        {
            return await _regiaoService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegiaoPorId(int id)
        {
            var regiao = await _regiaoService.GetByIdAsync(id);

            if (regiao == null)
                return NotFound();

            return Ok(regiao);
        }

        [HttpPost]
        public async Task<IActionResult> CriarRegiao([FromBody] Regiao regiao)
        {
            try
            {
                await _regiaoService.CreateAsync(regiao);
                return CreatedAtAction(nameof(GetRegiaoPorId), new { id = regiao.Id }, regiao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar a região: {ex.Message} - {ex.InnerException?.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarRegiao(int id)
        {
            var regiao = await _regiaoService.GetByIdAsync(id);
            if (regiao == null)
            {
                return NotFound();
            }

            await _regiaoService.DeletarAsync(id); 
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarRegiao(int id, [FromBody] Regiao regiaoAtualizada)
        {
            if (regiaoAtualizada == null)
                return BadRequest("Erro: O corpo da requisição está vazio!");

            if (id != regiaoAtualizada.Id)
                return BadRequest("Erro: O ID da URL não bate com o ID da requisição!");

            try
            {
                var regiaoExistente = await _regiaoService.GetByIdAsync(id);
                if (regiaoExistente == null)
                    return NotFound("Região não encontrada.");

                regiaoExistente.Nome = regiaoAtualizada.Nome;
                regiaoExistente.Ativo = regiaoAtualizada.Ativo;

                regiaoExistente.Cidades.Clear();
                foreach (var cidade in regiaoAtualizada.Cidades)
                {
                    regiaoExistente.Cidades.Add(new RegiaoCidade
                    {
                        RegiaoId = id,
                        CidadeId = cidade.CidadeId
                    });
                }

                await _regiaoService.AtualizarAsync(regiaoExistente);

                return Ok(new { message = "Região atualizada com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar região: {ex.Message}");
            }
        }



        [HttpGet("export")]
        public IActionResult ExportToExcel()
        {
            var stream = _regiaoAppService.ExportToExcel();
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "regioes.xlsx");
        }
    }
}
