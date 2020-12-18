using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class HousePart : Entity
    {
        public House house;

        public HousePart(int _x, int _y, House house) : base(_x, _y)
        {
            this.house = house;
        }

        public static HousePart SetPart((int, int) XY, House house)
        {
            if (house.CanPlacePartOnCell(XY))
                return new HousePart(XY.Item1, XY.Item2, house);
            return null;  
        }

    }
}
