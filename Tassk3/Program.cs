/*Використовуючи Visual Studio, створіть проект за шаблоном Console Application. Потрібно описати структуру з іменем Price, що містить такі поля:
• назва товару;
• назва магазину, де продається товар;
• вартість товару у гривнях.
Написати програму, яка виконує такі дії:
• введення з клавіатури даних до масиву, що складається з двох елементів типу Price (записи мають бути впорядковані в алфавітному порядку за назвами магазинів);
• виведення на екран інформації про товари, що продаються в магазині, назва якого введена з клавіатури (якщо такого магазину немає, вивести виняток).*/
using System;
using System.Globalization;
using System.Text;

namespace Task3;

class Program
{
    static void Main()
    {
        CultureInfo ukraine = new CultureInfo("uk-UA");
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        Price[] array = new Price[2];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = new Price(
                InputHandler.GetText("Введіть назву продукту"),
                InputHandler.GetText("Введіть назву магазину"),
                InputHandler.GetPrice("Введіть ціну товару")); 
        }

        array.SortByLetter();

        try
        {
            string price = array.FindMarket(InputHandler.GetText("Введіть назву магазину")).ToString();
            Console.WriteLine(price);
        }catch(WrongMarketException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}



public static class InputHandler
{   
    public static string GetText(string whatToType)
    {
        Console.Write(whatToType + ": ");
        string result = Console.ReadLine();
        return result;
    }

    public static double GetPrice(string whatToType)
    {
        bool tryParsePrice = false;

        do
        {
            Console.Write(whatToType + ": ");
            tryParsePrice = double.TryParse(Console.ReadLine(), out double result);

            if (tryParsePrice) return result;
            Console.Clear();
            Console.WriteLine("Невірне значення");
        }
        while (true);
    }
}

public static class PriceExtension
{
    public static Price[] FindMarket(this Price[] array, string targetMarket)
    {
        int countOfMarkets = 0;
        Price[] newArray = new Price[0];
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].MarketName.Equals(targetMarket, StringComparison.OrdinalIgnoreCase))
            {
                countOfMarkets++;
                Price[] temporaryArray = new Price[newArray.Length + 1];
                for (int j = 0; j < newArray.Length; j++)
                {
                    temporaryArray[j] = newArray[j];
                }
                temporaryArray[temporaryArray.Length - 1] = array[i];
                newArray = temporaryArray;
            }
        }
        if (countOfMarkets > 0)
        {
            return newArray;
        }
        throw new WrongMarketException($"Такого магазину як {targetMarket}, немає..");
    }
    public static Price[] SortByLetter(this Price[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 1; j < array.Length - i; j++)
            {
                if (Char.ToUpper(array[j].MarketName[0]) < Char.ToUpper(array[j - 1].MarketName[0])) (array[j] , array[j - 1]) = (array[j - 1] , array[j]);
            }
        }
        return array;
    }
}

public struct Price
{
    public string ProductName { get; set; }
    public string MarketName { get ; set; }
    public double ProductPrice { get; set; }

    public Price() { }

    public Price(string ProductName, string MarketName, double ProductPrice)
    {
        this.ProductName = ProductName;
        this.MarketName = MarketName;
        this.ProductPrice = ProductPrice;
    }
    public override string ToString()
    {
        return string.Format($"Мазагин: {MarketName}\nТовар: {ProductName}\nЦіна: {ProductPrice.ToString("C")}");
    }
}