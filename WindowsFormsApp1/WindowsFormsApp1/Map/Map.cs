using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Map
    {
        public Random Random = new Random();
        public int Rows;
        public int Cols;
        public Cell[,] Cells;

        public MainSentry mainSentry;

        private PerlinNoise perlinNoise;

        public Map(int Rows, int Cols,
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
            int chanceOfPlantToSpawnOnShard)
        {
            this.Rows = Rows;
            this.Cols = Cols;
            perlinNoise = new PerlinNoise();
            Cells = new Cell[Cols, Rows];
            for (int i = 0; i < Cols; i++)
                for (int j = 0; j < Rows; j++)
                {
                    Cells[i, j] = new Cell(perlinNoise.MultiOctaveNoise(i, j, 4));
                    SetCellState(i, j);
                }

            mainSentry = new MainSentry(apples, applesGrowth, carrots, carrotsGrowth, oats, oatsGrowth, humans, ticksHumanStutter, deers, ticksDeerStutter, mice, ticksMouseStutter, rabbits, ticksRabbitStutter, bears, ticksBearStutter, pigs, ticksPigStutter, raccoons, ticksRaccoonStutter, foxes, ticksFoxStutter, lions, ticksLionStutter, wolves, ticksWolfStutter, maxOrgVisionRange, maxOrgTicksBeforeReproducing, maxOrgTicksBeforeBecomingGrass, dayNightChange, maxAmountOfMeteoritesFallingSimultaneously, maxTicksMeteoriteFalling, maxTicksMeteoriteCracking, maxTicksMeteoriteBeforeDissolving, chanceOfMeteoriteToFallOnMap, chanceOfHumanToSpawnOnShard, chanceOfPlantToSpawnOnShard, this);
        }

        private void SetCellState(int x, int y)
        {
            if (Cells[x, y].NoiseValue < -0.15)
            {
                Cells[x, y].CellState = CellState.Water;
            }
            else if (Cells[x, y].NoiseValue < -0.05)
            {
                Cells[x, y].CellState = CellState.Sand;
            }
            else if (Cells[x, y].NoiseValue < 0.2)
            {
                Cells[x, y].CellState = CellState.Grass;
            }
            else
            {
                Cells[x, y].CellState = CellState.Hill;
            }
        }

        public Organism GetOrganismOnCell((int, int) XY)
        {
            for (int i = 0; i < Cells[XY.Item1, XY.Item2].OnCell.Count; i++)
            {
                if (Cells[XY.Item1, XY.Item2].OnCell[i] is Organism)
                {
                    return (Organism)Cells[XY.Item1, XY.Item2].OnCell[i];
                }
            }
            return null;
        }
        public void EntityWasMade(Entity entity)
        {
            Cells[entity.X, entity.Y].OnCell.Add(entity);
        }

        public void EntityWasDestroyed(Entity entity)
        {
            Cells[entity.X, entity.Y].OnCell.Remove(Cells[entity.X, entity.Y].OnCell.Find(probablyThisEntity => probablyThisEntity.ID == entity.ID));
        }

        public Cell[,] CreateWorld()
        {
            mainSentry.FirstTick();
            return Cells;
        }
        public Cell[,] UpdateWorld()
        {
            mainSentry.NextTick();
            return Cells;
        }
    }
}
