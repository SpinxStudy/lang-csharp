# Overview do Garbage Collector em C#

O Garbage Collector (GC) é um componente essencial do runtime do .NET, responsável por gerenciar automaticamente a memória em aplicações C#. Ele elimina a necessidade de alocação e liberação manual de memória, como em linguagens como C ou C++, reduzindo erros como vazamentos de memória e acessos inválidos.

## Principais Características

1. **Gerenciamento Automático de Memória**
   - O GC rastreia objetos alocados na heap gerenciada e libera aqueles que não são mais acessíveis, evitando a intervenção manual do programador.

2. **Sistema de Gerações**
   - Divide os objetos em três gerações (0, 1 e 2) para otimizar a coleta:
     - **Geração 0**: Objetos recém-criados, coletados com frequência.
     - **Geração 1**: Objetos que sobreviveram a uma coleta, promovidos da Geração 0.
     - **Geração 2**: Objetos de longa duração, coletados menos vezes.

3. **Não Determinístico**
   - O GC decide quando executar a coleta com base em fatores como pressão de memória, não garantindo liberação imediata de objetos.

4. **Suporte a Finalizadores**
   - Permite que objetos definam um método finalizador (`~Classe`) para executar ações antes da liberação, como liberar recursos não gerenciados.

5. **Modos de Operação**
   - **Workstation GC**: Otimizado para aplicações cliente, com baixa latência.
   - **Server GC**: Otimizado para servidores, com maior throughput, usando múltiplos threads para coleta.

## Funcionamento Básico

O GC opera em etapas principais:

1. **Marcação (Mark Phase)**
   - Começa pelas "raízes" (ex.: variáveis locais, estáticas, registradas em threads).
   - Constrói um grafo de objetos acessíveis, marcando-os como "vivos".
   - Objetos não marcados são considerados "mortos" e elegíveis para coleta.

2. **Compactação (Sweep and Compact Phase)**
   - Remove os objetos mortos da memória.
   - Reorganiza os objetos vivos na heap para evitar fragmentação, movendo-os para posições contíguas.

3. **Atualização de Referências**
   - Ajusta os ponteiros das referências para refletir as novas posições dos objetos após a compactação.

### Condições para Coleta
- Um objeto é coletado quando não tem mais referências ativas a partir de uma raiz.
- Exemplos: fim do escopo de uma variável local, reatribuição a `null`, ou remoção de uma coleção.

## Partes Importantes

1. **Heap Gerenciada**
   - Área de memória onde o GC aloca e gerencia objetos. Dividida em:
     - **Small Object Heap (SOH)**: Para objetos menores que 85 KB.
     - **Large Object Heap (LOH)**: Para objetos grandes, que não são compactados por padrão (apenas coletados).

2. **Garbage Collector API**
   - Métodos como `GC.Collect()` permitem forçar a coleta (embora raramente recomendado).
   - `GC.WaitForPendingFinalizers()` aguarda a execução de finalizadores.

3. **Common Language Runtime (CLR)**
   - O GC é parte integrante do CLR, que executa o código IL (Intermediate Language) gerado pelo compilador C#.

## Exemplo Prático
```csharp
class MeuObjeto {
    ~MeuObjeto() { Console.WriteLine("Coletado"); }
}
static void Main() {
    var obj = new MeuObjeto();
    obj = null; // Torna o objeto elegível
    GC.Collect(); // Força a coleta
}
```

## Considerações
- **Performance**: O GC é otimizado para balancear uso de memória e latência, mas chamadas manuais podem degradá-la.
- **Casos Especiais**: Recursos não gerenciados (ex.: arquivos, conexões) exigem `Dispose` ou `using`, pois o GC não os gerencia diretamente.

O GC do C# é uma ferramenta poderosa que simplifica o desenvolvimento, mas entender seu funcionamento é crucial para otimizar aplicações críticas.