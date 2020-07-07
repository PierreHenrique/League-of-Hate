using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_of_Hate.Modules
{
    class TeamBoost : Module
    {
        public override string Name => "Ativar boost (Skin Aram) para equipe";

        public override async Task<bool> Execute()
        {
            var response = await Connection.Post("lol-champ-select/v1/team-boost/purchase", null);

            if (response != null)
            {
                Debug.Write("- Boost ativado");
                await Task.Delay(500);

                return true;
            }
            else
            {
                Debug.Write("[Erro] - Falha ao ativar o boost", true);

#if DEBUG
                Debug.Write($"[Output] - {response}");
#endif

                return await Execute();
            }
        }
    }
}
