using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Text;

namespace _4_check
{//Создайте текстовый файл-чек по типу «Наименование товара – 0.00 (цена) грн.» с
  //  определенным количеством наименований товаров и датой совершения покупки.Выведите на
 //   экран информацию из чека в формате текущей локали пользователя и в формате локали en-US.
    class Program
    {
        static void Main(string[] args)
        {
            string filepath = @"test 4.txt";
            Console.WriteLine("Оригинал");
            // Создание файла.
            FileStream file = File.Create(filepath); //метод Create возвращает уже поток
            var writer = new StreamWriter(file); //конфигурируем поток на запись
            writer.WriteLine("Кассовый чек");
            writer.WriteLine("Хлеб - 1шт. 20.00 ");
            writer.WriteLine("Яблоки - 1кг 35.00 ");
            writer.WriteLine("Сгущенка - 1паллета 10435.00 ");
            writer.WriteLine("Дата: 18-06-2021");
            writer.WriteLine("Время: 18:05:32");
            writer.Close();
            file.Close();

            string text = File.ReadAllText(filepath, Encoding.UTF8);
            Console.WriteLine(text);

            var my = new CultureInfo("ru-RU");
            var us = new CultureInfo("en-US");

            string pattern1 = @"[0-9]+[.,][0-9]+";
            string txt1 = Regex.Replace(text, pattern1, "*"); // замена цен
            string txtMy1 = Regex.Replace(text, pattern1, (m) => double.Parse(m.Value.Replace('.', ',')).ToString("C", my));
            string txtUS1 = Regex.Replace(text, pattern1, (m) => double.Parse(m.Value.Replace('.', ',')).ToString("C", us));

            Console.WriteLine("Замена только цен");
            Console.WriteLine();
            Console.WriteLine("В формате ru-RU");
            Console.WriteLine(txtMy1);
            Console.WriteLine("В формате en-US");
            Console.WriteLine(txtUS1);

            Console.WriteLine("Замена даты");
            string pattern2 = @"Дата: (?<dat>\S+)";
            DateTime date = new DateTime();
            var regex = new Regex(pattern2); // замена даты и времени //\S+ любое кол-во непробельных символов
            for (Match m = regex.Match(text); m.Success; m = m.NextMatch())// как только в переменной нашелся текст, соотв-щий шаблону, вывести этот текст
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Вытягивание даты: " + m.Groups["dat"]);// имена внутришаблонных переменных
                date = DateTime.Parse( m.Groups["dat"].Value ); // взяли dat в виде string и распарсили в DateTime
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            
            Console.WriteLine($"Дата типа DateTime {date}");
            Console.WriteLine();
            
            Console.WriteLine("Дата в формате ru-RU");
            Console.WriteLine(Regex.Replace(txtMy1, pattern2, (m) => "Дата: " + date.ToString("d", new CultureInfo("ru-RU"))));
            Console.WriteLine("Дата в формате en-US");
            Console.WriteLine(Regex.Replace(txtUS1, pattern2, (m) => "Дата: " + date.ToString("d", new CultureInfo("en-US"))));



        }
    }
}
