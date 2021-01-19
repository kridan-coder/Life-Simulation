using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Barn
    {
        public static int AmountOfActiveBarns;
        public bool Empty = true;

        public int StonksMax = 0;

        public int startX, startY;

        public int HowManyTicksBuild;

        private int currBuilding = 0;

        private int currBarnRange = 0;

        public bool HasBuilt;
        public bool Deserted;
        public List<BarnPart> BarnParts = new List<BarnPart>();
        public List<Entity> Stonks = new List<Entity>();

        BarnSentry barnSentry;
        public Barn(BarnSentry barnSentry)
        {
            this.barnSentry = barnSentry;
        }

        public void CreatePart(BarnPart part)
        {
            BarnParts.Add(part);
            barnSentry.PartWasMadeOnCell(part);
        }

        private void firstTick()
        {
            BarnPart firstPart = new BarnPart(startX, startY, this);
            CreatePart(firstPart);
        }
        public void NextTick()
        {
            if (currBuilding < HowManyTicksBuild)
            {
                currBuilding++;
                makeParts(++currBarnRange);
            }
            else
            {
                HasBuilt = true;
            }
        }

        private void makeParts(int currBarnRange)
        {
            // top
            for (int i = startX - currBarnRange; i < startX + currBarnRange; i++)
            {
                if (barnSentry.CanPlacePartOnCell((i, startY - currBarnRange)))
                {
                    CreatePart(new BarnPart(i, startY - currBarnRange, this));
                }
            }
            // right
            for (int i = startY - currBarnRange; i < startY + currBarnRange; i++)
            {
                if (barnSentry.CanPlacePartOnCell((startX + currBarnRange, i)))
                {
                    CreatePart(new BarnPart(startX + currBarnRange, i, this));
                }
            }
            // bottom
            for (int i = startX + currBarnRange; i > startX - currBarnRange; i--)
            {
                if (barnSentry.CanPlacePartOnCell((i, startY + currBarnRange)))
                {
                    CreatePart(new BarnPart(i, startY + currBarnRange, this));
                }
            }
            // left
            for (int i = startY + currBarnRange; i > startY - currBarnRange; i--)
            {
                if (barnSentry.CanPlacePartOnCell((startX - currBarnRange, i)))
                {
                    CreatePart(new BarnPart(startX - currBarnRange, i, this));
                }
            }
        }

        public void BarnSummon(int _howManyTicksBuild, int _startX, int _startY)
        {
            AmountOfActiveBarns++;
            startX = _startX;
            startY = _startY;
            HowManyTicksBuild = _howManyTicksBuild;
            StonksMax = HowManyTicksBuild * 10 + 3;
            firstTick();
        }

        public bool CanPlacePartOnCell((int, int) XY)
        {
            return barnSentry.CanPlacePartOnCell(XY);
        }
    }
}
