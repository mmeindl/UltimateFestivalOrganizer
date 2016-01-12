using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.Server
{
    public enum BLType { Lokal, WebService }

    public static class UFOServerFactory
    {
        private static IDictionary<BLType, IUFOServer> dictionary = new Dictionary<BLType, IUFOServer>();

        public static IUFOServer GetUFOServer(BLType type = BLType.Lokal)
        {
            IUFOServer server;

            if (!dictionary.TryGetValue(type, out server))
            {
                switch (type)
                {
                    case BLType.Lokal:
                        server = new UFOServerImpl();
                        break;
                    case BLType.WebService:
                        server = new UFOWebServiceImpl();
                        break;
                    default:
                        throw new ArgumentException("Invalid BLType");
                }

                dictionary[type] = server;
            }

            return server;
        }
    }
}
