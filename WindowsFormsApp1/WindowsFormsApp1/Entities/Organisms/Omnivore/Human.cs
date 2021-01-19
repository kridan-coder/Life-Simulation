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
        Barn nearestBarn;

        Entity carriedFood;

        public int Power = 0;
        public bool NeedToCollect = false;
        public bool NeedToBuildHouse = false;
        public bool NeedToBuildBarn = false;
        public bool BabyNow = false;

        public int BabyFor = 0;
        public int BabyUntil = 50;

        public (int, int)? NearestHouseXY;
        public (int, int)? NearestBarnXY;
        public Human(int _x, int _y, Sex _sex, int _range, int _rollBack, int _deadUntil, int _stutter, OrganismSentry _organismSentry) : base(_x, _y, _sex, _range, _rollBack, _deadUntil, _stutter, _organismSentry)
        {
        }
        public override void NextMove()
        {
            base.NextMove();
            if (IsAlive)
            {
                if (Sex == Sex.Male)
                    CheckHouse();
                CheckBarn();
            }
        }

        public void CheckHouse()
        {
            if (NeedToBuildHouse && NearestBuildingIsNotClose(NearestHouseXY) && NearestBuildingIsNotFar(NearestHouseXY))
            {
                ChangeValuesOnHouseBuilding();
                BuildHouse();
            }
        }

        public void CheckBarn()
        {
            if (NeedToBuildBarn && NearestBuildingIsNotClose(NearestBarnXY) && NearestBuildingIsNotFar(NearestBarnXY) && NearestBuildingIsNotClose(NearestHouseXY) && NearestBuildingIsNotFar(NearestHouseXY))
            {
                ChangeValuesOnBarnBuilding();
                BuildBarn();
            }
        }

        public bool NearestBuildingIsNotClose((int, int)? NearestBuilding)
        {
            if (NearestBuilding != null)
            {
                (int, int) temp = ((int, int))NearestBuilding;
                if ((Math.Abs(temp.Item1 - X) < Power + 3 || Math.Abs(temp.Item2 - Y) < Power + 3))
                    return false;
            }
            return true;
        }

        public bool NearestBuildingIsNotFar((int, int)? NearestBuilding)
        {
            if (NearestBuilding != null)
            {
                (int, int) temp = ((int, int))NearestBuilding;
                if ((Math.Abs(temp.Item1 - X) > Power + 5 || Math.Abs(temp.Item2 - Y) > Power + 5))
                    return false;
            }
            return true;
        }

        public void ChangeValuesOnHouseBuilding()
        {
            NeedToBuildHouse = false;
        }

        public void ChangeValuesOnBarnBuilding()
        {
            NeedToBuildBarn = false;
        }

        public void BuildHouse()
        {
            home = OrganismSentry.AddAndSummonHouse(0, X, Y, new List<Human>() { this, partner });
            partner.home = home;
        }

        public void BuildBarn()
        {
            OrganismSentry.AddAndSummonBarn(Power, X, Y);
        }

        public virtual bool FindingPartner()
        {
            return true;
        }

        public Barn? getNearestBarn((int, int)? nearestBarnXY)
        {
            if (nearestBarnXY != null)
            {
                (int, int) temp = ((int, int))nearestBarnXY;
                return OrganismSentry.FindBarnOnCell(temp);
            }
            return null;
        }

        public override Direction MakeDecisionWhereToGo(Direction direction)
        {
            NearestHouseXY = FindOnMap(SearchAnyHouse, null, null);
            NearestBarnXY = FindOnMap(SearchAnyBarn, null, null);
            nearestBarn = getNearestBarn(NearestBarnXY);
            if (home == null)
            {
                if (partner == null && WantReproduce)
                    direction = ChooseDirection(FindOnMap(SearchFoodOrPartner, Sex, null));
                if (WantFood && nearestBarn != null && BarnHasFood())
                    direction = ChooseDirection(NearestBarnXY);
                else if (WantFood)
                    direction = ChooseDirection(FindOnMap(SearchFoodOrPartner, null, null));
                if (NeedToBuildHouse)
                {
                    if (!NearestBuildingIsNotClose(NearestHouseXY))
                        direction = RandomDirection8(OrganismSentry.Random);
                    else if (!NearestBuildingIsNotFar(NearestHouseXY))
                        direction = ChooseDirection(NearestHouseXY);
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
                else if (WantFood && nearestBarn != null && BarnHasFood())
                    direction = ChooseDirection(NearestBarnXY);
                else if (carriedFood != null)
                {
                    if (HomeStonksAreFull())
                    {
                        if (nearestBarn != null)
                        {
                            if (BarnStonksAreFull())
                            {
                                if (!NearestBuildingIsNotClose(NearestBarnXY))
                                    direction = RandomDirection8(OrganismSentry.Random);
                                else if (!NearestBuildingIsNotFar(NearestBarnXY))
                                    direction = ChooseDirection(NearestBarnXY);
                            }
                            else
                            {
                                direction = ChooseDirection(NearestBarnXY);
                            }
                        }
                        else
                        {
                            if (!NearestBuildingIsNotClose(NearestHouseXY))
                                direction = RandomDirection8(OrganismSentry.Random);
                            else if (!NearestBuildingIsNotFar(NearestHouseXY))
                                direction = ChooseDirection(NearestHouseXY);
                        }
                    }
                    else
                    {
                        direction = ChooseDirection((home.startX, home.startY));
                    }
                }

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
                else if (carriedFood == null && Sex == Sex.Female && home != null)
                {
                    Entity temp = EatFood();
                    if (temp is SuitableForGathering)
                        carriedFood = (Plant)temp;
                }
                else if (carriedFood == null && Sex == Sex.Male && home != null)
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
            else if (WantFood && ThisIsBarn((X,Y)) && BarnHasFood())
            {
                nearestBarn.Stonks.RemoveAt(nearestBarn.Stonks.Count - 1);
                ChangeValuesOnEating();
            }
            else if (carriedFood != null && ThisIsHome((X,Y)))
            {
                home.Stonks.Add(carriedFood);
                carriedFood = null;
            }
            else if (carriedFood != null && ThisIsBarn((X, Y)))
            {
                nearestBarn.Stonks.Add(carriedFood);
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
                    baby.BabyNow = true;
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

        private bool ThisIsBarn((int, int) XY)
        {
            if (nearestBarn != null)
            {
                for (int i = 0; i < nearestBarn.BarnParts.Count(); i++)
                {
                    if (nearestBarn.BarnParts[i].X == XY.Item1 && nearestBarn.BarnParts[i].Y == XY.Item2)
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
                        NeedToBuildHouse = true;
                    }
                    else
                    {
                        NeedToBuildHouse = false;
                    }
                }
                else
                {
                    NeedToCollect = true;
                    NearestBarnXY = FindOnMap(SearchAnyBarn, null, null);
                    nearestBarn = getNearestBarn(NearestBarnXY);
                    if (HomeStonksAreFull() && (NearestBarnXY == null || BarnStonksAreFull()))
                    {
                        NeedToBuildBarn = true;
                    }
                    else
                    {
                        NeedToBuildBarn = false;
                    }
                }
            }
            else if (Sex == Sex.Female)
            {
                if (home != null)
                {
                    NeedToCollect = true;
                    NearestBarnXY = FindOnMap(SearchAnyBarn, null, null);
                    nearestBarn = getNearestBarn(NearestBarnXY);
                    if (HomeStonksAreFull() && (NearestBarnXY == null || BarnStonksAreFull()))
                    {
                        NeedToBuildBarn = true;
                    }
                    else
                    {
                        NeedToBuildBarn = false;
                    }
                }
            }

            if (BabyNow == true)
            {
                if (BabyFor++ >= BabyUntil)
                {
                    BabyNow = false;
                    home = null;
                }
                else
                {
                    WantReproduce = false;
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

        private bool BarnHasFood()
        {
            return nearestBarn.Stonks.Any();
        }

        private bool BarnStonksAreFull()
        {
            return nearestBarn.Stonks.Count() >= nearestBarn.StonksMax;
        }
    }
}
