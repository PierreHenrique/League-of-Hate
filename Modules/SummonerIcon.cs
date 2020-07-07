using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_of_Hate.Modules
{
    class SummonerIcon : Module
    {
        public override string Name => "Alterar o icone";

        public override async Task<bool> Execute()
        {
            Debug.Write("- Informe o ID do icone: ");
            var id = Console.ReadLine();

            if (int.TryParse(id, out int result))
            {
                Process.Start();

                var response = await Connection.Put("lol-summoner/v1/current-summoner/icon", new JData.SummonerIcon {ProfileIconId = result});

                if (response != null)
                {
                    Debug.Write("- Icone alterado");
                    await Task.Delay(500);
#if DEBUG
                    Debug.Write($"[Output] - {response}");
#endif

                    return true;
                }
                else
                {
                    Debug.Write("[Erro] - Icone invalido", true);
#if DEBUG
                    Debug.Write($"[Output] - {response}");
#endif
                    return await Execute();
                }
            }
            else
            {
                Debug.Clean();
                return await Execute();
            }
        }
    }
}
