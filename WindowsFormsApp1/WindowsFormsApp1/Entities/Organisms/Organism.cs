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
        private static int lastOrgID;

        public bool is_alive;

        public int orgID;

        public int organismRange;

        public int deadUntil;
        public int noReproduceUntil;

        public bool male;

        public int deadFor = 0;
        public int reproducedFor = 0;
        public int fullness = 100;

        public bool wantFood = false;
        public bool wantReproduce = false;

        public Organism(int _x, int _y, bool _male, int _range, int _rollBack, int _deadUntil, Map _map) : base(_x, _y, _map)
        {
            orgID = lastOrgID++;
            male = _male;
            organismRange = _range;
            deadUntil = _deadUntil;
            noReproduceUntil = _rollBack;
            is_alive = true;
        }

        // main function called by map
        public void NextMove()
        {
            Direction direction = Direction.None;
            if (is_alive)
            {
                checkFood();
                checkReproduce();
                direction = makeDecision(direction);
                makeMove(direction);
            }
            else if (deadLongEnough())
                map.OrganismBecamePlant(this);
        }

        public static Organism RandSpawn(Map map)
        {
            int x, y, orgRange;
            bool male;
            while (true)
            {
                x = map.random.Next(map.cols);
                y = map.random.Next(map.rows);
                if (map.map[x, y].on_cell.Count == 0)
                {
                    if (map.random.Next(100) > 50)
                        male = true;
                    else
                        male = false;
                    orgRange = map.minOrgRange + map.random.Next(map.minOrgRange);

                    return new Organism(x, y, male, orgRange, map.orgRollBackReproduce, map.orgDeadBeforeBecomingGrass, map);
                }
            }


        }

        private void checkFood()
        {
            if (map.IsOnCell<Plant>(x, y) && wantFood)
            {
                changeValuesOnEatingPlant();
                map.PlantWasEaten(x, y);
            }
        }
        private void checkReproduce()
        {
            if (map.OrganismHasOppositeSex(x, y, male) && wantReproduce)
            {
                changeValuesOnReproduce();
                map.FindMyPartner(x, y, this.male).changeValuesOnReproduce();
                makeBaby();
            }
        }
        private void makeMove(Direction direction)
        {
            map.DeleteOrgOnCell(this);
            changeValuesOnMove();
            moveOnMap(direction);
            map.OrganismMadeItsMove(this);
        }
        private Direction makeDecision(Direction direction)
        {
            if (wantReproduce)
                direction = chooseDirection(FindOnMap<Organism>(true, this.male));
            else if (wantFood)
                direction = chooseDirection(FindOnMap<Plant>(false, null));
            // no idea what to do
            if (direction == Direction.None)
                direction = randomDirection8();
            return direction;
        }
        private void makeBaby()
        {
            bool babyMale;
            int babyRange;
            if (map.random.Next(100) > 50)
                babyMale = true;
            else
                babyMale = false;
            babyRange = map.minOrgRange + map.random.Next(map.minOrgRange);
            map.BabyWasMade(new Organism(x, y, babyMale, babyRange, map.orgRollBackReproduce, map.orgDeadBeforeBecomingGrass, map));
        }
        private int setActualRange()
        {
            return (map.day) ? organismRange : organismRange / 2;
        }
        private bool sellIsAppropriate<T>(int x, int y, bool findingPartner, bool? sex)
        {
            return map.CheckBorders(x, y)
                && map.IsOnCell<T>(x, y)
                && ((findingPartner) ? map.OrganismHasOppositeSex(x, y, (bool)sex) : true);
        }
        private (int, int)? checkLines<T>(int range, bool findingPartner, bool? sex)
        {
            // top
            for (int i = x - range; i < x + range; i++)
                if (sellIsAppropriate<T>(i, y - range, findingPartner, sex))
                    return (i, y - range);
            // right
            for (int i = y - range; i < y + range; i++)
                if (sellIsAppropriate<T>(x + range, i, findingPartner, sex))
                    return (x + range, i);
            // bottom
            for (int i = x + range; i > x - range; i--)
                if (sellIsAppropriate<T>(i, y + range, findingPartner, sex))
                    return (i, y + range);
            // left
            for (int i = y + range; i > y - range; i--)
                if (sellIsAppropriate<T>(x - range, i, findingPartner, sex))
                    return (x - range, i);
            return null;

        }
        private (int, int)? FindOnMap<T>(bool findingPartner, bool? sex)
        {
            int currentRange = 1;
            int maxRange = setActualRange();
            (int,int)? found = null;
            while (currentRange <= maxRange && found == null)
                found = checkLines<T>(currentRange++, findingPartner, sex);
            return found;
        }
        private Direction chooseDirection((int, int)? goal)
        {
            if (goal == null)
                return Direction.None;
            else
            {
                (int, int) goalIndeed = ((int, int)) goal;
                if (x < goalIndeed.Item1)
                {
                    if (y > goalIndeed.Item2)
                        return Direction.TopRight;
                    else if (y == goalIndeed.Item2)
                        return Direction.Right;
                    else if (y < goalIndeed.Item2)
                        return Direction.BottomRight;
                }
                else if (x > goalIndeed.Item1)
                {
                    if (y < goalIndeed.Item2)
                        return Direction.BottomLeft;
                    else if (y == goalIndeed.Item2)
                        return Direction.Left;
                    else if (y > goalIndeed.Item2)
                        return Direction.TopLeft;
                }
                else if (x == goalIndeed.Item1)
                {
                    if (y < goalIndeed.Item2)
                        return Direction.Bottom;
                    else if (y > goalIndeed.Item2)
                        return Direction.Top;
                }
                return Direction.None;
            }



        }
        private void moveOnMap(Direction direction)
        {
            switch (direction)
            {
                case Direction.TopLeft:
                    if (map.CheckBorders(x - 1, y))
                        x--;
                    else if (map.CheckBorders(x, y - 1))
                        y--;
                    break;
                case Direction.Top:
                    if (map.CheckBorders(x, y - 1))
                        y--;
                    break;
                case Direction.TopRight:
                    if (map.CheckBorders(x + 1, y))
                        x++;
                    else if (map.CheckBorders(x, y - 1))
                        y--;
                    break;
                case Direction.Right:
                    if (map.CheckBorders(x + 1, y))
                        x++;
                    break;
                case Direction.BottomRight:
                    if (map.CheckBorders(x + 1, y))
                        x++;
                    else if (map.CheckBorders(x, y + 1))
                        y++;
                    break;
                case Direction.Bottom:
                    if (map.CheckBorders(x, y + 1))
                        y++;
                    break;
                case Direction.BottomLeft:
                    if (map.CheckBorders(x - 1, y))
                        x--;
                    else if (map.CheckBorders(x, y + 1))
                        y++;
                    break;
                case Direction.Left:
                    if (map.CheckBorders(x - 1, y))
                        x--;
                    break;
            }
        }

        // check if alive and increment deadFor counter if needed
        private bool deadLongEnough()
        {
            if (!is_alive && deadFor >= deadUntil)
                return true;
            else
                deadFor++;
            return false;
        }
        private void changeValuesOnMove()
        {
            if (fullness <= 0)
            {
                is_alive = false;
            }
            else if (fullness <= 10 && wantReproduce)
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
                if (reproducedFor >= noReproduceUntil)
                    wantReproduce = true;
                else
                    reproducedFor++;
            }
        }
        private void changeValuesOnReproduce()
        {
            reproducedFor = 0;
            wantReproduce = false;
        }
        private void changeValuesOnEatingPlant()
        {
            fullness = 100;
            wantFood = false;
        }
    }
}
