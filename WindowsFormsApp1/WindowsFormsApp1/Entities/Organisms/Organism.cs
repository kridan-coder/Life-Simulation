using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace WindowsFormsApp1
{
    public abstract class Organism<T, TFood> : Organism
        where T : Organism
        where TFood : Edible
    {
        private static OrganismSentry organismSentry;

        public bool IsAlive;

        public int OrganismRange;

        public int DeadUntil;
        public int NoReproduceUntil;
        public static int StutterUntil;

        public bool Male;

        public int DeadFor = 0;
        public int ReproducedFor = 0;
        public int Fullness = 100;
        public int StutterFor = 0;

        public bool WantFood = false;
        public bool WantReproduce = false;

        public Organism(int _x, int _y, bool _male, int _range, int _rollBack, int _deadUntil, int _stutter, OrganismSentry _organismSentry) : base(_x, _y)
        {
            organismSentry = _organismSentry;
            StutterUntil = _stutter;
            Male = _male;
            OrganismRange = _range;
            DeadUntil = _deadUntil;
            NoReproduceUntil = _rollBack;
            IsAlive = true;
        }

        public override void NextMove()
        {
            Direction direction = Direction.None;
            if (IsAlive)
            {
                checkFood();
                checkReproduce();
                direction = makeDecision(direction);
                makeMove(direction);
            }
            else if (deadLongEnough())
                becomingPlant();
        }
        
        private void checkFood()
        {
            if (organismSentry.IsOnCell<TFood>((X,Y)) && WantFood)
            {
                changeValuesOnEating();
                EatFood();
            }
        }
        private void becomingPlant()
        {
            organismSentry.OrganismBecamePlant(this);
        }

        public static Organism RandSpawn(OrganismSentry organismSentry)
        {
            (int, int) XY;
            int orgRange;
            bool Male;
            while (true)
            {
                XY = organismSentry.GetRandCoordsOnMap();
                if (organismSentry.CellIsEmpty(XY))
                {
                    Male = randomSex();
                    orgRange = randomVisionRange(organismSentry);
                    return SetOrganism<T>(XY, orgRange, Male);
                }
            }
        }

        public static Organism SetOrganism<Type>((int, int) XY, int range, bool Male) 
            where Type : Organism
        {
            return (Type)Activator.CreateInstance(typeof(Type), new object[] { XY.Item1, XY.Item2, Male, range, organismSentry.MaxOrgTicksBeforeReproducing, organismSentry.MaxOrgTicksBeforeBecomingGrass, StutterUntil, organismSentry });
        }

        public Organism MakeBaby()
        {
            bool babyMale;
            int babyRange;
            babyMale = randomSex();
            babyRange = randomVisionRange(organismSentry);
            return SetOrganism<T>((X,Y), babyRange, babyMale);
        }

        private static bool randomSex()
        {
            return organismSentry.Random.Next(100) > 50;
        }

        private static int randomVisionRange(OrganismSentry organismSentry)
        {
            return organismSentry.MaxOrgVisionRange - organismSentry.Random.Next(organismSentry.MaxOrgVisionRange);
        }

        public void EatFood()
        {
            organismSentry.EntityWasEaten<TFood>((X,Y));
        }

        private void checkReproduce()
        {
            if (WantReproduce)
            {
                Organism<T,TFood> potentialPartner = organismSentry.FindOrganismPartner<T,TFood>((X, Y), Male);
                if (potentialPartner != null)
                {
                    changeValuesOnReproduce();
                    potentialPartner.changeValuesOnReproduce();
                    MakeBaby();
                }
            }
        }

        private void makeMove(Direction direction)
        {
            organismSentry.OrganismWasDestroyedOnMap(this);
            changeValuesOnMove();
            move(direction);
            organismSentry.OrganismWasMadeOnMap(this);
        }
        private Direction makeDecision(Direction direction)
        {
            if (WantReproduce)
                direction = chooseDirection(findOnMap(Male));
            else if (WantFood)
                direction = chooseDirection(findOnMap(null));
            // no idea what to do
            if (direction == Direction.None)
                direction = RandomDirection8(organismSentry.Random);
            direction = finalDecision(direction);
            return direction;
        }
        private Direction finalDecision(Direction direction)
        {
            if (StutterFor++ >= StutterUntil)
            {
                StutterFor = 0;
                return direction;
            }
            return Direction.None;
        }
        private int setActualRange()
        {
            return (organismSentry.IsItDayToday()) ? OrganismRange : OrganismRange / 2;
        }
        private bool cellIsAppropriate((int, int) XY, bool? sex)
        {
            return organismSentry.CellIsAppropriate<T, TFood>(XY, sex);
        }
        private (int, int)? checkLines(int range, bool? sex)
        {
            // top
            for (int i = X - range; i < X + range; i++)
                if (cellIsAppropriate((i, Y - range), sex))
                    return (i, Y - range);
            // right
            for (int i = Y - range; i < Y + range; i++)
                if (cellIsAppropriate((X + range, i), sex))
                    return (X + range, i);
            // bottom
            for (int i = X + range; i > X - range; i--)
                if (cellIsAppropriate((i, Y + range), sex))
                    return (i, Y + range);
            // left
            for (int i = Y + range; i > Y - range; i--)
                if (cellIsAppropriate((X - range, i), sex))
                    return (X - range, i);
            return null;

        }
        private (int, int)? findOnMap(bool? sex)
        {
            int currentRange = 1;
            int maxRange = setActualRange();
            (int,int)? found = null;
            while (currentRange <= maxRange && found == null)
                found = checkLines(currentRange++, sex);
            return found;
        }
        private Direction chooseDirection((int, int)? goal)
        {
            if (goal == null)
                return Direction.None;
            else
            {
                (int, int) goalIndeed = ((int, int)) goal;
                if (X < goalIndeed.Item1)
                {
                    if (Y > goalIndeed.Item2)
                        return Direction.TopRight;
                    else if (Y == goalIndeed.Item2)
                        return Direction.Right;
                    else if (Y < goalIndeed.Item2)
                        return Direction.BottomRight;
                }
                else if (X > goalIndeed.Item1)
                {
                    if (Y < goalIndeed.Item2)
                        return Direction.BottomLeft;
                    else if (Y == goalIndeed.Item2)
                        return Direction.Left;
                    else if (Y > goalIndeed.Item2)
                        return Direction.TopLeft;
                }
                else if (X == goalIndeed.Item1)
                {
                    if (Y < goalIndeed.Item2)
                        return Direction.Bottom;
                    else if (Y > goalIndeed.Item2)
                        return Direction.Top;
                }
                return Direction.None;
            }
        }
        private void move(Direction direction)
        {
            switch (direction)
            {
                case Direction.TopLeft:
                    if (canStepOnCell((X - 1, Y)))
                        X--;
                    else if (canStepOnCell((X, Y - 1)))
                        Y--;
                    break;
                case Direction.Top:
                    if (canStepOnCell((X, Y - 1)))
                        Y--;
                    break;
                case Direction.TopRight:
                    if (canStepOnCell((X + 1, Y)))
                        X++;
                    else if (canStepOnCell((X, Y - 1)))
                        Y--;
                    break;
                case Direction.Right:
                    if (canStepOnCell((X + 1, Y)))
                        X++;
                    break;
                case Direction.BottomRight:
                    if (canStepOnCell((X + 1, Y)))
                        X++;
                    else if (canStepOnCell((X, Y + 1)))
                        Y++;
                    break;
                case Direction.Bottom:
                    if (canStepOnCell((X, Y + 1)))
                        Y++;
                    break;
                case Direction.BottomLeft:
                    if (canStepOnCell((X - 1, Y)))
                        X--;
                    else if (canStepOnCell((X, Y + 1)))
                        Y++;
                    break;
                case Direction.Left:
                    if (canStepOnCell((X - 1, Y)))
                        X--;
                    break;
            }
        }

        private bool canStepOnCell((int, int) XY)
        {
            return organismSentry.CanStepOnCell(XY);
        }

        // check if alive and increment DeadFor counter if needed
        private bool deadLongEnough()
        {
            if (!IsAlive && DeadFor >= DeadUntil)
                return true;
            else
                DeadFor++;
            return false;
        }
        private void changeValuesOnMove()
        {
            if (Fullness <= 0)
            {
                IsAlive = false;
            }
            else if (Fullness <= 10 && WantReproduce)
            {
                WantReproduce = false;
                WantFood = true;
                Fullness--;
            }
            else if (Fullness < 50 && !WantReproduce)
            {
                WantFood = true;
                Fullness--;
            }
            else
            {
                Fullness--;
                if (ReproducedFor >= NoReproduceUntil)
                {
                    WantReproduce = true;
                    WantFood = false;
                }
                else
                    ReproducedFor++;
            }
        }
        private void changeValuesOnReproduce()
        {
            ReproducedFor = 0;
            WantReproduce = false;
        }
        private void changeValuesOnEating()
        {
            Fullness = 100;
            WantFood = false;
        }
    }
}
