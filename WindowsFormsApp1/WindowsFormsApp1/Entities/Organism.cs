using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace WindowsFormsApp1
{
    public class Organism : Entity
    {
        public bool is_alive;
        public int orgID;
        public int organismRange;
        public int waitReproduceTime;

        public int deadUntil;
        public bool male;
        public Organism(int _x, int _y, bool _male, int _id, int _range, int _rollBack, int _deadUntil) : base(_x, _y)
        {
            male = _male;
            orgID = _id;
            organismRange = _range;
            deadUntil = _deadUntil;
            waitReproduceTime = _rollBack;
            rollBackReproduce = _rollBack;
            is_alive = true;
        }

        public int deadFor = 0;
        public int rollBackReproduce; 
        public int fullness = 100;


        public bool wantFood = false;
        public bool foundFood = false;

        public bool wantReproduce = false;
        public bool foundPartner = false;

        // return [0-8]{1}. 0 means that no food was found. [1-8] means directions.
        // 1 2 3
        // 8   4
        // 7 6 5
        public int CheckFood(Map map_)
        {
            Organism organism = this;
            int cols = map_.cols;
            int rows = map_.rows;
            int range = 1;
            bool day = map_.day;
            int organismsRange = organismRange;

            if (!day)
                organismsRange = organismsRange / 2;

            organism.foundFood = false;
            while (!organism.foundFood && range <= organismsRange)
            {

                for (int i = organism.x - range; i < organism.x + range; i++)
                {
                    if (map_.CheckBorders(i, organism.y - range, cols, rows))
                    {
                        if (map_.PlantIsOnCell(i, organism.y - range))
                        {
                            organism.foundFood = true;
                            if (organism.x > i)
                                return 1;
                            else if (organism.x == i)
                                return 2;
                            else if (organism.x < i)
                                return 3;
                        }
                    }

                }
                for (int i = organism.y - range; i < organism.y + range; i++)
                {
                    if (map_.CheckBorders(organism.x + range, i, cols, rows))
                    {
                        if (map_.PlantIsOnCell(organism.x + range, i))
                        {
                            organism.foundFood = true;
                            if (organism.y > i)
                                return 3;
                            else if (organism.y == i)
                                return 4;
                            else if (organism.y < i)
                                return 5;
                        }
                    }
                }
                for (int i = organism.x + range; i > organism.x - range; i--)
                {
                    if (map_.CheckBorders(i, organism.y + range, cols, rows))
                    {
                        if (map_.PlantIsOnCell(i, organism.y + range))
                        {
                            organism.foundFood = true;
                            if (organism.x < i)
                                return 5;
                            else if (organism.x == i)
                                return 6;
                            else if (organism.x > i)
                                return 7;
                        }
                    }
                }
                for (int i = organism.y + range; i > organism.y - range; i--)
                {
                    if (map_.CheckBorders(organism.x - range, i, cols, rows))
                    {
                        if (map_.PlantIsOnCell(organism.x - range, i))
                        {
                            organism.foundFood = true;
                            if (organism.y < i)
                                return 7;
                            else if (organism.y == i)
                                return 8;
                            else if (organism.y > i)
                                return 1;
                        }
                    }
                }
                range++;
            }
            return 0;
        }



        // return [0-8]{1}. 0 means that no partner was found. [1-8] means directions.
        // 1 2 3
        // 8   4
        // 7 6 5
        public int CheckPartner(Map map_)
        {
            Organism organism = this;
            int cols = map_.cols;
            int rows = map_.rows;
            int range = 1;
            bool day = map_.day;
            int organismsRange = organismRange;

            if (!day)
                organismsRange = organismsRange / 2;

            organism.foundPartner = false;
            while (!organism.foundPartner && range <= organismsRange)
            {
                for (int i = organism.x - range; i < organism.x + range; i++)
                {
                    if (map_.CheckBorders(i, organism.y - range, cols, rows))
                    {
                        if (map_.OrganismHasOppositeSex(i, organism.y - range, organism.male, organism))
                        {
                            organism.foundPartner = true;
                            if (organism.x > i)
                                return 1;
                            else if (organism.x == i)
                                return 2;
                            else if (organism.x < i)
                                return 3;
                        }
                    }

                }
                for (int i = organism.y - range; i < organism.y + range; i++)
                {
                    if (map_.CheckBorders(organism.x + range, i, cols, rows))
                    {
                        if (map_.OrganismHasOppositeSex(organism.x + range, i, organism.male, organism))
                        {
                            organism.foundPartner = true;

                            if (organism.y > i)
                                return 3;
                            else if (organism.y == i)
                                return 4;
                            else if (organism.y < i)
                                return 5;
                        }
                    }
                }
                for (int i = organism.x + range; i > organism.x - range; i--)
                {
                    if (map_.CheckBorders(i, organism.y + range, cols, rows))
                    {
                        if (map_.OrganismHasOppositeSex(i, organism.y + range, organism.male, organism))
                        {
                            organism.foundPartner = true;
                            if (organism.x < i)
                                return 5;
                            else if (organism.x == i)
                                return 6;
                            else if (organism.x > i)
                                return 7;
                        }
                    }
                }
                for (int i = organism.y + range; i > organism.y - range; i--)
                {
                    if (map_.CheckBorders(organism.x - range, i, cols, rows))
                    {
                        if (map_.OrganismHasOppositeSex(organism.x - range, i, organism.male, organism))
                        {
                            organism.foundPartner = true;
                            if (organism.y < i)
                                return 7;
                            else if (organism.y == i)
                                return 8;
                            else if (organism.y > i)
                                return 1;
                        }
                    }
                }
                range++;
            }
            return 0;
        }


        public void MoveOnMap(int direction, Map map_)
        {
            Organism organism = this;
            int cols = map_.cols;
            int rows = map_.rows;
            switch (direction)
            {
                case 0:
                    {
                        map_.DeleteOrgOnCell(this);
                    }
                    break;
                case 1:
                    if (map_.CheckBorders(organism.x - 1, organism.y, cols, rows))
                    {
                        map_.DeleteOrgOnCell(this);
                        organism.x--;
                    }
                    else if (map_.CheckBorders(organism.x, organism.y - 1, cols, rows))
                    {
                        map_.DeleteOrgOnCell(this);
                        organism.y--;
                    }
                    break;
                case 2:
                    if (map_.CheckBorders(organism.x, organism.y - 1, cols, rows))
                    {
                        map_.DeleteOrgOnCell(this);
                        organism.y--;
                    }
                    break;
                case 3:
                    if (map_.CheckBorders(organism.x + 1, organism.y, cols, rows))
                    {
                        map_.DeleteOrgOnCell(this);
                        organism.x++;
                    }
                    else if (map_.CheckBorders(organism.x, organism.y - 1, cols, rows))
                    {
                        map_.DeleteOrgOnCell(this);
                        organism.y--;
                    }
                    break;
                case 4:
                    if (map_.CheckBorders(organism.x + 1, organism.y, cols, rows))
                    {
                        map_.DeleteOrgOnCell(this);
                        organism.x++;
                    }
                    break;
                case 5:
                    if (map_.CheckBorders(organism.x + 1, organism.y, cols, rows))
                    {
                        map_.DeleteOrgOnCell(this);
                        organism.x++;
                    }
                    else if (map_.CheckBorders(organism.x, organism.y + 1, cols, rows))
                    {
                        map_.DeleteOrgOnCell(this);
                        organism.y++;
                    }
                    break;
                case 6:
                    if (map_.CheckBorders(organism.x, organism.y + 1, cols, rows))
                    {
                        map_.DeleteOrgOnCell(this);
                        organism.y++;
                    }
                    break;
                case 7:
                    if (map_.CheckBorders(organism.x - 1, organism.y, cols, rows))
                    {
                        map_.DeleteOrgOnCell(this);
                        organism.x--;
                    }
                    else if (map_.CheckBorders(organism.x, organism.y + 1, cols, rows))
                    {
                        map_.DeleteOrgOnCell(this);
                        organism.y++;
                    }
                    break;
                case 8:
                    if (map_.CheckBorders(organism.x - 1, organism.y, cols, rows))
                    {
                        map_.DeleteOrgOnCell(this);
                        organism.x--;
                    }
                    break;
            }
        }

        public void MakeMove()
        {
            if (fullness <= 0)
            {
                is_alive = false;
            }
            else if(fullness <= 10 && wantReproduce)
            {
                wantReproduce = false;
                wantFood = true;
                fullness--;
            }
            else if (fullness < 50)
            {
                wantFood = true;
                fullness--;
            }
            else
            {
                fullness--;
                if (rollBackReproduce <= 0)
                    wantReproduce = true;
                else
                    rollBackReproduce--;
            }
        }

        public bool DeadLongEnough()
        {
            if (!is_alive && deadFor >= deadUntil)
                return true;
            else
                deadFor++;
            return false;
        }

        public void Reproduce()
        {
            rollBackReproduce = waitReproduceTime;
            wantReproduce = false;
            foundPartner = false;
        }

        public void EatPlant()
        {
            fullness = 100;
            wantFood = false;
            foundFood = false;
        }
    }
}
