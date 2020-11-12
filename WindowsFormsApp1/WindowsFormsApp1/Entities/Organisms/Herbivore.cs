using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApp1
{
    // травоядное
    public class Herbivore : Organism<Plant>, EdibleForPredatory, EdibleForOmnivore
    {
        public Herbivore(int _x, int _y, bool _male, int _range, int _rollBack, int _deadUntil, Map _map) : base(_x, _y, _male, _range, _rollBack, _deadUntil, _map)
        {
        }

        public static Herbivore RandSpawn(Map map)
        {
            (int, int, bool, int, int, int) randValues = RandSpawnValues(map);
            return new Herbivore(randValues.Item1, randValues.Item2, randValues.Item3, randValues.Item4, randValues.Item5, randValues.Item6, map);
        }

        public override void becomingPlant()
        {
            map.HerbivoreBecamePlant(this);
        }

        public override void EatFood()
        {
            map.PlantWasEaten(x, y);
        }

        public override Direction finalDecision(Direction direction)
        {
            return (map.random.Next(100) < 15) ? Direction.None : direction;
        }

        public override void makeBaby()
        {
            (int, int, bool, int, int, int) babyValues = MakeBabyValues();
            map.HerbivoreBabyWasMade(new Herbivore(babyValues.Item1, babyValues.Item2, babyValues.Item3, babyValues.Item4, babyValues.Item5, babyValues.Item6, map));
        }
    }
}
