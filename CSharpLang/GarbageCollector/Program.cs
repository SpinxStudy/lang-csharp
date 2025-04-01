using System;

namespace GarbageCollector;

class Program
{
    static void Main(string[] args)
    {
        // Nota para o Garbage Collector funcionar
        // O Garbage Collector do C# realiza uma checagem do objeto que nao esta mais sendo utilizado, e o remove da memoria.
        // A checagem do objeto e feita atraves da verificacao da acessibilidade do objeto atraves da raiz de referencia, 
        // basicamente ele percorre no grafo de referencias e verifica se o objeto ainda esta acessivel
        // em caso negativo, ele estara elegivel para ser removido e coletado pelo GC
        Console.WriteLine("Garbage Collector - Testing");

        // Nao estarao disponiveis para a coleta do GC
        Payment payment1 = new Payment(1);
        Payment payment2 = new Payment(2);
        Payment payment3 = new Payment(3);

        CreatePayment();

        GC.Collect(); // Forca o GC a coletar os objetos elegiveis
        GC.WaitForPendingFinalizers();

        Console.WriteLine("Garbage Collector finish your task!");
    }

    static void CreatePayment()
    {
        // Estarao disponiveis para a coleta do GC pois no momento da execucao do metodo Collect,
        // esse metodo Create estara finalizado e os objetos sem referencias
        Payment payment4 = new Payment(4);
        Payment payment5 = new Payment(5);
        Payment payment6 = new Payment(6);
    }
}