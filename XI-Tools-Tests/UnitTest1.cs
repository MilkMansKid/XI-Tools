using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZeroLimits.XITools;
using Moq;
using System.Diagnostics;
using ZeroLimits.XITools.Interfaces;
using ZeroLimits.XITools.Classes;

namespace XI_Tools_Tests
{
    [TestClass]
    public class UnitTest1
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
        }
    }
}
