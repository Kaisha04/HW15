/*Використовуючи Visual Studio, створіть проект за шаблоном Console Application. 
    Створіть клас Calculator. 
    У тілі класу створіть чотири методи для арифметичних дій: Add – додавання, Sub – віднімання, Mul – множення, Div – розподіл. Метод поділу повинен перевірити поділ на нуль, якщо перевірка не проходить, згенерувати виняток.
    Користувач вводить значення, над якими хоче зробити операцію та вибрати саму операцію. У разі виникнення помилок повинні викидатися винятки.*/

using System;

namespace Task5
{
    class Program
    {
        static void Main()
        {
            double firstOperand = InputHandler.GetNumber("Введіть перший операнд");
            char operation = InputHandler.GetOperation();
            double secondOperand = InputHandler.GetNumber("Введіть другий операнд");

            Console.WriteLine("\nРезультат обчислення:");
            try
            {
                double result = 0;

                switch (operation)
                {
                    case '+': result = Calculator.Add(firstOperand, secondOperand); break;
                    case '-': result = Calculator.Sub(firstOperand, secondOperand); break;
                    case '*': result = Calculator.Mul(firstOperand, secondOperand); break;
                    case '/': result = Calculator.Div(firstOperand, secondOperand); break;
                    default:
                        throw new InvalidOperationException("Невідома операція! Використовуйте тільки +, -, *, /.");
                }
                Console.WriteLine($"{firstOperand} {operation} {secondOperand} = {result}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Помилка математики: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Сталася помилка: " + ex.Message);
            }
        }
    }

    public static class InputHandler
    {
        public static double GetNumber(string message)
        {
            double result;
            bool isParsed = false;
            do
            {
                Console.Write(message + ": ");
                isParsed = double.TryParse(Console.ReadLine(), out result);

                if (!isParsed)
                {
                    Console.WriteLine("Це не схоже на число. Спробуйте ще раз.");
                }
            }
            while (!isParsed);
            return result;
        }
        public static char GetOperation()
        {
            char result;
            bool isParsed = false;
            do
            {
                Console.Write("Введіть знак операції (+, -, *, /): ");
                isParsed = char.TryParse(Console.ReadLine(), out result);

                if (!isParsed)
                {
                    Console.WriteLine("Невірний формат. Введіть лише один символ.");
                }
            }
            while (!isParsed);
            return result;
        }
    }

    public static class Calculator
    {
        public static double Add(double first, double second) => first + second;
        public static double Sub(double first, double second) => first - second;
        public static double Mul(double first, double second) => first * second;

        public static double Div(double first, double second)
        {
            if (second == 0)
            {
                throw new DivideByZeroException("Ділення на нуль заборонено!");
            }

            return first / second;
        }
    }
}