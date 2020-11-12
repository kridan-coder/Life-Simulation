using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace WindowsFormsApp1
{
    public abstract class Organism<TFood> : Entity
        where TFood : Edible
    {
        private static int lastOrgID;

        public bool isAlive;

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
            isAlive = true;
        }

        // main function called by map
        public void NextMove()
        {
            Direction direction = Direction.None;
            if (isAlive)
            {
                checkFood();
                checkReproduce();
                direction = makeDecision(direction);
                makeMove(direction);
            }
            else if (deadLongEnough())
                becomingPlant();
        }
        public abstract void becomingPlant();
        public static (int, int, bool, int, int, int) RandSpawnValues(Map map)
        {
            int x, y, orgRange;
            bool male;
            while (true)
            {
                x = map.random.Next(map.cols);
                y = map.random.Next(map.rows);
                if (map.map[x, y].OnCell.Count == 0)
                {
                    if (map.random.Next(100) > 50)
                        male = true;
                    else
                        male = false;
                    orgRange = map.minOrgRange + map.random.Next(map.minOrgRange);
                    return (x, y, male, orgRange, map.orgRollBackReproduce, map.orgDeadBeforeBecomingGrass);
                }
            }
        }

        private void checkFood()
        {
            if (map.IsOnCell<TFood>(x, y) && wantFood)
            {
                changeValuesOnEating();
                EatFood();
            }
        }

        public abstract void EatFood();

        private void checkReproduce()
        {
            if (wantReproduce && map.OrganismHasOppositeSex<TFood>(x, y, male))
            {
                changeValuesOnReproduce();
                map.FindMyPartner<TFood>(x, y, this.male).changeValuesOnReproduce();
                makeBaby();
            }
        }

        public abstract void makeBaby();
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
                direction = chooseDirection(FindOnMap<Organism<TFood>>(true, this.male));
            else if (wantFood)
                direction = chooseDirection(FindOnMap<TFood>(false, null));
            // no idea what to do
            if (direction == Direction.None)
                direction = randomDirection8();
            direction = finalDecision(direction);
            return direction;
        }

        public abstract Direction finalDecision(Direction direction);
        public (int, int, bool, int, int, int) MakeBabyValues()
        {
            bool babyMale;
            int babyRange;
            if (map.random.Next(100) > 50)
                babyMale = true;
            else
                babyMale = false;
            babyRange = map.minOrgRange + map.random.Next(map.minOrgRange);
            return (x, y, babyMale, babyRange, map.orgRollBackReproduce, map.orgDeadBeforeBecomingGrass);
        }
        private int setActualRange()
        {
            return (map.day) ? organismRange : organismRange / 2;
        }
        private bool sellIsAppropriate<T>(int x, int y, bool findingPartner, bool? sex)
        {
            return map.CheckBorders(x, y)
                && map.IsOnCell<T>(x, y)
                && ((findingPartner) ? map.OrganismHasOppositeSex<TFood>(x, y, (bool)sex) : true);
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
            if (!isAlive && deadFor >= deadUntil)
                return true;
            else
                deadFor++;
            return false;
        }
        private void changeValuesOnMove()
        {
            if (fullness <= 0)
            {
                isAlive = false;
            }
            else if (fullness <= 10 && wantReproduce)
            {
                wantReproduce = false;
                wantFood = true;
                fullness--;
            }
            else if (fullness < 50 && !wantReproduce)
            {
                wantFood = true;
                fullness--;
            }
            else
            {
                fullness--;
                if (reproducedFor >= noReproduceUntil)
                {
                    wantReproduce = true;
                    wantFood = false;
                }
                else
                    reproducedFor++;
            }
        }
        private void changeValuesOnReproduce()
        {
            reproducedFor = 0;
            wantReproduce = false;
        }
        private void changeValuesOnEating()
        {
            fullness = 100;
            wantFood = false;
        }
    }
}
