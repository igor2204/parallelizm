using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        int N = 100; // Количество элементов вектора
        int M = 4; // Число потоков
        
        double[] vector = new double[N];
        
        // Заполняем вектор случайными значениями
        Random random = new Random();
        for (int i = 0; i < N; i++)
        {
            vector[i] = random.NextDouble();
        }
        
        Console.WriteLine("Последовательная обработка:");
        double number = 2.5; // Число для умножения
        
        SequentialProcessing(vector, number); // Последовательная обработка

        Console.WriteLine("Многопоточная обработка:");
        ParallelProcessing(vector, number, M); // Многопоточная обработка

        Console.ReadLine();
    }
    
    // Последовательная обработка элементов вектора
    static void SequentialProcessing(double[] vector, double number)
    {
        for (int i = 0; i < vector.Length; i++)
        {
            vector[i] *= number;
        }
        
        // Вывод результатов
        for (int i = 0; i < vector.Length; i++)
        {
            Console.WriteLine(vector[i]);
        }
    }
    
    // Многопоточная обработка элементов вектора
    static void ParallelProcessing(double[] vector, double number, int threadCount)
    {
        int elementsPerThread = vector.Length / threadCount;
        
        // Создаем задачи для потоков
        Task[] tasks = new Task[threadCount];
        for (int i = 0; i < threadCount; i++)
        {
            int startIndex = i * elementsPerThread;
            int endIndex = (i == threadCount - 1) ? vector.Length : (i + 1) * elementsPerThread;
            
            tasks[i] = Task.Factory.StartNew(() =>
            {
                for (int j = startIndex; j < endIndex; j++)
                {
                    vector[j] *= number;
                }
            });
        }
        
        // Дожидаемся завершения всех задач
        Task.WaitAll(tasks);
        
        // Вывод результатов
        for (int i = 0; i < vector.Length; i++)
        {
            Console.WriteLine(vector[i]);
        }
    }
}
