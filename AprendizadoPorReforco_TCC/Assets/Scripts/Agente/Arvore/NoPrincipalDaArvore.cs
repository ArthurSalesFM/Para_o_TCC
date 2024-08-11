using System.Collections.Generic;
using System.Linq;

public class NoPrincipalDaArvore 
{
    private int pontuacaoMaximaDoCaminho;
    private List<TestaTodosOsCaminhos> shapes = new List<TestaTodosOsCaminhos>();
    // Referência para o filho à esquerda
    private NoPrincipalDaArvore filhoEsquerdo;

    // Referência para o filho à direita
    private NoPrincipalDaArvore filhoDireito;

    // Construtor que inicializa o nó com um valor
    public NoPrincipalDaArvore(int valor, string[,] shape)
    {
        this.pontuacaoMaximaDoCaminho = valor; // Armazena o valor no nó
        AdicionarMatrizSeNaoExistir(shape);
        
        this.filhoEsquerdo = null; // Inicialmente, o filho à esquerda é nulo
        this.filhoDireito = null; // Inicialmente, o filho à direita é nulo 
    }
    
    
    public void AdicionarMatrizSeNaoExistir(string[,] novaMatriz)
    {
        // Verifica se a matriz já existe na lista
        TestaTodosOsCaminhos shape;
        //shape.setaValores(novaMatriz);

        if(this.shapes.Count == 0)
        {
            shape = new TestaTodosOsCaminhos();
            shape.setaValores(novaMatriz);
            this.shapes.Add(shape);
        }


        //bool matrizExiste = MatrizEhIgual();

        // Se a matriz não existir, adiciona à lista

    }

    // Método auxiliar para comparar duas matrizes
    private bool MatrizEhIgual(string[,] matriz1, string[,] matriz2)
    {
        // Verifica se as dimensões são diferentes
        if (matriz1.GetLength(0) != matriz2.GetLength(0) || matriz1.GetLength(1) != matriz2.GetLength(1))
        {
            return false;
        }

        // Verifica se os valores das matrizes são diferentes
        for (int i = 0; i < matriz1.GetLength(0); i++)
        {
            for (int j = 0; j < matriz1.GetLength(1); j++)
            {
                // Aqui você precisa implementar a comparação dos elementos do tipo TestaTodosOsCaminhos
                // A comparação abaixo é apenas um exemplo. Ajuste conforme necessário.
                if (matriz1[i,j] != matriz2[i,j])
                {
                    return false;
                }
            }
        }

        return true; // As matrizes são iguais
    }

    // Getter para filhoEsquerdo
    public NoPrincipalDaArvore getFilhoEsquerdo()
    {
        return filhoEsquerdo;
    }

    // Getter para filhoDireito
    public NoPrincipalDaArvore getFilhoDireito()
    {
        return filhoDireito;
    }

    // Métodos para definir os filhos, se necessário
    public void setFilhoEsquerdo(NoPrincipalDaArvore filho)
    {
        this.filhoEsquerdo = filho;
    }

    public void setFilhoDireito(NoPrincipalDaArvore filho)
    {
        this.filhoDireito = filho;
    }

    // Getter para pontuacaoMaximaDoCaminho
    public int getPontuacaoMaximaDoCaminho()
    {
        return this.pontuacaoMaximaDoCaminho;
    }

    // Método auxiliar para comparar dois elementos do tipo TestaTodosOsCaminhos
    private bool MatrizElementoEhIgual(TestaTodosOsCaminhos elemento1, string[,] elemento2)
    {
        // Aqui você deve definir como comparar dois elementos do tipo TestaTodosOsCaminhos
        // Exemplo de comparação: se eles forem iguais ou se tiverem alguma propriedade específica que pode ser comparada
        return elemento1.Equals(elemento2);
    }

}
