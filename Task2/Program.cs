using System;
using System.Globalization;
using System.Text;

namespace Task2
{
    public static class ExtensionMethod
    {
        public static Worker[] Sort(this Worker[] array)
        {
            Worker temporaryWorker;
            for (int i = 0; i < array.Length - 1; i++)
            {
                // Починаємо з 1, порівнюємо i та i-1
                for (int q = 1; q < array.Length - i; q++)
                {
                    if (array[q - 1].FullName[0] > array[q].FullName[0])
                    {
                        temporaryWorker = array[q];
                        array[q] = array[q - 1];
                        array[q - 1] = temporaryWorker;
                    }
                }
            }
            return array;
        }
    }

    public struct Worker
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public DateTime Year { get; set; }

        public Worker(string fullName, string position, DateTime year)
        {
            FullName = fullName;
            Position = position;
            Year = year;
        }

        public void Show()
        {
            Console.WriteLine($"Прізвище: {FullName}, Посада: {Position}, Рік: {Year.Year}");
        }
    }

    class Program
    {
        static void Main()
        {

            Console.InputEncoding = Encoding.UTF8;
                Console.OutputEncoding = Encoding.UTF8; 
            Worker[] array = new Worker[5];
            Console.WriteLine("-----Заповнюємо масив-----");

            for (int i = 0; i < array.Length; i++)
            {
                string name = GetText("Введіть прізвище та ініціали:");
                string position = GetText("Введіть посаду:");
                DateTime year = GetYear();
                array[i] = new Worker(name, position, year);
            }

            array.Sort();

            Console.WriteLine("\nВведіть рік, щоб знайти працівників із більшим стажем:");
            int targetYear = int.Parse(Console.ReadLine());

            Console.WriteLine("\nРезультат:");
            foreach (var w in array)
            {
                if (w.Year.Year < targetYear)
                {
                    w.Show();
                }
            }
        }

        static string GetText(string arg)
        {
            Console.WriteLine(arg);
            return Console.ReadLine();
        }

        static DateTime GetYear()
        {
            while (true)
            {
                Console.WriteLine("Введи дату в форматі dd.MM.yyyy:");
                string format = "dd.MM.yyyy";
                try
                {
                    string inputDate = Console.ReadLine();
                    if (!DateTime.TryParseExact(inputDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                    {
                        throw new DataFormatExeption("Невірний формат");
                    }

                    return parsedDate;
                }
                catch (DataFormatExeption e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}