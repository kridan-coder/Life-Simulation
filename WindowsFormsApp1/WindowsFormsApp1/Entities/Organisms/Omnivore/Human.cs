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
        public int Power = 0;
        public Human(int _x, int _y, Sex _sex, int _range, int _rollBack, int _deadUntil, int _stutter, AnimalSentry _organismSentry) : base(_x, _y, _sex, _range, _rollBack, _deadUntil, _stutter, _organismSentry)
        {
        }


        public override void NextMove()
        {
            base.NextMove();

        }
    }
}
