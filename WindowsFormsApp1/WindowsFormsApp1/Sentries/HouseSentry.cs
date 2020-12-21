using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class HouseSentry
    {
        public List<House> Houses = new List<House>();

        MainSentry mainSentry;

        public Random Random;
        public HouseSentry(MainSentry mainSentry)
        {
            this.mainSentry = mainSentry;
            Random = mainSentry.Random;
        }
        public void NextTick()
        {
            if (Houses.Count() != 0)
            {
                for (int i = 0; i < Houses.Count(); i++)
                {
                    houseNextTick(Houses[i]);
                }
            }
        }

        private void houseNextTick(House house)
        {
            if (!house.HasBuilt)
                house.NextTick();
        }

        public House AddAndSummonHouse(int hostPower, int hostChosenX, int hostChosenY, List<Human> owners)
        {
            House brandNewHome = new House(this);
            brandNewHome.HouseSummon(hostPower, hostChosenX, hostChosenY, owners);
            Houses.Add(brandNewHome);
            return brandNewHome;
            //new List<Human>() { Host, Hostess }
        }

        public void PartWasMadeOnCell(HousePart part)
        {
            mainSentry.EntityWasMadeOnCell(part);
        }

        public bool CanPlacePartOnCell((int, int) XY)
        {
            return mainSentry.CheckBorders(XY) && mainSentry.CellIsEmpty(XY);
        }
    }
}
