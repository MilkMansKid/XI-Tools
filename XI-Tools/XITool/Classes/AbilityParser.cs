
/*///////////////////////////////////////////////////////////////////
<EasyFarm, general farming utility for FFXI.>
Copyright (C) <2013>  <Zerolimits>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
*/
///////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ZeroLimits.XITool.Enums;

namespace ZeroLimits.XITool.Classes
{
    /// <summary>
    /// A class for loading the ability and spell xmls from file.
    /// </summary>
    public class AbilityParser
    {
        private const string ABILS = "abils.xml";
        private const string SPELLS = "spells.xml";

        protected static XElement _spelldoc = null;
        protected static XElement _abilsdoc = null;

        /// <summary>
        /// Class load time initializer
        /// </summary>
        static AbilityParser()
        {
            _abilsdoc = LoadResource(ABILS);
            _spelldoc = LoadResource(SPELLS);
        }

        /// <summary>
        /// Ensures that the resource file passed exists
        /// and returns the XElement obj associated with the file.
        /// </summary>
        /// <param name="abils"></param>
        /// <returns></returns>
        private static XElement LoadResource(string abils)
        {
            XElement XMLDoc = null;

            String WorkingDirectory = Directory.GetCurrentDirectory();

            // Change to the resources directory if it exists.
            if (Directory.Exists("resources"))
            {
                Directory.SetCurrentDirectory("resources");

                // We can't operate without the resource files, shut it down.
                if (File.Exists(abils))
                {
                    XMLDoc = XElement.Load(abils);
                }

                // Revert to previous directory
                Directory.SetCurrentDirectory(WorkingDirectory);
            }

            return XMLDoc;
        }

        /// <summary>
        /// Grabs all abilities from the resource files with the specified name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected ICollection<Ability> ParseActions(String name)
        {
            // Parse spells and set spell status field.
            var spells = ParseResources("s", _spelldoc, name);
            foreach (var value in spells) value.ActionType = ActionType.Spell;

            // Parse abilities and set ability status field. 
            var abilities = ParseResources("a", _abilsdoc, name);
            foreach (var value in abilities) value.ActionType = ActionType.Ability;

            // Create a list containing both spells and abilities. 
            var resources = spells.Union(abilities).ToList();

            foreach (var value in resources)
            {
                if (string.Equals(value.Prefix, "/range", StringComparison.OrdinalIgnoreCase))
                    value.ActionType = ActionType.Ranged;
            }

            // Set their use distances.
            if (IsDistanceEnabled) { SetDistances(resources); }

            if (IsRangedDelayEnabled) { SetRangedDelay(resources); }

            return resources;
        }

        private void SetRangedDelay(List<Ability> resources)
        {
            resources.FindAll(x => x.ActionType.Equals(ActionType.Ranged))
                 .ForEach(x => x.RangedDelay = this.RangedDelay);
        }

        /// <summary>
        /// Grabs all abilities from the resource files with the specified name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected ICollection<Ability> ParseAbilities(String name)
        {
            return ParseActions(name)
                .Where(x => x.ActionType == ActionType.Ability)
                .ToList();
        }

        /// <summary>
        /// Grabs all abilities from the resource files with the specified name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected ICollection<Ability> ParseSpells(String name)
        {
            return ParseActions(name)
                .Where(x => x.ActionType == ActionType.Spell)
                .ToList();
        }

        /// <summary>
        /// A general method for loading abilites from the .xml files. 
        /// </summary>
        /// <param name="pname">a or s for spell or ability</param>
        /// <param name="XDoc"></param>
        /// <param name="aname">Name of the ability to retrieve</param>
        /// <returns></returns>
        protected ICollection<Ability> ParseResources(String pname, XElement XDoc, String aname)
        {
            var Abilities = new List<Ability>();

            // Fetches the ability from xml.
            var element = XDoc.Elements(pname).Attributes()
                .Where(x => (x.Name == "english" && x.Value == aname))
                .Select(x => x.Parent);

            // Return blank if we did not find the ability.
            if (element == null) { return Abilities; }

            // Loop through all attributes and 
            // create an ability for each one. 
            foreach (var e in element)
            {
                Ability Ability = new Ability();

                Ability.Alias = (string)e.Attribute("alias");
                Ability.Element = (string)e.Attribute("element");
                Ability.Name = (string)e.Attribute("english");
                Ability.Prefix = (string)e.Attribute("prefix");
                Ability.Skill = (string)e.Attribute("skill");
                Ability.Targets = (string)e.Attribute("targets");
                Ability.Type = (string)e.Attribute("type");
                Ability.CastTime = (double)e.Attribute("casttime");
                Ability.ID = (int)e.Attribute("id");
                Ability.Index = (int)e.Attribute("index");
                Ability.MPCost = (int)e.Attribute("mpcost");
                Ability.Recast = (double)e.Attribute("recast");
                Ability.TPCost = (int?)e.Attribute("tpcost") ?? 0;

                Abilities.Add(Ability);
            }

            return Abilities;
        }

        /// <summary>
        /// Set whether distances should be set when abilities are created. 
        /// </summary>
        public bool IsDistanceEnabled { get; set; }

        /// <summary>
        /// Set whether ranged delay should be set when abilties are created. 
        /// </summary>
        public bool IsRangedDelayEnabled { get; set; }

        /// <summary>
        /// Takes a list of ability and returns a modified list with distances
        /// that tell what at distance it should be used. 
        /// </summary>
        /// <param name="abilities"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public void SetDistances(List<Ability> abilities)
        {
            abilities.ForEach(x => x.Distance = GetDistance(x));
        }

        /// <summary>
        /// Returns the ability distance depending on the abilities type.
        /// </summary>
        /// <param name="ability"></param>
        /// <returns></returns>
        public double GetDistance(Ability ability)
        {
            // The distance we should approach from. 
            double approach = 0;

            // If spell, set to spell distance. 
            if (ability.ActionType.Equals(ActionType.Spell))
            {
                approach = Constants.SPELL_CAST_DISTANCE;
            }
            // if ranged, set to ranged distance. 
            else if (ability.ActionType.Equals(ActionType.Ranged))
            {
                approach = Constants.RANGED_ATTACK_MAX_DISTANCE;
            }
            // if melee, set to melee distance.
            else
            {
                approach = Constants.MELEE_DISTANCE;
            }

            return approach;
        }

        /// <summary>
        /// Value used for for setting the ranged delay on abilities.
        /// Must be set by consumer for it to work. 
        /// </summary>
        public double RangedDelay { get; set; }
    }
}