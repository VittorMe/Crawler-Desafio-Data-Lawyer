using Crawler.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Persistence.DTO
{
    public class ProcessDTO
    {

        public int Id { get; set; }
        public string? Classe { get; set; }
        public string? Area { get; set; }
        public string? Assunto { get; set; }
        public string? Origem { get; set; }
        public string? Distribuição { get; set; }
        public string? Relator { get; set; }
        public List<SitProcessoDTO>? SitProcessos { get; set; }
        public List<MovimentacaoDTO>? Movimentacoes { get; set; }
    }
}
