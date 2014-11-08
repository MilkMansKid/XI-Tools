using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroLimits.XITool;
using ZeroLimits.XITool.Classes;

namespace ZeroLimits.XITool.Interfaces
{
    public interface IAbilityService
    {
        Ability CreateAbility(string name);
        ICollection<Ability> GetAbilitiesWithName(String name);
        ICollection<Ability> GetJobAbilitiesByName(string name);
        ICollection<Ability> GetSpellAbilitiesByName(string name);
        bool Exists(string actionName);
        bool IsDistanceEnabled { get; set; }
    }
}
