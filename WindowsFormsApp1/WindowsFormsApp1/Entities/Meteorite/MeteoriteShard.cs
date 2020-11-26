using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class MeteoriteShard : Entity
    {
        public MeteoriteSentry meteoriteSentry;

        public MeteoriteShard(int _x, int _y, MeteoriteSentry meteoriteSentry) : base(_x, _y)
        {
            this.meteoriteSentry = meteoriteSentry;
        }

        public static MeteoriteShard SetShard((int, int) XY, MeteoriteSentry meteoriteSentry)
        {
            if (meteoriteSentry.CanPlaceShardOnCell(XY))
                return new MeteoriteShard(XY.Item1, XY.Item2, meteoriteSentry);
            return null;  
        }

    }
}
