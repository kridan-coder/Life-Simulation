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
        Random random = new Random();
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
            (int, int) x_y;

            for (int i = 0; i < plantsGrowth;)
            {
                rand_ind = random.Next(plants.Count);
                x_y = plants[rand_ind].Grow(this);
                if (x_y.Item1 != -1)
                {
                    plants.Add(new Plant(x_y.Item1, x_y.Item2));
                    map[x_y.Item1, x_y.Item2].on_cell.Add(plants[plants.Count - 1]);
                    i++;
                }
            }
        }
        public Organism GetOrganismOnCell(int x, int y)
        {
            for (int i = 0; i < map[x, y].on_cell.Count; i++)
            {
                if (map[x, y].on_cell[i] is Organism)
                {
                    return (Organism)map[x, y].on_cell[i];
                }
            }
            return null;
        }
        public bool CheckBorders(int x, int y, int cols, int rows)
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
            {
                if (map[x, y].on_cell[i] is Plant)
                {
                    map[x, y].on_cell.Remove(map[x, y].on_cell[i]);
                }
            }
        }
        public bool OrganismIsOnCell(int x, int y)
        {
            for (int i = 0; i < map[x,y].on_cell.Count; i++)
            {
                if (map[x, y].on_cell[i] is Organism)
                    return true;
            }
            return false;
        }
        public bool OrganismHasOppositeSex(int x, int y, bool sex, Organism me)
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
        public bool PlantIsOnCell(int x, int y)
        {
            for (int i = 0; i < map[x, y].on_cell.Count; i++)
                if (map[x, y].on_cell[i] is Plant)
                    return true;
            return false;
        }
        public Cell[,] CreateWorld()
        {
            int x, y;
            bool male;
            int orgRange;

            // set plants
            for (int i = 0; i < plantsAmount;)
            {
                x = random.Next(cols);
                y = random.Next(rows);
                if (map[x, y].on_cell.Count == 0)
                {
                    plants.Add(new Plant(x, y));
                    map[x, y].on_cell.Add(plants[plants.Count - 1]);
                    i++;
                }
            }

            // set organisms
            for (int i = 0; i < organismsAmount;)
            {
                x = random.Next(cols);
                y = random.Next(rows);
                if (random.Next(100) > 50)
                    male = true;
                else
                    male = false;
                orgRange = minOrgRange + random.Next(minOrgRange);

                if (map[x, y].on_cell.Count == 0)
                {
                    organisms.Add(new Organism(x, y, male, lastOrgID++, orgRange, orgRollBackReproduce, orgDeadBeforeBecomingGrass));
                    map[x, y].on_cell.Add(organisms[organisms.Count - 1]);
                    i++;
                }
            }

            return map;

        }
        public Cell[,] UpdateWorld()
        {
            addPlants();

            int x, y;
            int direction;

            for (int i = organisms.Count - 1; i >= 0; i--)
            {
                // surviving ones
                if (organisms[i].is_alive)
                {
                    direction = 0;

                    // eating plants
                    if (PlantIsOnCell(organisms[i].x, organisms[i].y) && organisms[i].wantFood)
                    {
                        organisms[i].EatPlant();
                        plants.Remove(plants.Find(plant => plant.x == organisms[i].x && plant.y == organisms[i].y));
                        DeletePlantOnCell(organisms[i].x, organisms[i].y);
                    }

                    // reproducing
                    if (OrganismHasOppositeSex(organisms[i].x, organisms[i].y, organisms[i].male, organisms[i]) && organisms[i].wantReproduce)
                    {
                        organisms[i].Reproduce();

                        for (int j = 0; j < map[organisms[i].x, organisms[i].y].on_cell.Count; j++)
                        {
                            if (map[organisms[i].x, organisms[i].y].on_cell[j] is Organism)
                            {
                                Organism Partner = (Organism)map[organisms[i].x, organisms[i].y].on_cell[j];
                                if (Partner.male != organisms[i].male && Partner.is_alive && Partner.wantReproduce)
                                {
                                    Partner.Reproduce();
                                    break;
                                }
                            }
                        }

                        // creating baby
                        bool male;
                        if (random.Next(100) > 50)
                            male = true;
                        else
                            male = false;
                        int orgRange;
                        orgRange = minOrgRange + random.Next(minOrgRange);
                        organisms.Add(new Organism(organisms[i].x, organisms[i].y, male, lastOrgID++, orgRange, orgRollBackReproduce, orgDeadBeforeBecomingGrass));
                        map[organisms[i].x, organisms[i].y].on_cell.Add(organisms[organisms.Count - 1]);
                    }

                    // wanna reproduce ones
                    if (organisms[i].wantReproduce)
                        direction = organisms[i].CheckPartner(this);

                    // hungry ones
                    else if (organisms[i].wantFood)
                        direction = organisms[i].CheckFood(this);

                    if (direction == 0)
                        direction = random.Next(9);

                    // organism movement
                    organisms[i].MoveOnMap(direction, this);
                    organisms[i].MakeMove();

                    // organism made his move on the previous step. Now he is in another cell.
                    map[organisms[i].x, organisms[i].y].on_cell.Add(organisms[i]);
                }
                else
                {
                    if (organisms[i].DeadLongEnough())
                    {
                        // create plant
                        if (!PlantIsOnCell(organisms[i].x, organisms[i].y))
                        {
                            plants.Add(new Plant(organisms[i].x, organisms[i].y));
                            map[organisms[i].x, organisms[i].y].on_cell.Add(plants[plants.Count - 1]);
                        }

                        // delete org
                        DeleteOrgOnCell(organisms[i]);
                        organisms.Remove(organisms[i]);

                    }

                }

            }

            if (timeToChangeDayOrNight())
                day = !day;
            return map;
        }
    }
}
