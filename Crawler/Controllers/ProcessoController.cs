using Crawler.Application.Repository.IRepository;
using Crawler.Persistence.DTO;
using Crawler.Persistence.Processos;
using Microsoft.AspNetCore.Mvc;

namespace Crawler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessoController : ControllerBase
    {

        private readonly IProcesso _Processo;
        private readonly IProcessosPersist _ProcessosPersist;
        public ProcessoController(IProcesso processo, IProcessosPersist processosPersist)
        {
            _Processo = processo;
            _ProcessosPersist = processosPersist;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Add(string id)
        {
            try
            {
                if (!Utils.Functions.VerificaCodProcesso(id)) return this.StatusCode(StatusCodes.Status400BadRequest, $"Codigo do Processo incorreto!");

                var result = await _Processo.AddProcesso(id);
                if (result == null || await _ProcessosPersist.TodosOsCamposSaoNulos(result)) return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar Processo.");

                return Ok(result);

            }
            catch (Exception err)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar Processo. Erro.:{err.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _Processo.GetAllProcessosAsync();
                if (eventos == null) return NoContent();


                return Ok(eventos);
            }
            catch (Exception err)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Processo. Erro.:{err.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _Processo.GetEventoByIdAsync(id);
                if (evento == null) return NoContent();

                return Ok(evento);
            }
            catch (Exception err)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar evento. Erro.:{err.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Domain.Processo model)
        {
            try
            {
                var evento = await _Processo.UpdateEvento(id, model);
                if (evento == null) return NoContent();

                return Ok(evento);
            }
            catch (Exception err)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar alterações. Erro.:{err.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var processo = await _Processo.GetEventoByIdAsync(id);

                if (processo == null) return NoContent();

                return await _Processo.DeleteProcesso(id) ? Ok("Deletado") : throw new Exception("Ocorreu um problema não específico ao deletar Processo!");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}