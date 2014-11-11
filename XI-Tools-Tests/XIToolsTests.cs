
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics;
using ZeroLimits.XITool.Classes;
using ZeroLimits.XITool.Interfaces;

namespace XI_Tools_Tests
{
    [TestClass]
    public class XIToolsTests
    {
        [TestInitialize]
        public void SetUp()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
        }

        [TestClass]
        public class UnitTests
        {
            [TestMethod]
            public void TestUnitFiltering()
            {
                var mock = new Mock<IUnit>();
                mock.SetupGet(x => x.IsClaimed).Returns(true);
                IUnit unit = (IUnit)mock.Object;
                Assert.AreEqual(true, unit.IsClaimed);
            }
        }

        [TestClass]
        public class AbilityTests
        {
            /// <summary>
            /// Tests whether the ability parser properly sets 
            /// spell distance. 
            /// </summary>
            [TestMethod]
            public void TestSpellDistance()
            {
                var service = new AbilityService();
                service.IsDistanceEnabled = true;
                Assert.AreEqual(Constants.SPELL_CAST_DISTANCE,
                    service.CreateAbility("Dia").Distance);
            }

            /// <summary>
            /// Tests whether the ability parse properly sets
            /// ranged distance.
            /// </summary>
            [TestMethod]
            public void TestRangedDistance()
            {
                var service = new AbilityService();
                service.IsDistanceEnabled = true;
                Assert.AreEqual(Constants.RANGED_ATTACK_MAX_DISTANCE,
                    service.CreateAbility("Ranged").Distance);
            }

            /// <summary>
            /// Tests whether the ability parse properly sets
            /// melee distance. 
            /// </summary>
            [TestMethod]
            public void TestMeleeDistance()
            {
                var service = new AbilityService();
                service.IsDistanceEnabled = true;
                Assert.AreEqual(Constants.MELEE_DISTANCE,
                    service.CreateAbility("Provoke").Distance);
            }

            [TestMethod]
            public void TestToString()
            {
                Ability test = new Ability();
                test.Prefix = "/magic";
                test.Name = "Cure";
                test.Targets = "Self";
                var cure = new AbilityService().CreateAbility("Cure");
                StringAssert.Equals(test.ToString(), cure.ToString());
            }
        }
    }
}