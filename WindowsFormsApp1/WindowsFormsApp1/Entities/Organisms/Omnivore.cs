using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    // всеядное
    public class Omnivore : Organism<Entity>
    {
        public Omnivore(int _x, int _y, bool _male, int _range, int _rollBack, int _deadUntil, Map _map) : base(_x, _y, _male, _range, _rollBack, _deadUntil, _map)
        {
        }
        public static Omnivore RandSpawn(Map map)
        {
            (int, int, bool, int, int, int) randValues = RandSpawnValues(map);
            return new Omnivore(randValues.Item1, randValues.Item2, randValues.Item3, randValues.Item4, randValues.Item5, randValues.Item6, map);
        }
        public override void becomingPlant()
        {
            map.OmnivoreBecamePlant(this);
        }
        public override void makeBaby()
        {
            (int, int, bool, int, int, int) babyValues = MakeBabyValues();
            map.OmnivoreBabyWasMade(new Omnivore(babyValues.Item1, babyValues.Item2, babyValues.Item3, babyValues.Item4, babyValues.Item5, babyValues.Item6, map));
        }
    }
}
