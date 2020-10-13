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
        private Cell[,] map;
        public List<Organism> organisms = new List<Organism>();
        public List<Plant> plants = new List<Plant>();
        public int rows;
        public int cols;
        private int organismsAmount;
        private int plantsAmount;
        private int plantsGrowth;
        private int organismsRange;


        Form1 form1;

        public Map(int resolution, int _organisms, int _plants, int _plantsGrowth, int _organismsRange, ref Form1 form)
        {
            form1 = form;
            organismsAmount = _organisms;
            plantsAmount = _plants;
            plantsGrowth = _plantsGrowth;
            organismsRange = _organismsRange;
            rows = form1.CountRows(resolution);
            cols = form1.CountCols(resolution);
            map = new Cell[cols, rows];
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    map[i, j] = new Cell();
                }
            }
        }

        private bool checkBorders(int x, int y, int cols, int rows)
        {
            if (x >= 0 && y >= 0 && x < cols && y < rows)
                return true;
            return false;
        }

        // return [0-8]{1}. 0 means that no food was found. [1-8] means directions.
        // 1 2 3
        // 8   4
        // 7 6 5
        private int checkFood(Organism organism)
        {

            int range = 1;
            organism.foundFood = false;
            while (!organism.foundFood && range <= organismsRange)
            {

                for (int i = organism.x - range; i < organism.x + range; i++)
                {
                    if (checkBorders(i, organism.y - range, cols, rows))
                    {
                        if (map[i, organism.y - range].on_cell is Plant)
                        {
                            organism.foundFood = true;

                            if (organism.x > i)
                            {

                                return 1;

                            }
                            else if (organism.x == i)
                            {

                                return 2;
                            }
                            else if (organism.x < i)
                            {

                                return 3;
                            }
                        }
                    }

                }
                for (int i = organism.y - range; i < organism.y + range; i++)
                {
                    if (checkBorders(organism.x + range, i, cols, rows))
                    {
                        if (map[organism.x + range, i].on_cell is Plant)
                        {
                            organism.foundFood = true;

                            if (organism.y > i)
                            {

                                return 3;
                            }
                            else if (organism.y == i)
                            {

                                return 4;
                            }
                            else if (organism.y < i)
                            {

                                return 5;
                            }
                        }
                    }
                }
                for (int i = organism.x + range; i > organism.x - range; i--)
                {
                    if (checkBorders(i, organism.y + range, cols, rows))
                    {
                        if (map[i, organism.y + range].on_cell is Plant)
                        {
                            organism.foundFood = true;

                            if (organism.x < i)
                            {

                                return 5;
                            }
                            else if (organism.x == i)
                            {

                                return 6;
                            }
                            else if (organism.x > i)
                            {

                                return 7;
                            }
                        }
                    }
                }
                for (int i = organism.y + range; i > organism.y - range; i--)
                {
                    if (checkBorders(organism.x - range, i, cols, rows))
                    {
                        if (map[organism.x - range, i].on_cell is Plant)
                        {
                            organism.foundFood = true;

                            if (organism.y < i)
                            {

                                return 7;
                            }
                            else if (organism.y == i)
                            {

                                return 8;
                            }
                            else if (organism.y > i)
                            {


                                return 1;
                            }
                        }
                    }
                }
                range++;
            }

            return 0;
        }

        private void moveOnMap(Organism organism, int direction)
        {

            switch (direction)
            {
                case 1:
                    if (checkBorders(organism.x - 1, organism.y, cols, rows) && !(map[organism.x - 1, organism.y].on_cell is Organism))
                    {

                        map[organism.x, organism.y].on_cell = null;

                        organism.x--;

                    }
                    else if (checkBorders(organism.x, organism.y - 1, cols, rows) && !(map[organism.x, organism.y - 1].on_cell is Organism))
                    {
                        map[organism.x, organism.y].on_cell = null;

                        organism.y--;

                    }
                    break;
                case 2:
                    if (checkBorders(organism.x, organism.y - 1, cols, rows) && !(map[organism.x, organism.y - 1].on_cell is Organism))
                    {
                        map[organism.x, organism.y].on_cell = null;

                        organism.y--;
                    }
                    break;
                case 3:
                    if (checkBorders(organism.x + 1, organism.y, cols, rows) && !(map[organism.x + 1, organism.y].on_cell is Organism))
                    {

                        map[organism.x, organism.y].on_cell = null;

                        organism.x++;

                    }
                    else if (checkBorders(organism.x, organism.y - 1, cols, rows) && !(map[organism.x, organism.y - 1].on_cell is Organism))
                    {
                        map[organism.x, organism.y].on_cell = null;

                        organism.y--;

                    }
                    break;
                case 4:
                    if (checkBorders(organism.x + 1, organism.y, cols, rows) && !(map[organism.x + 1, organism.y].on_cell is Organism))
                    {

                        map[organism.x, organism.y].on_cell = null;

                        organism.x++;

                    }
                    break;
                case 5:
                    if (checkBorders(organism.x + 1, organism.y, cols, rows) && !(map[organism.x + 1, organism.y].on_cell is Organism))
                    {

                        map[organism.x, organism.y].on_cell = null;

                        organism.x++;

                    }
                    else if (checkBorders(organism.x, organism.y + 1, cols, rows) && !(map[organism.x, organism.y + 1].on_cell is Organism))
                    {
                        map[organism.x, organism.y].on_cell = null;

                        organism.y++;

                    }
                    break;
                case 6:
                    if (checkBorders(organism.x, organism.y + 1, cols, rows) && !(map[organism.x, organism.y + 1].on_cell is Organism))
                    {
                        map[organism.x, organism.y].on_cell = null;

                        organism.y++;

                    }
                    break;
                case 7:
                    if (checkBorders(organism.x - 1, organism.y, cols, rows) && !(map[organism.x - 1, organism.y].on_cell is Organism))
                    {

                        map[organism.x, organism.y].on_cell = null;

                        organism.x--;

                    }
                    else if (checkBorders(organism.x, organism.y + 1, cols, rows) && !(map[organism.x, organism.y + 1].on_cell is Organism))
                    {
                        map[organism.x, organism.y].on_cell = null;

                        organism.y++;

                    }
                    break;
                case 8:
                    if (checkBorders(organism.x - 1, organism.y, cols, rows) && !(map[organism.x - 1, organism.y].on_cell is Organism))
                    {

                        map[organism.x, organism.y].on_cell = null;

                        organism.x--;

                    }
                    break;
            }
        }

        private void addPlants()
        {
            int x, y;

            for (int i = 0; i < plantsGrowth;)
            {
                x = random.Next(cols);
                y = random.Next(rows);
                if (map[x, y].on_cell is null)
                {
                    plants.Add(new Plant(x, y));
                    map[x, y].on_cell = plants[plants.Count - 1];
                    i++;
                }
            }
        }

        public Cell[,] CreateWorld()
        {
            int x, y;

            // set plants
            for (int i = 0; i < plantsAmount;)
            {
                x = random.Next(cols);
                y = random.Next(rows);
                if (map[x, y].on_cell is null)
                {
                    plants.Add(new Plant(x, y));
                    map[x, y].on_cell = plants[plants.Count - 1];
                    i++;
                }
            }


            // set organisms
            for (int i = 0; i < organismsAmount;)
            {
                x = random.Next(cols);
                y = random.Next(rows);
                if (map[x, y].on_cell == null)
                {
                    organisms.Add(new Organism(x, y));
                    map[x, y].on_cell = organisms[organisms.Count - 1];
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
                    if (map[organisms[i].x, organisms[i].y].on_cell is Plant)
                    {
                        organisms[i].EatPlant();
                        plants.Remove(plants.Find(plant => plant.x == organisms[i].x && plant.y == organisms[i].y));
                    }

                    map[organisms[i].x, organisms[i].y].on_cell = organisms[i];

                    // hungry ones
                    if (organisms[i].wantFood)
                    {
                        direction = checkFood(organisms[i]);
                    }


                    if (direction == 0)
                        direction = random.Next(9);

                    // organism movement
                    moveOnMap(organisms[i], direction);
                    organisms[i].MakeMove();


                }
                // gonna die
                else
                {
                    map[organisms[i].x, organisms[i].y].on_cell = null;
                    organisms.Remove(organisms[i]);
                }

            }

            return map;


        }



    }
}
