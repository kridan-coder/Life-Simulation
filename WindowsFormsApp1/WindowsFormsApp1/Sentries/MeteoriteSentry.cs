using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class MeteoriteSentry
    {
        public List<Meteorite> Meteorites = new List<Meteorite>();

        MainSentry mainSentry;

        private int maxTicksMeteoriteFalling;
        private int maxTicksMeteoriteCracking;
        private int maxTicksMeteoriteBeforeDissolving;

        private int maxAmountOfMeteoritesFallingSimultaneously;
        private int chanceOfMeteoriteToFallOnMap;

        public Random Random;
        public MeteoriteSentry(int maxTicksMeteoriteFalling, int maxTicksMeteoriteCracking, int maxTicksMeteoriteBeforeDissolving, int maxAmountOfMeteoritesFallingSimultaneously, int chanceOfMeteoriteToFallOnMap, MainSentry mainSentry)
        {
            this.maxTicksMeteoriteFalling = maxTicksMeteoriteFalling;
            this.maxTicksMeteoriteCracking = maxTicksMeteoriteCracking;
            this.maxTicksMeteoriteBeforeDissolving = maxTicksMeteoriteBeforeDissolving;
            this.maxAmountOfMeteoritesFallingSimultaneously = maxAmountOfMeteoritesFallingSimultaneously;
            this.chanceOfMeteoriteToFallOnMap = chanceOfMeteoriteToFallOnMap;
            this.mainSentry = mainSentry;
            Random = mainSentry.Random;
        }
        public void FirstTick()
        {
            initMeteorites();
        }
        public void NextTick()
        {
            if (Meteorite.AmountOfActiveMeteorites < maxAmountOfMeteoritesFallingSimultaneously && Random.Next(100) < chanceOfMeteoriteToFallOnMap)
            {
                addAndSummonMeteorite();
            }

            for (int i = 0; i < maxAmountOfMeteoritesFallingSimultaneously; i++)
            {
                meteoriteNextTick(Meteorites[i]);
            }
        }

        private void meteoriteNextTick(Meteorite meteorite)
        {
            if (meteorite.Active)
                meteorite.NextTick();
        }

        private void addAndSummonMeteorite()
        {
            for (int i = 0; i < maxAmountOfMeteoritesFallingSimultaneously; i++)
            {
                if (!Meteorites[i].Active)
                {
                    Meteorites[i] = new Meteorite(this);
                    Meteorites[i].MeteoriteSummon(Random.Next(maxTicksMeteoriteFalling) + 1, Random.Next(maxTicksMeteoriteCracking) + 1, Random.Next(maxTicksMeteoriteBeforeDissolving) + 1, mainSentry.GetRandCoordsOnMap().Item1, mainSentry.GetRandCoordsOnMap().Item2);
                }
            }
        }

        private void initMeteorites()
        {
            for (int i = 0; i < maxAmountOfMeteoritesFallingSimultaneously; i++)
            {
                Meteorites.Add(new Meteorite(this));
            }
        }

        public bool CheckBorders((int, int) XY)
        {
            return mainSentry.CheckBorders(XY);
        }

        public void ShardKilledEverything((int, int) XY)
        {
            mainSentry.ShardKilledEverything(XY);
        }

        public void ShardBecameSomething((int, int) XY)
        {
            mainSentry.ShardBecameSomething(XY);
        }
        public void ShardWasMadeOnCell(MeteoriteShard shard)
        {
            mainSentry.EntityWasMadeOnCell(shard);
        }

        public void ShardWasDestroyedOnCell(MeteoriteShard shard)
        {
            mainSentry.EntityWasDestroyedOnCell(shard);
        }

        public bool CanPlaceShardOnCell((int, int) XY)
        {
            return mainSentry.CheckBorders(XY) && !mainSentry.IsOnCell<MeteoriteShard>(XY);
        }
    }
}
