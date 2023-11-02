using System;

namespace campoMinado
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Criação do campo
            int[,] campo = new int[10, 10]; //matriz com as posições do campo
            int[,] jogo = new int[10, 10]; //matriz que registra as ações do jogador

            int qtdLinhas = campo.GetLength(0);
            int qtdColunas = campo.GetLength(1);

            bool problemaArquivo = false;
            string caminho_absoluto = "C:\\Users\\ferna\\Documents\\dev\\graduation-campo-minado\\vscode\\campo.txt";

            try
            {
                //Informa o caminho e o nome do arquivo
                StreamReader sr = new StreamReader(caminho_absoluto);

                //Lê a primeira linha
                String linha_arq = sr.ReadLine();
                int linha_mtz = 0;
                int coluna_mtz = 0;

                //Lê até não identificar nova linha
                while (linha_arq != null || linha_mtz < 10)
                {
                    //Separando os elementos por vírgula
                    foreach (var numero in linha_arq.Split(','))
                    {
                        int num;

                        //Converte os elementos para int
                        if (int.TryParse(numero, out num))
                        {
                            //Armazena elementos na matriz campo
                            campo[linha_mtz, coluna_mtz] = num;
                            jogo[linha_mtz, coluna_mtz] = -1;
                            coluna_mtz++;
                        }
                    }

                    //Lê a próxima linha
                    linha_arq = sr.ReadLine();

                    //Continua a leitura da próxima linha iniciando pelo primeiro valor
                    coluna_mtz = 0;
                    linha_mtz++;
                }
                //Conclui a leitura do arquivo
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocorreu um problema na leitura do arquivo!");
                problemaArquivo = true;
            }

            //Jogo
            if (!problemaArquivo)
            {
                bool fimJogo = false;
                bool vitoria = false;
                do
                {
                    for (int l = 0; l < qtdLinhas; l++)
                    {
                        for (int c = 0; c < qtdColunas; c++)
                        {
                            Console.Write(string.Format("{0}", jogo[l, c]));
                        }
                        Console.Write(Environment.NewLine + Environment.NewLine);
                    }
                    Console.WriteLine("Seleciona uma linha [1-10]: ");
                    int linha = Convert.ToInt32(Console.ReadLine()) - 1;
                    Console.WriteLine("Selecione uma coluna [1-10]: ");
                    int coluna = Convert.ToInt32(Console.ReadLine()) - 1;

                    switch (campo[linha, coluna])
                    {
                        case 0:
                            jogo[linha, coluna] = 0;
                            Console.Write("Continue tentando... \n\n");
                            break;
                        case 1:
                            jogo[linha, coluna] = 1;
                            Console.Write("BOOOM!!! Você perdeu...\n\n");
                            fimJogo = true;
                            break;
                        default:
                            jogo[linha, coluna] = 2;
                            Console.Write("Parabéns, você ganhou!!! \n\n");
                            fimJogo = true;
                            vitoria = true;
                            break;
                    }
                } while (!fimJogo);

                string[] arquivo = File.ReadAllLines(caminho_absoluto);
                string msgVitorias = arquivo[arquivo.Length - 2];
                string msgDerrotas = arquivo[arquivo.Length - 1];
                try
                {
                    StreamWriter sw = new StreamWriter(caminho_absoluto);
                    int contagem;
                    int linha_sobrescrever;
                    string texto;

                    if (vitoria)
                    {
                        int.TryParse(msgVitorias.Split(':')[1], out contagem);
                        linha_sobrescrever = 12;
                        texto = "Vitórias:";
                    }
                    else
                    {
                        int.TryParse(msgDerrotas.Split(':')[1], out contagem);
                        linha_sobrescrever = 13;
                        texto = "Derrotas:";
                    }

                    contagem++;

                    for (int i = 0; i < arquivo.Length; i++)
                    {
                        if (i == linha_sobrescrever)
                            sw.WriteLine(texto + contagem);
                        else
                            sw.WriteLine(arquivo[i]);
                    }
                    sw.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ocorreu um problema na escrita do arquivo...");
                }
            }
        }
    }
}
