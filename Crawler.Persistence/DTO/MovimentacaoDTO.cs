using Crawler.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Persistence.DTO
{
    public class MovimentacaoDTO
    {

        public int Id { get; set; }
        public DateTime DataMovimento { get; set; }
        public string? Descricao { get; set; }

        public int ProcessoId { get; set; }
        public Processo? Processo { get; set; }

    }
}
