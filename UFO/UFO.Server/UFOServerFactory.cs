using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.Server
{
    public static class UFOServerFactory
    {
        private static IUFOServer server;

        public static IUFOServer GetUFOServer()
        {
            if (server == null)
                server = new UFOServerImpl();
            return server;
        }
    }
}
