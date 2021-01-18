using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class BarnPart : Entity
    {
        public Barn barn;

        public BarnPart(int _x, int _y, Barn barn) : base(_x, _y)
        {
            this.barn = barn;
        }

        public static BarnPart SetPart((int, int) XY, Barn barn)
        {
            if (barn.CanPlacePartOnCell(XY))
                return new BarnPart(XY.Item1, XY.Item2, barn);
            return null;  
        }

    }
}
