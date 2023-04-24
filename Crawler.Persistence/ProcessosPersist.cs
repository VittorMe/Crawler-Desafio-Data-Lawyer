using Crawler.Domain;
using Crawler.Persistence.Processos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Globalization;
using Crawler.Persistence.DTO;
using Crawler.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Crawler.Persistence
{
    public class ProcessosPersist : IProcessosPersist
    {
        private readonly HttpClient _httpClient;
        private readonly HtmlWeb _htmlWeb;
        private readonly CrawlerContext _context;

        public ProcessosPersist(CrawlerContext context)
        {
            _httpClient = new HttpClient();
            _htmlWeb = new HtmlWeb();
            _context = context;
        }

        public async Task<string[]> FormatDadosProcesso(string idProcesso)
        {
            string parte1 = idProcesso.Substring(0, 15);
            string parte2 = idProcesso.Substring(16, 4);
            string parte3 = idProcesso.Substring(21);

            string[] partesSeparadas = new string[] { parte1, parte2, parte3 };
            return await Task.FromResult(partesSeparadas);
        }

        public async Task<ProcessDTO> GetProcesso(string listDadosRef)
        {
            try
            {
                ProcessDTO processo = new ProcessDTO();
                MovimentacaoDTO movimentacao = new MovimentacaoDTO();
                SitProcessoDTO sitProcesso = new SitProcessoDTO();
                processo.Movimentacoes = new List<MovimentacaoDTO>();
                processo.SitProcessos = new List<SitProcessoDTO>();
                var url = string.Format($"http://esaj.tjba.jus.br/cpo/sg/search.do?paginaConsulta=1&cbPesquisa=NUMPROC&tipoNuProcesso=SAJ&numeroDigitoAnoUnificado=&foroNumeroUnificado=&dePesquisaNuUnificado=&dePesquisa={listDadosRef}&pbEnviar=Pesquisar");

                var htmlDocument = await _htmlWeb.LoadFromWebAsync(url);
                if (htmlDocument.DocumentNode.InnerText != null)
                {
                    sitProcesso.NumeroProcesso = FormatText(htmlDocument.DocumentNode.SelectSingleNode("/html/body/table[4]//tr/td/table[2]//tr[1]/td[2]/table//tr/td/span[1]").InnerText);
                    sitProcesso.Situacao = FormatText(htmlDocument.DocumentNode.SelectSingleNode("/html/body/table[4]//tr/td/table[2]//tr[1]/td[2]/table//tr/td/span[3]").InnerText);

                    processo.SitProcessos.Add(sitProcesso);


                    processo.Classe = FormatText(htmlDocument.DocumentNode.SelectSingleNode("html/body/table[4]//tr/td/table[2]//tr[2]/td[2]/table//tr/td/span/span").InnerText);
                    processo.Area = FormatText(htmlDocument.DocumentNode.SelectSingleNode("//span[@class='labelClass']/following-sibling::text()[1]").InnerText);
                    processo.Assunto = FormatText(htmlDocument.DocumentNode.SelectSingleNode("/html/body/table[4]//tr/td/table[2]//tr[4]/td[2]/span").InnerText);
                    processo.Origem = FormatText(htmlDocument.DocumentNode.SelectSingleNode("/html/body/table[4]//tr/td/table[2]//tr[5]/td[2]/span/text()").InnerText);
                    processo.Distribuição = FormatText(htmlDocument.DocumentNode.SelectSingleNode("/html/body/table[4]//tr/td/table[2]//tr[7]/td[2]/span").InnerText);
                    processo.Relator = FormatText(htmlDocument.DocumentNode.SelectSingleNode("/html/body/table[4]//tr/td/table[2]//tr[8]/td[2]/span").InnerText);


                    var movitacoes = htmlDocument.DocumentNode.SelectSingleNode("/html/body/table[4]//tr/td/table[10]");
                    if (movitacoes.ChildNodes.Count > 0)
                    {
                        var td = movitacoes.ChildNodes;
                        foreach (var item in td)
                        {
                            if (item.Name == "tr")
                            {
                                var format = FormatText(item.InnerText);

                                string[] parts = format.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                string data = parts[0];
                                string descricao = "";
                                for (int i = 1; i < parts.Length; i++)
                                {
                                    descricao += parts[i] + " ";
                                }
                                descricao = descricao.Trim();

                                movimentacao.DataMovimento = DateTime.ParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                movimentacao.Descricao = FormatText(descricao);
                                processo.Movimentacoes.Add(movimentacao);
                            }
                        }
                    }
                    return processo;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            
            return null;
        }

        public async Task<bool> TodosOsCamposSaoNulos(object obj)
        {
            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(obj);

                if (value != null)
                {
                    return false;
                }
            }

            return true;
        }


        public async Task<Processo> GetProcessoByIdAsync(int processoId)
        {
            IQueryable<Processo> query = _context.Processos
                .Include(e => e.SitProcessos)
                .Include(e => e.Movimentacoes);


            query =  query.AsNoTracking().AsNoTracking().OrderBy(e => e.Id).Where(e => e.Id == processoId);

            return await query.FirstOrDefaultAsync();
        }

        private string FormatText(string text)
        {

            var texto = text.Replace("\n", "").Replace("\t", "").Replace("\r", "").Trim();

            return texto;
        }

        public async Task<Processo[]> GetAllProcessosAsync()
        {
            IQueryable<Processo> query = _context.Processos
                 .Include(e => e.SitProcessos)
                 .Include(e => e.Movimentacoes);


            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }
    }
}
