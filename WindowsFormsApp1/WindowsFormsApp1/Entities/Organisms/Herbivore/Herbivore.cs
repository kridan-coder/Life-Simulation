﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApp1
{
    // травоядное
    public abstract class Herbivore<T, TFood> : Organism<T, TFood>, EdibleForPredatory, EdibleForOmnivore
        where T : Organism
        where TFood : EdibleForHerbivore
    {
        public Herbivore(int _x, int _y, bool _male, int _range, int _rollBack, int _deadUntil, int _stutter, OrganismSentry _organismSentry) : base(_x, _y, _male, _range, _rollBack, _deadUntil, _stutter, _organismSentry)
        {
        }
    }
}