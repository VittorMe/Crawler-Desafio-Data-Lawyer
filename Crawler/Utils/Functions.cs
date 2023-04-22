namespace Crawler.Utils
{
    public class Functions
    {

        public static bool VerificaCodProcesso(string minhaString)
        {
            string referencia = "0000000-00.0000.0.00.0000";

            bool isValid = false;

            if (minhaString.Length == referencia.Length)
            { // verifica se a string tem o mesmo tamanho da referência
                string[] partesReferencia = referencia.Split('-', '.', '.', '.', '.'); // separa a referência em partes
                string[] partesMinhaString = minhaString.Split('-', '.', '.', '.', '.'); // separa a string em partes

                if (partesReferencia.Length == partesMinhaString.Length)
                { // verifica se a string tem a mesma quantidade de partes que a referência
                    isValid = true;

                    for (int i = 0; i < partesReferencia.Length; i++)
                    { // verifica se cada parte da string corresponde a parte da referência
                        if (partesReferencia[i].Length != partesMinhaString[i].Length)
                        {
                            isValid = false;
                            break;
                        }
                    }
                }
            }

            if (isValid) return true;
            else return false;

        }


    }
}

