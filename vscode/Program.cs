using System;

namespace campoMinado
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Criação do campo
            int[,] campo = new int[10, 10];
            int[,] jogo = new int[10, 10];

            int qtdLinhas = campo.GetLength(0);
            int qtdColunas = campo.GetLength(1);

            for (int l = 0; l < qtdLinhas; l++)
            {
                for (int c = 0; c < qtdColunas; c++)
                {
                    campo[l, c] = 0;
                    jogo[l, c] = -1;
                }
            }

            //Posicionando a bandeira aleatóriamente
            Random gerador = new Random();
            int linha = gerador.Next(qtdLinhas);
            int coluna = gerador.Next(qtdColunas);
            campo[linha, coluna] = 2;

            //Posicionando as bombas aleatóriamente
            int bombasPosicionadas = 0;
            do
            {
                linha = gerador.Next(qtdLinhas);
                coluna = gerador.Next(qtdColunas);
                if (campo[linha, coluna] == 0)
                {
                    campo[linha, coluna] = 1;
                    bombasPosicionadas++;
                }
            } while (bombasPosicionadas < 5);

            //Interação do usuário
            bool fimJogo = false;
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
                Console.Write("Selecione uma linha [1-10]: ");
                linha = Convert.ToInt32(Console.ReadLine()) - 1;
                Console.Write("Seleciona uma coluna [1-10]: ");
                coluna = Convert.ToInt32(Console.ReadLine()) - 1;

                switch (campo[linha, coluna])
                {
                    case 0:
                        jogo[linha, coluna] = 0;
                        Console.WriteLine("Continue tentando.\n\n");
                        break;

                    case 1:
                        jogo[linha, coluna] = 1;
                        Console.WriteLine("BOOM!!! Você perdeu...\n\n");
                        fimJogo = true;
                        break;

                    default:
                        jogo[linha, coluna] = 2;
                        Console.WriteLine("Parabéns!!! Você ganhou! \n\n");
                        fimJogo = true;
                        break;
                }
            } while (!fimJogo);
        }
    }
}
