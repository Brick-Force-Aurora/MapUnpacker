using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickForceDevTools
{
    class MapHeader
    {
        public string mapName;
        public string creator;
        public int brickCount;
        public int mapID;
        public DateTime date;
        public ushort modeMask;

        public MapHeader(RegMap map)
        {
            mapName = map.alias;
            creator = map.developer;
            brickCount = map.geometry.brickCount;
            mapID = map.map;
            date = map.regDate;
            modeMask = map.modeMask;
        }
    }
}
