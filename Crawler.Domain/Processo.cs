using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Domain
{


    public class SitProcessoEvent
    {
        public int SitProcessoId { get; set; }
        public SitProcesso? SitProcesso { get; set; }
        public int MovimentacaoId { get; set; }
        public Movimentacao? Movimentacao { get; set; }
    }

    public class Processo
    {
        public int Id { get; set; }
        public string? Classe { get; set; }
        public string? Area { get; set; }
        public string? Assunto { get; set; }
        public string? Origem { get; set; }
        public string? Distribuição { get; set; }
        public string? Relator { get; set; }
        public List<SitProcesso>? SitProcessos { get; set; }
        public List<Movimentacao>? Movimentacoes { get; set; }

    }

    public class SitProcesso
    {
        
        public int SitProcessoId { get; set; }
        public string? NumeroProcesso { get; set; }
        public string? Situacao { get; set; }

        public int ProcessoId { get; set; }
        public Processo? Processo { get; set; }
    }

    public class Movimentacao
    {
        public int Id { get; set; }
        public DateTime DataMovimento { get; set; }
        public string? Descricao { get; set; }

        public int ProcessoId { get; set; }
        public Processo? Processo { get; set; }
    }
}
