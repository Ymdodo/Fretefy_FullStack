using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretefy.Test.WebApi.Controllers
{
    [Route("api/cidade")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly ICidadeService _cidadeService;

        public CidadeController(ICidadeService cidadeService)
        {
            _cidadeService = cidadeService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cidade cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade.Nome) || string.IsNullOrWhiteSpace(cidade.UF))
                return BadRequest(new { error = "Nome e UF da cidade são obrigatórios!" });

            await _cidadeService.CreateAsync(cidade);
            return Ok(new { message = "Cidade cadastrada com sucesso!" });
        }

        [HttpGet]
        public IActionResult List([FromQuery] string uf, [FromQuery] string terms)
        {
            IEnumerable<Cidade> cidades;

            if (!string.IsNullOrEmpty(terms))
                cidades = _cidadeService.Query(terms);
            else if (!string.IsNullOrEmpty(uf))
                cidades = _cidadeService.ListByUf(uf);
            else
                cidades = _cidadeService.List();

            return Ok(cidades);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarCidade(int id)
        {
            var regiao = await _cidadeService.GetByIdAsync(id);
            if (regiao == null)
            {
                return NotFound();
            }

            await _cidadeService.DeletarAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var cidades = _cidadeService.Get(id);
            return Ok(cidades);
        }

    }
}
