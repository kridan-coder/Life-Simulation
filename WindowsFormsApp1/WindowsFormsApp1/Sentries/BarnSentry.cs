using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class BarnSentry
    {
        public List<Barn> Barns = new List<Barn>();

        MainSentry mainSentry;

        public Random Random;
        public BarnSentry(MainSentry mainSentry)
        {
            this.mainSentry = mainSentry;
            Random = mainSentry.Random;
        }
        public void NextTick()
        {
            if (Barns.Count() != 0)
            {
                for (int i = 0; i < Barns.Count(); i++)
                {
                    barnNextTick(Barns[i]);
                }
            }
        }

        private void barnNextTick(Barn barn)
        {
            if (!barn.HasBuilt)
                barn.NextTick();
        }

        public Barn AddAndSummonBarn(int hostPower, int hostChosenX, int hostChosenY, List<Human> owners)
        {
            Barn brandNewHome = new Barn(this);
            brandNewHome.BarnSummon(hostPower, hostChosenX, hostChosenY, owners);
            Barns.Add(brandNewHome);
            return brandNewHome;
            //new List<Human>() { Host, Hostess }
        }

        public void PartWasMadeOnCell(BarnPart part)
        {
            mainSentry.EntityWasMadeOnCell(part);
        }

        public bool CanPlacePartOnCell((int, int) XY)
        {
            return mainSentry.CheckBorders(XY) && mainSentry.CellIsEmpty(XY);
        }
    }
}
