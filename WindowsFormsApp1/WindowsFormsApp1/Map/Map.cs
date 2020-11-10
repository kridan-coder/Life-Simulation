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
        public Random random = new Random();
        public Cell[,] map;
        public List<Organism> organisms = new List<Organism>();
        public List<Plant> plants = new List<Plant>();
        public int rows;
        public int cols;
        public int organismsAmount;
        public int plantsAmount;
        public int plantsGrowth;
        public int lastOrgID = 0;
        public int untilDayOrNight;
        public int dayOrNightLastsFor = 0;
        public bool day = true;
        public int minOrgRange;
        public int orgRollBackReproduce;
        public int orgDeadBeforeBecomingGrass;

        public Map(int _organisms, int _plants, int _plantsGrowth, int _rows, int _cols, int _dayNightChange, int _minOrgRange, int _orgRollBackReproduce, int _orgDeadBeforeBecomingGrass)
        {
            organismsAmount = _organisms;
            plantsAmount = _plants;
            plantsGrowth = _plantsGrowth;
            rows = _rows;
            cols = _cols;
            untilDayOrNight = _dayNightChange;
            minOrgRange = _minOrgRange;
            orgRollBackReproduce = _orgRollBackReproduce;
            orgDeadBeforeBecomingGrass = _orgDeadBeforeBecomingGrass;
            map = new Cell[cols, rows];
            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    map[i, j] = new Cell();
        }

        // check if day should be changed and increment dayOrNightLastsFor counter if needed
        private bool timeToChangeDayOrNight()
        {
            if (dayOrNightLastsFor >= untilDayOrNight)
            {
                dayOrNightLastsFor = 0;
                return true;
            }
            else
            {
                dayOrNightLastsFor++;
                return false;
            }
        }
        private void addPlants()
        {
            int rand_ind;
            Plant? potentialPlant;
            for (int i = 0; i < plantsGrowth;)
            {
                rand_ind = random.Next(plants.Count);
                potentialPlant = plants[rand_ind].Grow();
                if (potentialPlant != null)
                {
                    PlantWasMade(potentialPlant);
                    i++;
                }
            }
        }

        public void PlantWasEaten(int x, int y)
        {
            plants.Remove(plants.Find(plant => plant.x == x && plant.y == y));
            DeletePlantOnCell(x, y);
        }
        public void BabyWasMade(Organism baby)
        {
            organisms.Add(baby);
            map[baby.x, baby.y].on_cell.Add(organisms[organisms.Count - 1]);
        }
        public void PlantWasMade(Plant plant)
        {
            plants.Add(plant);
            map[plant.x, plant.y].on_cell.Add(plants[plants.Count - 1]);
        }
        public void OrganismMadeItsMove(Organism organism)
        {
            map[organism.x, organism.y].on_cell.Add(organism);
        }
        public void OrganismBecamePlant(Organism organism)
        {
            // create plant
            if (!IsOnCell<Plant>(organism.x, organism.y))
            {
                plants.Add(new Plant(organism.x, organism.y, this));
                map[organism.x, organism.y].on_cell.Add(plants[plants.Count - 1]);
            }

            // delete org
            DeleteOrgOnCell(organism);
            organisms.Remove(organism);
        }
        public Organism FindMyPartner(int x, int y, bool mySex)
        {
            for (int i = 0; i < map[x, y].on_cell.Count; i++)
                if (map[x, y].on_cell[i] is Organism)
                {
                    Organism potentialPartner = (Organism)map[x, y].on_cell[i];
                    if (potentialPartner.is_alive && potentialPartner.male != mySex && potentialPartner.wantReproduce)
                        return potentialPartner;
                }
            // should never come to this line actually
            return null;
        }
        public Organism GetOrganismOnCell(int x, int y)
        {
            for (int i = 0; i < map[x, y].on_cell.Count; i++)
                if (map[x, y].on_cell[i] is Organism)
                    return (Organism)map[x, y].on_cell[i];
            return null;
        }
        public bool CheckBorders(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < cols && y < rows)
                return true;
            return false;
        }
        public void DeleteOrgOnCell(Organism me)
        {
            for (int i = 0; i < map[me.x, me.y].on_cell.Count; i++)
            {
                if (map[me.x, me.y].on_cell[i] is Organism)
                {
                    Organism isItMe = (Organism)map[me.x, me.y].on_cell[i];
                    if (isItMe.orgID == me.orgID)
                        map[me.x, me.y].on_cell.Remove(map[me.x, me.y].on_cell[i]);
                }
            }
        }
        public void DeletePlantOnCell(int x, int y)
        {
            for (int i = 0; i < map[x, y].on_cell.Count; i++)
                if (map[x, y].on_cell[i] is Plant)
                    map[x, y].on_cell.Remove(map[x, y].on_cell[i]);
        }
        public bool OrganismHasOppositeSex(int x, int y, bool sex)
        {
            for (int i = 0; i < map[x, y].on_cell.Count; i++)
                if (map[x, y].on_cell[i] is Organism)
                {
                    Organism potentialPartner = (Organism)map[x, y].on_cell[i];
                    if (potentialPartner.male != sex && potentialPartner.is_alive && potentialPartner.wantReproduce)
                        return true; 
                }
            return false;
        }
        public bool IsOnCell<T>(int x, int y)
        {
            for (int i = 0; i < map[x, y].on_cell.Count; i++)
                if (map[x, y].on_cell[i] is T)
                    return true;
            return false;
        }
        public Cell[,] CreateWorld()
        {
            // set plants
            for (int i = 0; i < plantsAmount; i++)
                PlantWasMade(Plant.RandSpawn(this));
            // set organisms
            for (int i = 0; i < organismsAmount; i++)
                BabyWasMade(Organism.RandSpawn(this));
            return map;
        }
        public Cell[,] UpdateWorld()
        {
            addPlants();
            for (int i = organisms.Count - 1; i >= 0; i--)
                organisms[i].NextMove();
            if (timeToChangeDayOrNight())
                day = !day;
            return map;
        }
    }
}
