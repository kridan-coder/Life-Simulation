using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Human : Omnivore<Human, EdibleForHuman>, EdibleForBear, EdibleForLion, EdibleForWolf
    {
        Human partner;
        House home;
        Plant carriedPlant;
        public int Power = 0;
        public bool NeedToCollect = false;
        public bool NeedToBuild = false;
        public Human(int _x, int _y, Sex _sex, int _range, int _rollBack, int _deadUntil, int _stutter, OrganismSentry _organismSentry) : base(_x, _y, _sex, _range, _rollBack, _deadUntil, _stutter, _organismSentry)
        {
        }


        public override void NextMove()
        {
            base.NextMove();
            if (IsAlive && Sex == Sex.Male)
            {
                CheckHouse();
            }
        }

        public void CheckHouse()
        {
            if (NeedToBuild && HouseCanBeBuiltOnCell())
            {
                ChangeValuesOnHouseBuilding();
                BuildHouse();
            }
        }

        public bool HouseCanBeBuiltOnCell()
        {

            return false;
        }

        public void ChangeValuesOnHouseBuilding()
        {

        }

        public void BuildHouse()
        {
            Power = Fullness / 10;
            home = OrganismSentry.AddAndSummonHouse(Power, X, Y, new List<Human>() { this, partner });
        }

        public override Direction MakeDecisionWhereToGo(Direction direction)
        {
            if (WantReproduce)
                direction = ChooseDirection((home.startX, home.startY));
            else if (WantFood || !HomeStonksAreFull())
                direction = ChooseDirection(FindOnMap(null));
            else if (NeedToBuild)
                direction = 
            // no idea what to do
            else if (home == null || !ThisIsHome((X,Y)))
                direction = RandomDirection8(OrganismSentry.Random);
            direction = FinalDecision(direction);
            return direction;
        }


        public override bool CellIsAppropriate((int, int) XY, Sex? sex)
        {
            return base.CellIsAppropriate(XY, sex) || (home != null && ThisIsHome(XY) && HomeHasFood() && !NeedToCollect);
        }

        public override void CheckFood()
        {
            if (FoodIsOnCell())
            {
                if (WantFood)
                {
                    ChangeValuesOnEating();
                    EatFood();
                }
                else if (carriedPlant == null)
                {
                    carriedPlant = (Plant)EatFood();
                }
            }
            else if (WantFood && carriedPlant != null)
            {
                ChangeValuesOnEating();
                carriedPlant = null;
            }
        }

        public override void CheckReproduce()
        {
            if (WantReproduce)
            {
                if (readyToMakeBaby())
                {
                    ChangeValuesOnReproduce();
                    partner.ChangeValuesOnReproduce();
                    OrganismSentry.CreateOrganism(MakeBaby((X, Y), OrganismSentry));
                }
            }
        }

        private bool readyToMakeBaby()
        {
            return partner != null && 
                home != null &&
                partner.WantReproduce &&
                partner.X == X && partner.Y == Y
                && ThisIsHome((X, Y));
        }

        private bool ThisIsHome((int, int) XY)
        {
            for (int i = 0; i < home.HouseParts.Count() - 1; i++)
            {
                if (home.HouseParts[i].X == XY.Item1 && home.HouseParts[i].Y == XY.Item2)
                {
                    return true;
                }
            }
            return false;
        }

        public override void ChangeValuesOnMove()
        {
            base.ChangeValuesOnMove();
            if (Sex == Sex.Female)
            {
                if (home != null && !HomeStonksAreFull())
                {
                    NeedToCollect = true;
                }
                else
                {
                    NeedToCollect = false;
                }
            }
            else
            {
                if (home == null && partner != null)
                {
                    NeedToBuild = true;
                }
                else
                {
                    NeedToBuild = false;
                }
            }
        }

        private bool HomeHasFood()
        {
            return home.Stonks.Any(); 
        }

        private bool HomeStonksAreFull()
        {
            return home.Stonks.Count() >= home.StonksMax;
        }
    }
}
