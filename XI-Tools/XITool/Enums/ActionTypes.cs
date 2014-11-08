using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroLimits.XITool.Enums
{
    [Flags]
    public enum ActionType  
    {
        None = 0,
        Ability = 1,
        Spell = 2,
        WeaponSkill = 4,
        Ranged = 8,
        Melee = 16,
        Item = 32
    }
}
