using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using League_of_Hate.Modules;

namespace League_of_Hate
{
    public class Bootstrap
    {
        public static bool IsAlreadyOpen = false;
        public static Connection Connection;
        private static readonly List<Module> Modules = new List<Module>
        {
            new CloseApplication(),
            new SummonerIcon(),
            new TeamBoost()
        };

        static async Task Main(string[] args)
        {
            Console.Title = "League of Hate";

            Connection = new Connection();
            await Connection.TryConnect();

            await ShowMenu();
        }

        public static async Task<bool> ShowMenu()
        {
            var currentSummoner = await Connection.Get("lol-summoner/v1/current-summoner");

            Debug.Write($"Summoner: {currentSummoner["displayName"]}", true);
            Debug.Write("");
            for (int i = 0; i < Modules.Count; i++)
            {
                var j = Modules[i];

                Debug.Write($"[{i}] - {j.Name}");
            }

            var option = Console.ReadLine();

            if (int.TryParse(option, out int response))
            {
                try
                {
                    Debug.Clean();
                    var result = await Modules[response].Execute();

                    if (result)
                        await ShowMenu();
                }
                catch
                {
                    Debug.Write("[Erro] - Opção invalida");
                    await Task.Delay(500);
                    await ShowMenu();
                }
            }
            else
            {
                Debug.Write("[Erro] - Opção invalida", true);
                await Task.Delay(500);
                await ShowMenu();
            }

            return true;
        }
    }
}
