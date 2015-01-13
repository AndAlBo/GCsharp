using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC_API
{

    //Этот класс содержит инфу о игроках в онлайне.
    //TODO:
    //Добавить подсчёт кол-ва игроков. Сделать отображение читаемым.

    class main_list
    {
        static string res;
        public main_list(string res1)
        {
            res = res1;
        }

        public string list()
        {
            string a = res.Replace("\n", "");
            a = a.Replace("[", "");
            a = a.Replace("]", "");
            a = a.Replace(" ", "");
            a = a.Replace(",", " ");
            return a;
        }
    }

    //А вот тут статус сервера.

    class main_stat
    {
        static string res;
        public main_stat(string res1)
        {
            res = res1;
        }

        public void status()
        {
            int i = res.IndexOf("status:");
            string a = "";
            while (res[i + 8] != ',')
            {
                a = a + res[i + 8];
                i++;
            }
            if (a == "true")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Сервер онлайн.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Сервер оффлайн.");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}