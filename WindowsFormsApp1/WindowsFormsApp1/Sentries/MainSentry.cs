using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class MainSentry
    {
        public PlantSentry plantSentry;
        public AnimalSentry organismSentry;
        public DayNightSentry dayNightSentry;
        public MeteoriteSentry meteoriteSentry;

        private int chanceOfHumanToSpawnOnShard;
        private int chanceOfPlantToSpawnOnShard;

        public Random Random;
        public Map Map;

        public MainSentry(
            int apples, int applesGrowth,
            int carrots, int carrotsGrowth,
            int oats, int oatsGrowth,
            int humans, int ticksHumanStutter,
            int deers, int ticksDeerStutter,
            int mice, int ticksMouseStutter,
            int rabbits, int ticksRabbitStutter,
            int bears, int ticksBearStutter,
            int pigs, int ticksPigStutter,
            int raccoons, int ticksRaccoonStutter,
            int foxes, int ticksFoxStutter,
            int lions, int ticksLionStutter,
            int wolves, int ticksWolfStutter,
            int maxOrgVisionRange,
            int maxOrgTicksBeforeReproducing,
            int maxOrgTicksBeforeBecomingGrass,
            int dayNightChange,
            int maxAmountOfMeteoritesFallingSimultaneously,
            int maxTicksMeteoriteFalling,
            int maxTicksMeteoriteCracking,
            int maxTicksMeteoriteBeforeDissolving,
            int chanceOfMeteoriteToFallOnMap,
            int chanceOfHumanToSpawnOnShard,
            int chanceOfPlantToSpawnOnShard,
            Map map)
        {
            Random = map.Random;
            Map = map;
            plantSentry = new PlantSentry(apples, applesGrowth, carrots, carrotsGrowth, oats, oatsGrowth, this);
            organismSentry = new AnimalSentry(humans, ticksHumanStutter, deers, ticksDeerStutter, mice, ticksMouseStutter, rabbits, ticksRabbitStutter, bears, ticksBearStutter, pigs, ticksPigStutter, raccoons, ticksRaccoonStutter, foxes, ticksFoxStutter, lions, ticksLionStutter, wolves, ticksWolfStutter, maxOrgVisionRange, maxOrgTicksBeforeReproducing, maxOrgTicksBeforeBecomingGrass, this);
            dayNightSentry = new DayNightSentry(dayNightChange, this);
            meteoriteSentry = new MeteoriteSentry(maxTicksMeteoriteFalling, maxTicksMeteoriteCracking, maxTicksMeteoriteBeforeDissolving, maxAmountOfMeteoritesFallingSimultaneously, chanceOfMeteoriteToFallOnMap, this);
            this.chanceOfHumanToSpawnOnShard = chanceOfHumanToSpawnOnShard;
            this.chanceOfPlantToSpawnOnShard = chanceOfPlantToSpawnOnShard;
        }

        public void FirstTick()
        {
            plantSentry.FirstTick();
            organismSentry.FirstTick();
            meteoriteSentry.FirstTick();
        }

        public void NextTick()
        {
            plantSentry.NextTick();
            organismSentry.NextTick();
            meteoriteSentry.NextTick();
            dayNightSentry.NextTick();
        }


        public (int, int) GetRandCoordsOnMap()
        {
            return (Random.Next(Map.Cols), Random.Next(Map.Rows));
        }


        public void EntityWasMadeOnCell(Entity entity)
        {
            Map.EntityWasMade(entity);
        }

        public void EntityWasDestroyedOnCell(Entity entity)
        {
            Map.EntityWasDestroyed(entity);
        }



        public int EntitiesAmountOnCell((int, int) XY)
        {
            return Map.Cells[XY.Item1, XY.Item2].OnCell.Count();
        }

        public bool CheckBorders((int, int) XY)
        {
            if (XY.Item1 >= 0 && XY.Item2 >= 0 && XY.Item1 < Map.Cols && XY.Item2 < Map.Rows)
                return true;
            return false;
        }

        public bool CellIsEmpty((int, int) XY)
        {
            return (Map.Cells[XY.Item1, XY.Item2].OnCell.Count == 0);
        }

        public bool IsOnCell<T>((int, int) XY)
        {
            for (int i = 0; i < Map.Cells[XY.Item1, XY.Item2].OnCell.Count; i++)
                if (Map.Cells[XY.Item1, XY.Item2].OnCell[i] is T)
                    return true;
            return false;
        }

        public Organism<T, TFood> FindOrganismPartnerOnCell<T, TFood>((int, int) XY, Sex mySex)
            where T : Organism
            where TFood : Edible
        {
            for (int i = 0; i < Map.Cells[XY.Item1, XY.Item2].OnCell.Count; i++)
                if (Map.Cells[XY.Item1, XY.Item2].OnCell[i] is Organism<T, TFood>)
                {
                    Organism<T, TFood> potentialPartner = (Organism<T, TFood>)Map.Cells[XY.Item1, XY.Item2].OnCell[i];
                    if (potentialPartner.IsAlive && potentialPartner.Sex != mySex && potentialPartner.WantReproduce)
                        return potentialPartner;
                }
            return null;
        }

        // conversation between sentries

        public void EntityWasEaten<TFood>((int, int) XY)
    where TFood : Edible
        {
            for (int i = 0; i < Map.Cells[XY.Item1, XY.Item2].OnCell.Count; i++)
                if (Map.Cells[XY.Item1, XY.Item2].OnCell[i] is TFood)
                {
                    if (Map.Cells[XY.Item1, XY.Item2].OnCell[i] is Plant)
                        plantSentry.DeletePlant((Plant)Map.Cells[XY.Item1, XY.Item2].OnCell[i]);
                    else
                        organismSentry.DeleteOrganism((Organism)Map.Cells[XY.Item1, XY.Item2].OnCell[i]);
                }
        }

        public void OrganismBecamePlant(Organism organism)
        {
            int randInt = Random.Next(3);
            switch(randInt)
            {
                case 0:
                    plantSentry.SetPlantOnCurrentCell<Apple>((organism.X, organism.Y));
                    break;

                case 1:
                    plantSentry.SetPlantOnCurrentCell<Carrot>((organism.X, organism.Y));
                    break;

                case 2:
                    plantSentry.SetPlantOnCurrentCell<Oat>((organism.X, organism.Y));
                    break;
            }

        }

        public void ShardKilledEverything((int, int) XY)
        {
            int orgsAndPlants = EntitiesAmountOnCell(XY);
            for (int i = 0; i < orgsAndPlants - 1; i++)
            {
                EntityWasEaten<Edible>(XY);
            }
        }

        public void ShardBecameSomething((int, int) XY)
        {
            int randNum = Random.Next(100);
            if (randNum < chanceOfHumanToSpawnOnShard)
            {
                organismSentry.SetOrganismOnCurrentCell<Human>(XY);
            }
            else if (randNum < chanceOfHumanToSpawnOnShard + chanceOfPlantToSpawnOnShard)
            {
                randNum = Random.Next(3);
                switch(randNum)
                {
                    case 0:
                        plantSentry.SetPlantOnCurrentCell<Apple>(XY);
                        break;
                    case 1:
                        plantSentry.SetPlantOnCurrentCell<Carrot>(XY);
                        break;
                    case 2:
                        plantSentry.SetPlantOnCurrentCell<Oat>(XY);
                        break;
                }
            }
        }

        public bool IsItDayToday()
        {
            return dayNightSentry.Day;
        }

    }
}
