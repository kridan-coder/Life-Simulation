using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class House
    {
        public static int AmountOfActiveHouses;
        public bool Empty = true;

        public int StonksMax = 0;

        public int startX, startY;

        public int HowManyTicksBuild;

        private int currBuilding = 0;

        private int currHouseRange = 0;

        public bool HasBuilt;
        public bool Deserted;
        public List<HousePart> HouseParts = new List<HousePart>();
        public List<Plant> Stonks = new List<Plant>();
        public List<Human> Owners;

        HouseSentry houseSentry;
        public House(HouseSentry houseSentry)
        {
            this.houseSentry = houseSentry;
        }

        public void CreatePart(HousePart part)
        {
            HouseParts.Add(part);
            houseSentry.PartWasMadeOnCell(part);
        }

        private void firstTick()
        {
            HousePart firstPart = new HousePart(startX, startY, this);
            CreatePart(firstPart);
        }
        public void NextTick()
        {
            if (currBuilding < HowManyTicksBuild)
            {
                currBuilding++;
                makeParts(++currHouseRange);
            }
            else
            {
                HasBuilt = true;
            }
        }

        private void makeParts(int currHouseRange)
        {
            // top
            for (int i = startX - currHouseRange; i < startX + currHouseRange; i++)
            {
                if (houseSentry.CanPlacePartOnCell((i, startY - currHouseRange)))
                {
                    CreatePart(new HousePart(i, startY - currHouseRange, this));
                }
            }
            // right
            for (int i = startY - currHouseRange; i < startY + currHouseRange; i++)
            {
                if (houseSentry.CanPlacePartOnCell((startX + currHouseRange, i)))
                {
                    CreatePart(new HousePart(startX + currHouseRange, i, this));
                }
            }
            // bottom
            for (int i = startX + currHouseRange; i > startX - currHouseRange; i--)
            {
                if (houseSentry.CanPlacePartOnCell((i, startY + currHouseRange)))
                {
                    CreatePart(new HousePart(i, startY + currHouseRange, this));
                }
            }
            // left
            for (int i = startY + currHouseRange; i > startY - currHouseRange; i--)
            {
                if (houseSentry.CanPlacePartOnCell((startX - currHouseRange, i)))
                {
                    CreatePart(new HousePart(startX - currHouseRange, i, this));
                }
            }
        }

        public void HouseSummon(int _howManyTicksBuild, int _startX, int _startY, List<Human> _owners)
        {
            AmountOfActiveHouses++;
            startX = _startX;
            startY = _startY;
            HowManyTicksBuild = _howManyTicksBuild;
            Owners = _owners;
            StonksMax = HowManyTicksBuild * 3;
            firstTick();
        }

        public bool CanPlacePartOnCell((int, int) XY)
        {
            return houseSentry.CanPlacePartOnCell(XY);
        }
    }
}
