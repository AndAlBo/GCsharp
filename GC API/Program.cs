using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyHttp.Http;
using System.Threading;

namespace GC_API
{
    class player
    {
        static string result;
        public player(string a)
        {
            result = a;
        }
        public string name()
        {
            int i = result.IndexOf("username:");
            string a = "";
            while (result[i + 10] != '\n')
            {
                a = a + result[i + 10];
                i++;
            }
            return a;
        }

        public bool status()
        {
            int i = result.IndexOf("main:");
            string a = "";
            while (result[i + 6] != '\n')
            {
                a = a + result[i + 6];
                i++;
            }
            if (a == "true")
                return true;
            else
                return false;
        }

        public string date()
        {
            int i = result.LastIndexOf("main:");
            string a = "";
            while (result[i + 6] != '\n')
            {
                a = a + result[i + 6];
                i++;
            }
            DateTime pDate = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(int.Parse(a));
            string time = Convert.ToString(pDate);
            return time;
        }

        public bool activ()
        {
            const int aktivTime = 1814400;
            int i = result.LastIndexOf("main:");
            string a = "";
            while (result[i + 6] != '\n')
            {
                a = a + result[i + 6];
                i++;
            }
            int Usertime = int.Parse(a);
            int NowTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            if (NowTime - Usertime <= aktivTime)
                return true;
            else
                return false;
        }

        public string prefix()
        {
            int i = result.IndexOf("[");
            string a = "";
            while (result[i - 1] != ']')
            {
                a = a + result[i];
                i++;
            }
            if (a == "[]")
                a = "";
            if (a == "[&aHELP!&4]")
                a = "[HELP!]";
            if (a == "[&1MOD&9]")
                a = "[MOD]";
            if (a == "[&rff66c016G&rfff7f7f7C&r99446666]")
                a = "[GC]";
            return a;
        }

        public bool banned()
        {
            int i = result.IndexOf("banned:");
            string a = "";
            while (result[i + 8] != '\n')
            {
                a = a + result[i + 8];
                i++;
            }
            if (a == "true")
                return true;
            else
                return false;
        }

        public string reg_date()
        {
            int i = result.LastIndexOf("reg_date:");
            string a = "";
            while (result[i + 10] != '\n')
            {
                a = a + result[i + 10];
                i++;
            }
            DateTime pDate = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(int.Parse(a));
            string time = Convert.ToString(pDate);
            return time;
        }
    }


    class Program
    {
        static string get(string type, string name)
        {

            var http = new HttpClient();
            http.Request.Accept = HttpContentTypes.ApplicationJson;
            string url = "https://api.greencubes.org" + type + "/"+ name;
            var result = http.Get(url);
            string ans = Convert.ToString(result.RawText);
            ans = ans.Replace("{", "");
            ans = ans.Replace("}", "");
            ans = ans.Replace("\"", "");
            ans = ans.Replace(",", "");
            return ans;
        }

        static void output(string res)
        {
            player user = new player(res);
            Console.Write("\n" + user.prefix());
            Console.WriteLine(user.name());
            if (user.status() == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Игрок онлайн.");
            }
            else
            {
                Console.Write("Игрок заходил {0}", user.date());
                if (user.activ() == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("  Игрок активен.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("  Игрок неактивен.");
                }
                if (user.banned() == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Игрок забанен.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Игрок не забанен.");
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Игрок зарегистрирован {0}", user.reg_date());

        }
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            string type = "/users";
            Console.WriteLine("Введите ник пользователя:");
            string name = Console.ReadLine();
            while(name!="Y")
            {
            string res = get(type, name);
            if (res.IndexOf("message") == -1)
            {
                output(res);
            }
            else
                Console.WriteLine("Такого игрока не существует.");
            Console.WriteLine("Вы можете ввести ник ещё раз или выйти (Y).");
            //Console.WriteLine(res.IndexOf("message"));
            name = Console.ReadLine();
            }
        }
    }
}