using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyHttp.Http;
using System.Threading;

namespace GC_API
{

    class Program
    {
        //Эта прекрасная функция отправляет запрос в api.
        static string get(string type, string name)
        {
            var http = new HttpClient();
            http.Request.Accept = HttpContentTypes.ApplicationJson;
            string url = "https://api.greencubes.org" + type + "/" + name;
            var result = http.Get(url);
            string ans = Convert.ToString(result.RawText);
            ans = ans.Replace("{", "");
            ans = ans.Replace("}", "");
            ans = ans.Replace("\"", "");
            return ans;
        }

        //Мейн надо сократить и разнести его по отдельным функциям.
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            string name = "";
            string type, res;
            res = get("/main", "status");
            main_stat stat = new main_stat(res);
            stat.status();  //Статус сервера. 
            //Вот этот цикл обеспечивает ввод данных игроком.
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Чтобы посмотеть список пользовотелей, нажми 1.\nЧтобы посмотеть инофрмацию об игрокуe, нажми 2.\nЧтобы выйти нажми e.\n");
                string choose = Convert.ToString(Console.ReadLine());
                switch (choose)
                {
                        //Все онлайн юзеры. Добавить их кол-во
                    case "1":
                        type = @"/main";
                        name = @"online";
                        res = get(type, name);
                        main_list m = new main_list(res);
                        Console.WriteLine(m.list());
                        break;
                        //Вся инфа о юзере. Добавить бейджи
                    case "2":
                        type = @"/users";
                        Console.WriteLine("Введите ник игрока: \n");
                        name = Console.ReadLine();
                        res = get(type, name);
                        player user = new player(res);
                        if (res.IndexOf("message") == -1)
                        {
                            user.player_output();
                        }
                        else
                            Console.WriteLine("Такого игрока не существует.");
                        break;
                }
                if (choose == "e")
                    break;
            }
        }
    }
}
