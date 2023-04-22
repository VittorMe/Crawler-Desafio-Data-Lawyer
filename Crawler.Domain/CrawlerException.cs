using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Domain
{
    public class CrawlerException
    {
        public enum TipoException : byte
        {
            ERRO = 1,
            ALERTA = 5,
            AVISO = 10,
            NI = 0
        }

        public enum GravidadeException : byte
        {
            ALTA = 1,
            MÉDIA = 5,
            BAIXA = 10,
            NI = 0

        }
        public class TExceptions : Exception
        {

            private int liCodErr;
            private string? lsMsg;


            //--MsgErr SUPORTE
            public string? MsgErr { get; set; }

            //--MsgErr USUARIO (AMIGÁVEL), COM SOLUÇÃO SE POSSIVEL.
            public string? MsgErrUsu { get; set; }

            public string? MsgErrOriginal { get; set; }

            //--Erro 1, Alerta 5,  Aviso 10
            public TipoException Tipo { get; set; }

            //--Alta, Baixa
            public GravidadeException Gravidade { get; set; }


            public int CodErr
            {
                get => liCodErr;
                set
                {
                    liCodErr = value;
                    if (value == -1)
                    {
                        this.Tipo = TipoException.ERRO;
                        this.Gravidade = GravidadeException.ALTA;
                        this.Mensagem = "Erro Desconhecido. Não tratado no código. Erro Inesperado. Abrir Chamado com o setor de desenvolvimento.";
                        this.MsgErr = "Erro Desconhecido. Não tratado no código. Erro Inesperado. Abrir Chamado com o setor de desenvolvimento.";
                        this.MsgErrUsu = "Erro Desconhecido ou Inesperado.";
                    }
                    else
                    {
                        if (value != 0)
                            return;
                        this.Tipo = TipoException.NI;
                        this.Gravidade = GravidadeException.NI;
                        this.Mensagem = "";
                    }
                }
            }

            public string Mensagem
            {
                get { return lsMsg; }
                set
                {
                    lsMsg = value;
                    MsgErr = value;
                    MsgErrUsu = value;
                    MsgErrOriginal = value;
                }
            }
        }
    }
}
