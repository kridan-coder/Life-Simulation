using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class MeteoriteShard : Entity
    {
        public int meteoritePieceID;
        private static int lastMeteoritePieceID;

        public Meteorite meteorite;

        public Meteorite MyFatherMeteorite()
        {
            return meteorite;
        }
        public MeteoriteShard TryToPlace(int x, int y)
        {
            if (map.CheckBorders(x,y) && !map.IsOnCell<MeteoriteShard>(x, y))
                return new MeteoriteShard(x, y, map, meteorite);
            return null;  
        }
        public MeteoriteShard(int _x, int _y, Map _map, Meteorite _meteorite) : base(_x, _y, _map)
        {
            meteorite = _meteorite;
            meteoritePieceID = lastMeteoritePieceID++;
        }
    }
}
