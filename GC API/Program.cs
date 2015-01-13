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
        
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            string type = "/users";
            Console.WriteLine("Введите ник пользователя:");
            string name = Console.ReadLine();
            while(name!="Y")
            {
            string res = get(type, name);
            player user = new player(res);
            if (res.IndexOf("message") == -1)
            {
                user.player_output();
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
