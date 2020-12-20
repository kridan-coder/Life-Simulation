﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Mouse : Herbivore<Mouse, EdibleForMouse>,
        /*Mouse is*/ EdibleForHuman, EdibleForRaccoon, EdibleForFox
    {
        public Mouse(int _x, int _y, Sex _sex, int _range, int _rollBack, int _deadUntil, int _stutter, OrganismSentry _organismSentry) : base(_x, _y, _sex, _range, _rollBack, _deadUntil, _stutter, _organismSentry)
        {
        }
    }
}
