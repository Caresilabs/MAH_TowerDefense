﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity
{
    public class Unit : GameObject
    {
        public bool Selected { get; set; }

        public string Description { get; set; }

        public Unit(float x, float y, float width, float height) : base(x, y, width, height)
        {
            this.Alive = true;
        }

        public bool Alive { get; set; }
    }
}
