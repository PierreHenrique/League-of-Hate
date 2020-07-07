using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_of_Hate.Modules
{
    public abstract class Module
    {
        public Connection Connection => Bootstrap.Connection;
        public virtual string Name { get; set; }

        public virtual async Task<bool> Execute()
        {
            return false;
        }
    }
}
