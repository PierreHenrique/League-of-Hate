using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_of_Hate.Modules
{
    class RainbowIcon : Module
    {
        private static int _lastIcon = 50;

        public override string Name => "Icones aleatorios";

        public override async Task<bool> Execute()
        {
            _lastIcon += 1;

            if (_lastIcon > 78)
                _lastIcon = 50;

            var response = await Connection.Put("lol-summoner/v1/current-summoner/icon", new JData.SummonerIcon {ProfileIconId = _lastIcon});

            if (response != null)
            {
                Debug.Write("- Icone alterado");
                await Task.Delay(1500);
#if DEBUG
                    Debug.Write($"[Output] - {response}");
#endif

                return await Execute();
            }
            else
            {
                Debug.Write("[Erro] - Icone invalido", true);
                await Task.Delay(1500);
#if DEBUG
                    Debug.Write($"[Output] - {response}");
#endif
                return await Execute();
            }
        }
    }
}
