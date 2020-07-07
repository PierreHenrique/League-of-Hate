using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_of_Hate.Modules
{
    class CloseApplication : Module
    {
        public override string Name => "Fechar a aplicação";

        public override async Task<bool> Execute()
        {
            Environment.Exit(0);

            return true;
        }
    }
}
