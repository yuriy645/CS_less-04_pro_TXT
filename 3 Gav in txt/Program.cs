using System;
using System.IO;
using System.Text.RegularExpressions;

namespace _3_Gav_in_txt
{//Напишите шуточную программу «Дешифратор», которая бы в текстовом файле могла бы
 //   заменить все предлоги на слово «ГАВ!».
    class Program
    {
        static void Main(string[] args)
        {

           
            string filepath = Directory.GetCurrentDirectory();
            filepath += @"\test.txt";
            string txt = "Часть предлогов, в основном производных, совмещают ряд значений. Так, предлоги за, под, из, от, в, на совмещают причинные, " +
                "пространственные и временные значения. Предлог через, выражая пространственные (через горы) и временные (через века) отношения, в " +
                "просторечии встречается при выражении причинных отношений (через тебя я лишился семьи). Другие предлоги совмещают причинные " +
                "значения со значениями цели, например для, по, а.";

            // Создание файла.
            FileStream file = File.Create(filepath); //метод Create возвращает уже поток
            var writer = new StreamWriter(file); //конфигурируем поток на запись
            writer.WriteLine(txt);
            writer.Close();
            file.Close();

            FileStream file1 = File.Open(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            var reader = new StreamReader(file1); //конфигурируем поток на чтение
            string txtIn = reader.ReadToEnd();  // Читаем до конца 
            reader.Close();
            //file.Close(); // Закрывать не обязательно так как reader закроет сам.

            Console.WriteLine($"вывод оригинала \n{txtIn}\n") ;

            //в, без, до, для, за, через, над, по, из, у, около, под, о, про, на, к, перед, при, с, между
            string pattern = @"[( ]а[.,) ]|[( ]в[.,) ]|[( ]без[.,) ]|[( ]до[.,) ]|[( ]для[.,) ]|[( ]за[.,) ]|[( ]через[.,) ]|[( ]над[.,) ]|[( ]по[.,) ]|[( ]из[.,) ]| у[.,) ]|[( ]около[.,) ]|[( ]под[.,) ]| о[.,) ]|[( ]про[.,) ]|[( ]на[.,) ]|[( ]к[.,) ]|[( ]перед[.,) ]|[( ]при[.,) ]|[( ]с[.,) ]|[( ]между[.,) ]";
            
            //string pattern = @"(?<![_\d\p{L}])[а-я]{1,3}(?![_\d\p{L}]) | (?<![_\d\p{L}])[А-Я]{1,3}(?![_\d\p{L}])";
            
            string txtOut = Regex.Replace(txtIn, pattern, " ГАВ "); // не плохо бы сюда вернуться и сделать "чистую" замену с учетом знаков препинания

            // Создание файла.
            file = File.Create(filepath); //метод Create возвращает уже поток
            writer = new StreamWriter(file); //конфигурируем поток на запись
            writer.WriteLine(txtOut);
            writer.Close();
            file.Close();
            Console.WriteLine(txtOut);
        }
    }
}
