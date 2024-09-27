
//Предположим, у вас есть консольное приложение, имитирующее банк с несколькими банкоматами. 
//Каждый банкомат позволяет клиентам снимать деньги со своих счетов.
//Однако если два клиента попытаются снять деньги одновременно,
//существует риск того, что основной баланс в банке, станет отрицательным, 
//в итоге выдача средств клиенту засчитается, но купюр он не получит.

//Чтобы предотвратить это, вы можете использовать класс Monitor для синхронизации доступа к переменной баланса счета.
using System;
using System.Threading;

//class BankAccount
//{
//    private decimal balance; 
//    private readonly object locker = new object(); 

//    public BankAccount(decimal Balance)
//    {
//        balance = Balance;
//    }

//    public void Withdraw(decimal amount)
//    {

//        Monitor.Enter(locker);
//        if (balance >= amount)
//        {
//            Console.WriteLine($"{Thread.CurrentThread.Name} собирается снять {amount}");
//            Thread.Sleep(1000);
//            balance -= amount;
//            Console.WriteLine($"{Thread.CurrentThread.Name} успешно снял {amount}");
//            Console.WriteLine($"текущий баланс: {balance}");
//        }
//        else
//        {
//            Console.WriteLine($"{Thread.CurrentThread.Name} попытался снять {amount}, но недостаточно средств.");
//        }
//        Monitor.Exit(locker);
//    }   

//}



//class Program
//{
//    static void Main()
//    {
//        BankAccount account = new BankAccount(3300m);

//        Thread[] threads = new Thread[5];
//        for (int i = 0; i < threads.Length; i++)
//        {
//            threads[i] = new Thread(() => account.Withdraw(700m)) { Name = $"Банкомат {i + 1}" };
//        }
//        foreach (Thread thread in threads)
//        {
//            thread.Start();
//        }
//        foreach(Thread thread in threads)
//        {
//            thread.Join();
//        }

//        Console.WriteLine("Транзакции завершены.");
//    }
//}



//Создайте приложение «Танцующие прогресс-бары».
//Приложение отображает набор прогресс-баров. Их количество определяется пользователем.
//По нажатию на кнопку прогресс-бары начинают заполняться (величины процесса заполнения
//и цвет определяются случайным образом). Используйте механизм многопоточности.

//class Program
//{
//    static Random random = new Random();
//    static object lockObject = new object();

//    static void Main(string[] args)
//    {
//        Console.WriteLine("Введите количество прогресс-баров:");
//        if (!int.TryParse(Console.ReadLine(), out int progressBarCount) || progressBarCount <= 0)
//        {
//            Console.WriteLine("Некорректное количество.");
//            return;
//        }
//        Thread[] threads = new Thread[progressBarCount];

//        for (int i = 0; i < progressBarCount; i++)
//        {
//            int index = i;
//            threads[i] = new Thread(() => RunProgressBar(index));
//            threads[i].Start();
//        }


//        foreach (Thread thread in threads)
//        {
//            thread.Join();
//        }
//    }

//    static void RunProgressBar(int index)
//    {

//        ConsoleColor barColor = GetRandomColor();
//        while (true)
//        {

//            lock (lockObject)
//            {
//                Console.ForegroundColor = barColor;
//                Console.SetCursorPosition(0, index);
//                Console.Write(new string(' ', Console.WindowWidth));
//                string equalString = new string('=', random.Next(1, 30));
//                Console.WriteLine(equalString);

//            }
//            Thread.Sleep(random.Next(300));
//        }
//    }
//    static ConsoleColor GetRandomColor()
//    {

//        ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
//        ConsoleColor color;
//        do
//        {
//            color = colors[random.Next(colors.Length)];
//        } while (color == ConsoleColor.Black);

//        return color;
//    }
//}

    //У вас есть консольное приложение, которое имитирует работу печати с несколькими принтерами. 
    //Каждый принтер может обрабатывать одно задание за раз, 
    //но заданий может быть больше, чем доступных принтеров. 
    //Чтобы предотвратить наложение заданий друг на друга и ограничить количество одновременно обрабатываемых заданий, 
    //вы можете использовать класс Semaphore для синхронизации доступа к принтерам.


class Program
{

    static Semaphore semaphore;

    static void Main(string[] args)
    {
        int Printers = 3; 
        int Jobs = 15; 


        semaphore = new Semaphore(Printers, Printers);


        for (int i = 1; i <= Jobs; i++)
        {
            int Id = i;
            Thread JobThread = new Thread(() => Print(Id));
            JobThread.Start();
        }
    }

    static void Print(int Id)
    {
        semaphore.WaitOne();
        Console.WriteLine($"Задание {Id} отправлено на печать.");
        Thread.Sleep(3000);
        Console.WriteLine($"Задание {Id} завершено.");
        semaphore.Release();

       
    }
}