using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class MeteoriteShard : Entity
    {
        public Meteorite meteorite;

        public MeteoriteShard(int _x, int _y, Meteorite meteorite) : base(_x, _y)
        {
            this.meteorite = meteorite;
        }

        public static MeteoriteShard SetShard((int, int) XY, Meteorite meteorite)
        {
            if (meteorite.CanPlaceShardOnCell(XY))
                return new MeteoriteShard(XY.Item1, XY.Item2, meteorite);
            return null;  
        }

    }
}
