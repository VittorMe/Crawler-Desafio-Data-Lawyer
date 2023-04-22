using Crawler.Persistence.DTO;
using static Crawler.Domain.CrawlerException;

namespace Crawler.Application.Repository.IRepository
{
    public interface IProcesso
    {
        Task<ProcessDTO> AddProcesso(string Processo);
        Task<bool> DeleteProcesso(int eventoId);
        Task<Domain.Processo> UpdateEvento(int eventoId, Domain.Processo Model);


        Task<Domain.Processo> GetEventoByIdAsync(int eventoId);
        Task<Domain.Processo[]> GetAllProcessosAsync();
    }
}