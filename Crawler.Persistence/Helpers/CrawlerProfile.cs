using AutoMapper;
using Crawler.Domain;
using Crawler.Persistence.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Persistence.Helpers
{
    public class CrawlerProfile : Profile
    {
        public CrawlerProfile()
        {
            CreateMap<Processo, ProcessDTO>().ReverseMap();
            CreateMap<Movimentacao, MovimentacaoDTO>().ReverseMap();
            CreateMap<SitProcesso, SitProcessoDTO>().ReverseMap();
        }
    }
}
