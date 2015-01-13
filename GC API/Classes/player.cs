using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC_API
{
    //Написать про бейджи
    class player
    {
        static string result;
        public player(string a)
        {
            result = a.Replace(",", "");
        }
        static string name()
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

        static bool status()
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

        static string date()
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

        static bool activ()
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

        static string prefix()
        {
            int i = result.IndexOf("prefix");
            string a = "";
            while (result[i + 8] != '\n')
            {
                a = a + result[i + 8];
                i++;
            }
            if (a != "null")
            {
                i = result.IndexOf("[");
                a = "";
                while (result[i - 1] != ']')
                {
                    a = a + result[i];
                    i++;
                }
                if (a == "[&aHELP!&4]")
                    a = "[HELP!]";
                if (a == "[&1MOD&9]")
                    a = "[MOD]";
                if (a == "[&rff66c016G&rfff7f7f7C&r99446666]")
                    a = "[GC]";
            }
            else
                a = "";
            return a;
        }

        static bool banned()
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

        static string reg_date()
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
        static int gcyears()
        {
            const int tw = 1209600;
            int i = result.LastIndexOf("reg_date:");
            string a = "";
            while (result[i + 10] != '\n')
            {
                a = a + result[i + 10];
                i++;
            }
            int Usertime = int.Parse(a);
            int NowTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            int years = (NowTime - Usertime) / tw;
            return years;   
        }

        public void player_output()
        {

            Console.Write("\n" + prefix());
            Console.WriteLine(name());
            if (status() == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Игрок онлайн.");
            }
            else
            {
                Console.Write("Игрок заходил {0}", date());
                if (activ() == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("  Игрок активен.");
                }
                  else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("  Игрок неактивен.");
                }
                if (banned() == true)
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
            Console.WriteLine("Игрок зарегистрирован {0}. ({1} гк-года)", reg_date(), gcyears());

        }
    }
}
