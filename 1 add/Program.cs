using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _1_add
{//Напишите консольное приложение, позволяющие пользователю зарегистрироваться под
 //«Логином», состоящем только из символов латинского алфавита, и пароля, состоящего из
 //цифр и символов.
        
    class Program
    {
        static void Main(string[] args)
        {
            var regexLogin = new Regex(@"^[A-Za-z]+$"); //(@"^[qwertyuiopasdfghjklzxcvbnm]+$"); //от начала строки, любые символы из списка, в кол-ве 1 и больше, до конца строки 
            var regexPass = new Regex(@"^[A-Za-z0-9]+$");   //(@"\w+");

            Dictionary<string, string> account = new Dictionary<string, string>
            {
               // ["Франция"] = "Париж",
            };
            string @login = null, @pass = null, loginInput, passInput;
            while (true)
            {
                Console.WriteLine("Ведите логин ");
                loginInput = Console.ReadLine();
                if (  regexLogin.IsMatch( loginInput )  )
                {
                    @login = loginInput;
                }
                else 
                { Console.WriteLine($"Введены недопустимые символы в {loginInput}"); continue; }
               
                Console.WriteLine("Ведите пароль ");
                passInput = Console.ReadLine();
                if ( regexPass.IsMatch(passInput) )
                {
                    @pass = passInput;
                }
                else
                { Console.WriteLine($"Введены недопустимые символы в {passInput}"); continue; }

                account.Add(@login, @pass);

                Console.WriteLine("--------");
                Console.WriteLine("Логин - Пароль");
                Console.WriteLine();
                foreach (var pair in account)
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
                Console.WriteLine("--------");
            }
        }
    }
}
