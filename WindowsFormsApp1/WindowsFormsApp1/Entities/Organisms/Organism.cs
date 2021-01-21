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
        public static OrganismSentry OrganismSentry;

        public bool IsAlive;

        public int OrganismRange;

        public int DeadUntil;
        public int NoReproduceUntil;
        public int StutterUntil;

        public Sex Sex;

        public int DeadFor = 0;
        public int ReproducedFor = 0;
        public int Fullness = 100;
        public int StutterFor = 0;

        public bool WantFood = false;
        public bool WantReproduce = false;

        public Organism(int _x, int _y, Sex _sex, int _range, int _rollBack, int _deadUntil, OrganismSentry _organismSentry) : base(_x, _y)
        {
            OrganismSentry = _organismSentry;
            Sex = _sex;
            OrganismRange = _range;
            DeadUntil = _deadUntil;
            NoReproduceUntil = _rollBack;
            IsAlive = true;
        }

        public delegate bool SearchDelegate((int, int) XY, Sex? sex, House house);
        public SearchDelegate SearchForGatheringOrHunting = (XY, sex, house) => (sex == Sex.Female) ? OrganismSentry.CellIsAppropriateForFoodOrPartner<T, SuitableForGathering>(XY, null) : OrganismSentry.CellIsAppropriateForFoodOrPartner<T, SuitableForHunting>(XY, null);
        public SearchDelegate SearchFoodOrPartner = (XY, sex, house) => OrganismSentry.CellIsAppropriateForFoodOrPartner<T, TFood>(XY, sex);
        public SearchDelegate SearchAnyHouse = (XY, sex, house) => OrganismSentry.IsOnCell<HousePart>(XY);
        public SearchDelegate SearchAnyBarn = (XY, sex, house) => OrganismSentry.IsOnCell<BarnPart>(XY);
        public SearchDelegate SearchSpecificHouse = (XY, sex, house) => OrganismSentry.SpecificHouseIsOnCell(XY, house);

        public override void SetStutter(Cell cell)
        {
            if (cell.CellState == CellState.Water)
            {
                StutterUntil = 3;
            }
            else if (cell.CellState == CellState.Sand)
            {
                StutterUntil = 2;
            }
            else if (cell.CellState == CellState.Grass)
            {
                StutterUntil = 0;
            }
            else if (cell.CellState == CellState.Hill)
            {
                StutterUntil = 1;
            }
        }

        public override bool GetIsAlive()
        {
            return IsAlive;
        }

        public override Sex GetSex()
        {
            return Sex;
        }

        public override bool GetReproduceWish()
        {
            return WantReproduce;
        }

        public override bool GetFoodEatingWish()
        {
            return WantFood;
        }

        public override int GetFullness()
        {
            return Fullness;
        }


        public override int GetOrganismRange()
        {
            return OrganismRange;
        }

        public override int GetBeforeBecomingPlant()
        {
            return (DeadUntil - DeadFor);
        }

        public override string GetOrganismType()
        {
            return typeof(T).Name;
        }
        public override void NextMove()
        {
            Direction direction = Direction.None;
            if (IsAlive)
            {
                CheckFood();
                CheckReproduce();
                direction = MakeDecisionWhereToGo(direction);
                MakeMove(direction);
            }
            else if (DeadLongEnough())
                BecomingPlant();
        }
        
        public virtual void CheckFood()
        {
            if (FoodIsOnCell() && WantFood)
            {
                ChangeValuesOnEating();
                EatFood();
                return;
            }
        }

        public bool FoodIsOnCell()
        {
            return OrganismSentry.IsOnCell<TFood>((X, Y));
        }

        public bool PlantIsOnCell()
        {
            return OrganismSentry.IsOnCell<Plant>((X, Y));
        }

        public void BecomingPlant()
        {
            OrganismSentry.OrganismBecamePlant(this);
        }

        public static Organism RandSpawn(OrganismSentry organismSentry)
        {
            (int, int) XY;
            int orgRange;
            Sex Sex;
            while (true)
            {
                XY = organismSentry.GetRandCoordsOnMap();
                if (organismSentry.CellIsEmpty(XY))
                {
                    Sex = randomSex(organismSentry);
                    orgRange = randomVisionRange(organismSentry);
                    return SetOrganism<T>(XY, orgRange, Sex, organismSentry);
                }
            }
        }

        public static Organism SetOrganism<Type>((int, int) XY, int range, Sex Sex,  OrganismSentry organismSentry) 
            where Type : Organism
        {

            var org = (Type)Activator.CreateInstance(typeof(Type), new object[] { XY.Item1, XY.Item2, Sex, range, organismSentry.Random.Next(organismSentry.MaxOrgTicksBeforeReproducing) + 1, organismSentry.Random.Next(organismSentry.MaxOrgTicksBeforeBecomingGrass) + 1, organismSentry });
            org.last_X = XY.Item1; org.last_Y = XY.Item2;
            return org;
        }

        public static Organism MakeBaby((int, int)XY, OrganismSentry organismSentry)
        {
            Sex babySex;
            int babyRange;
            babySex = randomSex(organismSentry);
            babyRange = randomVisionRange(organismSentry);
            return SetOrganism<T>(XY, babyRange, babySex, organismSentry);
        }

        private static Sex randomSex(OrganismSentry organismSentry)
        {
            return (organismSentry.Random.Next(100) > 50)? Sex.Female : Sex.Male;
        }

        private static int randomVisionRange(OrganismSentry organismSentry)
        {
            return organismSentry.MaxOrgVisionRange - organismSentry.Random.Next(organismSentry.MaxOrgVisionRange);
        }

        public Entity EatFood()
        {
            return OrganismSentry.EntityWasEaten<TFood>((X,Y));
        }

        public Organism<T, TFood> IsPotentialPartnerOnThisCell((int, int) XY, Sex sex)
        {
            return OrganismSentry.FindOrganismPartner<T, TFood>((X, Y), Sex);
        }
        public virtual void CheckReproduce()
        {
            if (WantReproduce)
            {
                Organism<T, TFood> potentialPartner = IsPotentialPartnerOnThisCell((X,Y), Sex);
                if (potentialPartner != null)
                {
                    ChangeValuesOnReproduce();
                    potentialPartner.ChangeValuesOnReproduce();
                    OrganismSentry.CreateOrganism(MakeBaby((X,Y), OrganismSentry));
                }
            }
        }

        public void MakeMove(Direction direction)
        {
            OrganismSentry.OrganismWasDestroyedOnCell(this);
            move(direction);
            ChangeValuesOnMove();
            OrganismSentry.OrganismWasMadeOnCell(this);
        }
        public virtual Direction MakeDecisionWhereToGo(Direction direction)
        {
            if (WantReproduce)
                direction = ChooseDirection(FindOnMap(SearchFoodOrPartner, Sex, null));
            else if (WantFood)
                direction = ChooseDirection(FindOnMap(SearchFoodOrPartner, null, null));
            // no idea what to do
            if (direction == Direction.None)
                direction = RandomDirection8(OrganismSentry.Random);
            direction = FinalDecision(direction);
            return direction;
        }
        public Direction FinalDecision(Direction direction)
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
            return (OrganismSentry.IsItDayToday()) ? OrganismRange : OrganismRange / 2;
        }



        //public virtual bool CellIsAppropriate((int, int) XY, Sex? sex)
        //{
        //    return SearchFoodOrPartner(XY, sex);
        //    return OrganismSentry.CellIsAppropriateForFoodOrPartner<T, TFood>(XY, sex);
        //}

        private (int, int)? checkLines(int range, SearchDelegate deleg, Sex? sex, House house)
        {

            if (range == 0)
            {
                if (deleg((X, Y), sex, house))
                    return (X, Y);
            }
            else
            {
                // top
                for (int i = X - range; i < X + range; i++)
                    if (deleg((i, Y - range), sex, house))
                        return (i, Y - range);
                // right
                for (int i = Y - range; i < Y + range; i++)
                    if (deleg((X + range, i), sex, house))
                        return (X + range, i);
                // bottom
                for (int i = X + range; i > X - range; i--)
                    if (deleg((i, Y + range), sex, house))
                        return (i, Y + range);
                // left
                for (int i = Y + range; i > Y - range; i--)
                    if (deleg((X - range, i), sex, house))
                        return (X - range, i);
            }
            return null;
        }
        public (int, int)? FindOnMap(SearchDelegate deleg, Sex? sex, House house)
        {
            int currentRange = 0;
            int maxRange = setActualRange();
            (int, int)? found = null;
            while (currentRange <= maxRange && found == null)
                found = checkLines(currentRange++, deleg, sex, house);
            return found;
        }
        public Direction ChooseDirection((int, int)? goal)
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
            bool thisFirst = (OrganismSentry.Random.Next(100) > 50) ? true : false;
            last_X = X;
            last_Y = Y;
            switch (direction)
            {
                case Direction.TopLeft:
                    if (thisFirst)
                    {
                        if (canStepOnCell((X - 1, Y)))
                            X--;
                        else if (canStepOnCell((X, Y - 1)))
                            Y--;
                    }
                    else
                    {
                        if (canStepOnCell((X, Y - 1)))
                            Y--;
                        else if (canStepOnCell((X - 1, Y)))
                            X--;
                    }
                    break;
                case Direction.Top:
                    if (canStepOnCell((X, Y - 1)))
                        Y--;
                    break;
                case Direction.TopRight:
                    if (thisFirst)
                    {
                        if (canStepOnCell((X + 1, Y)))
                            X++;
                        else if (canStepOnCell((X, Y - 1)))
                            Y--;
                    }
                    else
                    {
                        if (canStepOnCell((X, Y - 1)))
                            Y--;
                        else if (canStepOnCell((X + 1, Y)))
                            X++;
                    }
                    break;
                case Direction.Right:
                    if (canStepOnCell((X + 1, Y)))
                        X++;
                    break;
                case Direction.BottomRight:
                    if (thisFirst)
                    {
                        if (canStepOnCell((X + 1, Y)))
                            X++;
                        else if (canStepOnCell((X, Y + 1)))
                            Y++;
                    }
                    else
                    {
                        if (canStepOnCell((X, Y + 1)))
                            Y++;
                        else if (canStepOnCell((X + 1, Y)))
                            X++;
                    }

                    break;
                case Direction.Bottom:
                    if (canStepOnCell((X, Y + 1)))
                        Y++;
                    break;
                case Direction.BottomLeft:
                    if (thisFirst)
                    {
                        if (canStepOnCell((X - 1, Y)))
                            X--;
                        else if (canStepOnCell((X, Y + 1)))
                            Y++;
                    }
                    else
                    {
                        if (canStepOnCell((X, Y + 1)))
                            Y++;
                        else if (canStepOnCell((X - 1, Y)))
                            X--;
                    }

                    break;
                case Direction.Left:
                    if (canStepOnCell((X - 1, Y)))
                        X--;
                    break;
            }
        }

        private bool canStepOnCell((int, int) XY)
        {
            return OrganismSentry.CanStepOnCell(XY);
        }

        // check if alive and increment DeadFor counter if needed
        public bool DeadLongEnough()
        {
            if (!IsAlive && DeadFor >= DeadUntil)
                return true;
            else
                DeadFor++;
            return false;
        }
        public virtual void ChangeValuesOnMove()
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
        public void ChangeValuesOnReproduce()
        {
            ReproducedFor = 0;
            WantReproduce = false;
        }
        public void ChangeValuesOnEating()
        {
            Fullness = 100;
            WantFood = false;
        }
    }
}
