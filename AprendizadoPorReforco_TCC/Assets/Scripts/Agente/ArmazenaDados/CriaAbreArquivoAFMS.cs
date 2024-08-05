using System;
using System.IO;
using UnityEngine;

public class CriaAbreArquivoAFMS
{
    private string caminhoRelativo = "../Agente/ArmazenaDados/Dados"; // Caminho relativo

    // Propriedade para obter o caminho completo do arquivo
    private string CaminhoCompleto(string nomeArquivo)
    {
        // Combina o caminho relativo com o caminho do diret�rio de dados da aplica��o
        return Path.Combine(Application.dataPath, caminhoRelativo, nomeArquivo);
    }

    // M�todo para gravar a matriz em um arquivo de texto
    public void GravarMatriz(float[,] matriz, string nomeDoArquivo)
    {
        string caminhoCompleto = CaminhoCompleto(nomeDoArquivo);

        // Verifica se o arquivo j� existe
        if (File.Exists(caminhoCompleto))
        {
            Debug.LogWarning("O arquivo j� existe e n�o ser� alterado: " + caminhoCompleto);
            return;
        }

        try
        {
            // Cria o diret�rio se n�o existir
            Directory.CreateDirectory(Path.GetDirectoryName(caminhoCompleto));

            // Grava a matriz no arquivo
            using (StreamWriter writer = new StreamWriter(caminhoCompleto))
            {
                int linhas = matriz.GetLength(0);
                int colunas = matriz.GetLength(1);

                for (int i = 0; i < linhas; i++)
                {
                    for (int j = 0; j < colunas; j++)
                    {
                        writer.Write(matriz[i, j]);

                        // Adiciona um separador, como uma v�rgula
                        if (j < colunas - 1)
                        {
                            writer.Write("|");
                        }
                    }

                    // Adiciona uma nova linha para a pr�xima linha da matriz
                    writer.WriteLine();
                }
            }

            // Exibe o caminho completo onde o arquivo foi gravado
            Debug.Log("Matriz gravada com sucesso em " + caminhoCompleto);
        }
        catch (Exception e)
        {
            Debug.LogError("Erro ao gravar a matriz: " + e.Message);
        }
    }

    // M�todo para ler a matriz de um arquivo de texto
    public float[,] LerMatriz(string nomeDoArquivo)
    {
        string caminhoCompleto = CaminhoCompleto(nomeDoArquivo);

        if (!File.Exists(caminhoCompleto))
        {
            Debug.LogError("Arquivo n�o encontrado em " + caminhoCompleto);
            return null;
        }

        try
        {
            string[] linhas = File.ReadAllLines(caminhoCompleto);
            int numLinhas = linhas.Length;

            // Se o arquivo estiver vazio, retorna uma matriz vazia
            if (numLinhas == 0)
            {
                Debug.LogError("Arquivo vazio.");
                return new float[0, 0];
            }

            // Determina o n�mero de colunas
            int numColunas = linhas[0].Split('|').Length;
            float[,] matriz = new float[numLinhas, numColunas];

            for (int i = 0; i < numLinhas; i++)
            {
                string[] valores = linhas[i].Split('|');

                for (int j = 0; j < numColunas; j++)
                {
                    if (float.TryParse(valores[j], out float valor))
                    {
                        matriz[i, j] = valor;
                    }
                    else
                    {
                        Debug.LogError($"Valor inv�lido encontrado: {valores[j]}");
                    }
                }
            }

            Debug.Log("Matriz lida com sucesso.");
            return matriz;
        }
        catch (Exception e)
        {
            Debug.LogError("Erro ao ler a matriz: " + e.Message);
            return null;
        }
    }
}
