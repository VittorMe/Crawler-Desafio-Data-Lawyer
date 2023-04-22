using AutoMapper;
using Crawler.Application.Repository.IRepository;
using Crawler.Persistence.Processos;
using Crawler.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Crawler.Domain.CrawlerException;
using Crawler.Persistence.DTO;

namespace Crawler.Application.Repository
{
    public class Processo : IProcesso
    {
        private readonly IProcessosPersist _ProcessosPersist;
        private readonly IMapper _mapper;
        private readonly IGeralPersist _geralPersist;
        public Processo(IProcessosPersist processosPersist, IMapper mapper, IGeralPersist geralPersist)
        {
            _ProcessosPersist = processosPersist;
            _mapper = mapper;
            _geralPersist = geralPersist;
        }

        public async Task<ProcessDTO> AddProcesso(string Processo)
        {
            try
            {
                var listDados = await _ProcessosPersist.GetProcesso(Processo);

                var processo = _mapper.Map<Domain.Processo>(listDados);

                if (listDados == null || await _ProcessosPersist.TodosOsCamposSaoNulos(listDados)) return null;

                _geralPersist.Add<Domain.Processo>(processo);
                
                if (await _geralPersist.SaveChangesAsync())
                {
                   return _mapper.Map<ProcessDTO>(processo);
                   //return _mapper.Map<ProcessDTO>(await _ProcessosPersist.GetProcessoByIdAsync(processo.Id));
                }

                return listDados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<bool> DeleteProcesso(int eventoId)
        {
            try
            {
                var evento = await _ProcessosPersist.GetProcessoByIdAsync(eventoId);
                if (evento == null) throw new Exception("Evento para delete não foi encontrado.");

                _geralPersist.Delete<Domain.Processo>(evento);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<Domain.Processo[]> GetAllProcessosAsync()
        {
            try
            {
                var eventos = await _ProcessosPersist.GetAllProcessosAsync();
                if (eventos == null) return null;

                var resultado = _mapper.Map<Domain.Processo[]>(eventos);

                return resultado;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<Domain.Processo> GetEventoByIdAsync(int eventoId)
        {
            try
            {
                var evento = await _ProcessosPersist.GetProcessoByIdAsync(eventoId);
                if (evento == null) return null;

                //var resultado = _mapper.Map<ProcessDTO>(evento);

                return evento;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<Domain.Processo> UpdateEvento(int eventoId, Domain.Processo Model)
        {
            try
            {
                var evento = await _ProcessosPersist.GetProcessoByIdAsync(eventoId);
                if (evento == null) return null;

                Model.Id = evento.Id;

                _mapper.Map(Model, evento);

                _geralPersist.Update<Domain.Processo>(evento);

                if (await _geralPersist.SaveChangesAsync())
                {
                    return _mapper.Map<Domain.Processo>(await _ProcessosPersist.GetProcessoByIdAsync(eventoId));
                }
                return null;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
