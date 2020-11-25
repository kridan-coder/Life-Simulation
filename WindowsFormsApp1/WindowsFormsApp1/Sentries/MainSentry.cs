using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class MainSentry
    {
        private PlantSentry plantSentry;
        private OrganismSentry organismSentry;
        private DayNightSentry dayNightSentry;
        private MeteoriteSentry meteoriteSentry;

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
            int amountOfMeteoritesFallingSimultaneously,
            int maxTicksMeteoriteFalling,
            int maxTicksMeteoriteCracking,
            int maxTicksMeteoriteBeforeDissolving,
            Map map)
        {
            Random = map.Random;
            Map = map;
            plantSentry = new PlantSentry(apples, applesGrowth, carrots, carrotsGrowth, oats, oatsGrowth, this);
        }


        public (int, int) GetRandCoordsOnMap()
        {
            return (Random.Next(Map.cols), Random.Next(Map.rows));
        }

        public void EntityWasMadeOnMap(Entity entity)
        {
            Map.EntityWasMade(entity);
        }

        public void EntityWasDestroyedOnMap(Entity entity)
        {
            Map.EntityWasDestroyed(entity);
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

        public Organism<T, TFood> FindOrganismPartnerOnCell<T, TFood>((int, int) XY, bool mySex)
            where T : Organism
            where TFood : Edible
        {
            for (int i = 0; i < Map.Cells[XY.Item1, XY.Item2].OnCell.Count; i++)
                if (Map.Cells[XY.Item1, XY.Item2].OnCell[i] is Organism<T, TFood>)
                {
                    Organism<T, TFood> potentialPartner = (Organism<T, TFood>)Map.Cells[XY.Item1, XY.Item2].OnCell[i];
                    if (potentialPartner.IsAlive && potentialPartner.Male != mySex && potentialPartner.WantReproduce)
                        return potentialPartner;
                }
            return null;
        }

        // conversation between sentries

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

        public bool IsItDayToday()
        {
            return dayNightSentry.Day;
        }

    }
}
