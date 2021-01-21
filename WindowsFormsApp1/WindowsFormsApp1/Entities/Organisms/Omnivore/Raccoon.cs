using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Raccoon : Omnivore<Raccoon, EdibleForRaccoon>,
        /*Raccoon is*/ EdibleForHuman, EdibleForBear, EdibleForLion, EdibleForWolf, SuitableForHunting
    {
        public Raccoon(int _x, int _y, Sex _sex, int _range, int _rollBack, int _deadUntil, OrganismSentry _organismSentry) : base(_x, _y, _sex, _range, _rollBack, _deadUntil, _organismSentry)
        {
        }
    }
}
