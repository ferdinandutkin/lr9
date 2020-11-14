using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace fp
{

    public class Director
    {
        public delegate void PaymentChange(int i);


        public event PaymentChange OnRaise = delegate { };

        public event PaymentChange OnFine = delegate { };

        public void Raise(int i) => OnRaise(i);

        public void Fine(int i) => OnFine(-i);



    }


    class Worker
    {

        public Worker(string fullName, int basicPayment) => (FullName, Payment) = (fullName, basicPayment);
        public int Payment { get; set; }
        public string FullName { get; set; }
        public void PaymentChangeHandler(int change) => Payment += change;



    }

    class Turner : Worker
    {
        public Turner(string fullName, int basicPayment) : base(fullName, basicPayment) { }
        public override string ToString() => $"Токарь {FullName}, получает {Payment}";

    }

    class PartTimeStudent : Worker
    {
        public PartTimeStudent(string fullName, int basicPayment) : base(fullName, basicPayment) { }

        public override string ToString() => $"Студент-заочник {FullName}, получает {Payment}";


    }



    class Program
    {
        static void Main(string[] args)
        {


            Func<string, string> sortWords = s => //delegate(string s)
            {
                var words = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Array.Sort(words, (l, r) => l.CompareTo(r));
                return string.Join(' ', words);
            };


            Func<string, string> getEachFirst = (s) => new string(s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => s.First()).ToArray());
 
            Func<string, string> algo = s => getEachFirst(sortWords(s)).ToUpper();

            Action<string> printOut = (s) => Console.WriteLine(s);
 
            
            printOut(algo("Проверка алгоритма слово слово   еще слово"));



            
            var director = new Director();

          
            

            var workers = new List<Worker>
            {
                new Turner("Токарь", 2000), new Turner("Токарь1", 1900),
                new Turner("Токарь2", 1800), new Turner("Токарь3", 2100),
                new PartTimeStudent("Заочник", 1100), new PartTimeStudent("Заочник1", 1150),
                new PartTimeStudent("Заочник2", 900), new PartTimeStudent("Заочник3", 1000)

            };



            Console.WriteLine("Рабочие до событий: ");
            workers.ForEach(Console.WriteLine);
            Console.WriteLine();

            foreach (var worker in workers)
            {

                var rand = new Random();

                switch ((rand.Next(2), rand.Next(2)))
                {
                    case (1, 0):
                        director.OnRaise += worker.PaymentChangeHandler;
                        break;
                    case (0, 1):
                        director.OnFine += worker.PaymentChangeHandler;
                        break;
                    case (1, 1):
                        director.OnRaise += worker.PaymentChangeHandler;
                        director.OnFine += worker.PaymentChangeHandler;
                        break;
                    default: break;
                }
            }


            director.Fine(15);


            Console.WriteLine("Рабочие после штрафа на 15: ");
            workers.ForEach(Console.WriteLine);
            Console.WriteLine();


            director.Raise(20);


            Console.WriteLine("Рабочие после повышения зарплат на 20: ");
            workers.ForEach(Console.WriteLine);
            Console.WriteLine();






        }


    }
}
