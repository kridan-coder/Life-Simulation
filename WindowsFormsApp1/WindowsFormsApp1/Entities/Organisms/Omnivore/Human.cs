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

        Entity carriedFood;

        public int Power = 0;
        public bool NeedToCollect = false;
        public bool NeedToBuild = false;
        public (int, int)? NearestHouse;
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
            if (NeedToBuild && NearestHouseIsNotClose() && NearestHouseIsNotFar())
            {
                ChangeValuesOnHouseBuilding();
                BuildHouse();
            }
        }

        public bool NearestHouseIsNotClose()
        {
            if (NearestHouse != null)
            {
                (int, int) temp = ((int, int))NearestHouse;
                if ((Math.Abs(temp.Item1 - X) < Power + 3 || Math.Abs(temp.Item2 - Y) < Power + 3))
                    return false;
            }
            return true;
        }

        public bool NearestHouseIsNotFar()
        {
            if (NearestHouse != null)
            {
                (int, int) temp = ((int, int))NearestHouse;
                if ((Math.Abs(temp.Item1 - X) > Power + 5 || Math.Abs(temp.Item2 - Y) > Power + 5))
                    return false;
            }
            return true;
        }

        public void ChangeValuesOnHouseBuilding()
        {
            NeedToBuild = false;
        }

        public void BuildHouse()
        {
            home = OrganismSentry.AddAndSummonHouse(Power, X, Y, new List<Human>() { this, partner });
            partner.home = home;
        }

        public virtual bool FindingPartner()
        {
            return true;
        }


        public override Direction MakeDecisionWhereToGo(Direction direction)
        {

            if (home == null)
            {
                NearestHouse = FindOnMap(SearchAnyHouse, null, null);
                if (partner == null && WantReproduce)
                    direction = ChooseDirection(FindOnMap(SearchFoodOrPartner, Sex, null));
                if (WantFood)
                    direction = ChooseDirection(FindOnMap(SearchFoodOrPartner, null, null));
                if (NeedToBuild)
                {
                    if (!NearestHouseIsNotClose())
                        direction = RandomDirection8(OrganismSentry.Random);
                    else if (!NearestHouseIsNotFar())
                        direction = ChooseDirection(NearestHouse);
                }
                if (direction == Direction.None)
                    direction = RandomDirection8(OrganismSentry.Random);
            }
            else if (home != null)
            {
                if (WantReproduce)
                {
                    if (partner == null)
                        direction = ChooseDirection(FindOnMap(SearchFoodOrPartner, Sex, null));
                    else
                        direction = ChooseDirection((home.startX, home.startY));
                }
                else if (WantFood && HomeHasFood())
                    direction = ChooseDirection(FindOnMap(SearchFoodOrPartner, null, home));
                else if ((WantFood && !HomeHasFood()) || (NeedToCollect && carriedFood == null))
                    direction = ChooseDirection(FindOnMap(SearchForGatheringOrHunting, Sex, null));

                else if (carriedFood != null)
                    direction = ChooseDirection((home.startX, home.startY));

                if (direction == Direction.None && !ThisIsHome((X, Y)))
                    direction = ChooseDirection((home.startX, home.startY));
            }

            direction = FinalDecision(direction);
            return direction;
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
                else if (carriedFood == null && Sex == Sex.Female && home != null && !HomeStonksAreFull())
                {
                    Entity temp = EatFood();
                    if (temp is SuitableForGathering)
                        carriedFood = (Plant)temp;
                }
                else if (carriedFood == null && Sex == Sex.Male && home != null && !HomeStonksAreFull())
                {
                    Entity temp = EatFood();
                    if (temp is SuitableForHunting)
                        carriedFood = (Organism)temp;
                }
            }
            else if (WantFood && carriedFood != null)
            {
                ChangeValuesOnEating();
                carriedFood = null;
            }
            else if (WantFood && ThisIsHome((X, Y)) && HomeHasFood())
            {
                home.Stonks.RemoveAt(home.Stonks.Count - 1);
                ChangeValuesOnEating();
            }
            else if (carriedFood != null && ThisIsHome((X,Y)))
            {
                home.Stonks.Add(carriedFood);
                carriedFood = null;
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
                    Human baby = (Human)MakeBaby((X, Y), OrganismSentry);
                    baby.home = home;
                    OrganismSentry.CreateOrganism(baby);
                }
                else if (partner == null)
                {
                    partner = (Human)IsPotentialPartnerOnThisCell((X, Y), Sex);
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
            if (home != null)
            {
                for (int i = 0; i < home.HouseParts.Count(); i++)
                {
                    if (home.HouseParts[i].X == XY.Item1 && home.HouseParts[i].Y == XY.Item2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override void ChangeValuesOnMove()
        {
            base.ChangeValuesOnMove();
            Power = Fullness / 40;
            if (IsAlive == false && partner != null)
            {
                partner.partner = null;
                partner = null;
            }

            if (Sex == Sex.Male)
            {
                if (home == null)
                {
                    if (partner != null)
                    {
                        NeedToBuild = true;
                    }
                    else
                    {
                        NeedToBuild = false;
                    }
                }
                else
                {
                    if (!HomeStonksAreFull())
                    {
                        NeedToCollect = true;
                    }
                    else
                    {
                        NeedToCollect = false;
                    }
                }
            }
            else if (Sex == Sex.Female)
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
