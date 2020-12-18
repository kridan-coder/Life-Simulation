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
                for (int i = 0; i < Houses.Count() - 1; i++)
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

        public void AddAndSummonHouse(int hostPower, int hostChosenX, int hostChosenY, List<Human> owners)
        {

            Houses.Add(new House(this));
            Houses[Houses.Count() - 1].HouseSummon(hostPower, hostChosenX, hostChosenY, owners);
            //new List<Human>() { Host, Hostess }
                //if (!Meteorites[i].Active)
                //{
                //    Meteorites[i] = new Meteorite(this);
                //    Meteorites[i].MeteoriteSummon(Random.Next(maxTicksMeteoriteFalling) + 1, Random.Next(maxTicksMeteoriteCracking) + 1, Random.Next(maxTicksMeteoriteBeforeDissolving) + 1, mainSentry.GetRandCoordsOnMap().Item1, mainSentry.GetRandCoordsOnMap().Item2);
                //}
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
