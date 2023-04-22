using Crawler.Domain;
using Crawler.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Persistence.Processos
{
    public interface IProcessosPersist
    {
        //Task<string[]> FormatDadosProcesso(string idProcesso);
        Task<ProcessDTO> GetProcesso(string listDadosRef);

        Task<bool> TodosOsCamposSaoNulos(object obj);

        Task<Processo> GetProcessoByIdAsync(int eventoId);

        Task<Processo[]> GetAllProcessosAsync();
        

    }
}
