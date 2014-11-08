using FFACETools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZeroLimits.XITool.Classes
{
    public class XITools
    {
        /// <summary>
        /// The current fface instance bound to farming tools. 
        /// </summary>
        private FFACE _fface;

        public XITools(FFACE fface)
        {
            _fface = fface;
            this.AbilityExecutor = new AbilityExecutor(fface);
            this.AbilityService = new AbilityService();
            this.CombatService = new CombatService(fface);
            this.RestingService = new RestingService(fface);
            this.UnitService = new UnitService(fface);
            this.ActionBlocked = new ActionBlocked(fface);
        }

        /// <summary>
        /// Provides access to FFACE memory reading api which returns details
        /// about various game environment objects. 
        /// </summary>
        public FFACE FFACE
        {
            get { return _fface; }
            set { _fface = value; }
        }

        /// <summary>
        /// Provides services for acquiring ability/spell data.
        /// </summary>
        public AbilityService AbilityService { get; set; }

        /// <summary>
        /// Provides the ability to executor abilities/spells.
        /// </summary>
        public AbilityExecutor AbilityExecutor { get; set; }

        /// <summary>
        /// Provides methods for performing battle.
        /// </summary>
        public CombatService CombatService { get; set; }

        /// <summary>
        /// Provides methods for resting our character.
        /// </summary>
        public RestingService RestingService { get; set; }

        /// <summary>
        /// Provide details about the units around us. 
        /// </summary>
        public UnitService UnitService { get; set; }

        /// <summary>
        /// Provides methods on whether an ability/spell is usable.
        /// </summary>
        public ActionBlocked ActionBlocked { get; set; }
    }
}
