using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace _2_find_on_the_site
{//Напишите программу, которая бы позволила вам по указанному адресу web-страницы
 // выбирать все ссылки на другие страницы, номера телефонов, почтовые адреса и сохраняла
// полученный результат в файл.
    class Program
    {
        static void Main(string[] args)
        {
            WebRequest request = WebRequest.Create("https://sharp.com.ua/ru/contacts");  //https://kmb.ua/ru/contacts
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            reader.Close();
            response.Close();
            
            StreamWriter streamWriter = File.CreateText(@"test 2.txt");

            var regex = new Regex(@"https://(?<link>\S+) ");
            foreach (Match m in regex.Matches(text))
            {
                streamWriter.WriteLine("link: {0,-40}", "https://" + m.Groups["link"]);
            }

            regex = new Regex(@"(?<phone>[+38(0-9) ]{2,10}[0-9]{3}[ -][0-9]{2}[ -][0-9]{2})");
            foreach (Match m in regex.Matches(text))
            {
                streamWriter.WriteLine("Телефон: {0,-40}", m.Groups["phone"]);
            }
            
            regex = new Regex(@"[a-zA-Z]+@[a-zA-Z]+\.[a-zA-Z]{2,4}"); //@"(?<email>[a-z]+@[a-z]+[.][a-z]{2,4})"
            foreach (Match m in regex.Matches(text))
            {
                streamWriter.WriteLine("Почта: {0,-40}", m.Value);
            }
            streamWriter.Close();
        }
    }
}
