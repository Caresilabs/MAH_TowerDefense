using MAH_TowerDefense.Entity.Bullets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity
{
    public interface IHitable
    {
        /// <summary>
        /// Inject a object with a hitmodifier
        /// </summary>
        /// <param name="modifier"></param>
        /// <returns>Whenever it was a valid hit or not</returns>
        bool Hit(HitModifier modifier);
    }
}
