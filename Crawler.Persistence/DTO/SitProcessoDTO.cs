using Crawler.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Persistence.DTO
{
    public class SitProcessoDTO
    {

        public int SitProcessoId { get; set; }
        public string? NumeroProcesso { get; set; }
        public string? Situacao { get; set; }

        public int ProcessoId { get; set; }
        public Processo? Processo { get; set; }

    }
}
