using FFACETools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroLimits.XITool.Interfaces;

namespace ZeroLimits.XITool.Test
{
    public class TestUnit : IUnit
    {
        public int ID { get; set; }

        public int ClaimedID { get; set; }

        public double Distance { get; set; }

        public FFACETools.FFACE.Position Position { get; set; }

        public short HPPCurrent { get; set; }

        public bool IsActive { get; set; }

        public bool IsClaimed { get; set; }

        public bool IsRendered { get; set; }

        public string Name { get; set; }

        public byte NPCBit { get; set; }

        public NPCType NPCType { get; set; }

        public int PetID { get; set; }

        public float PosH { get; set; }

        public float PosX { get; set; }

        public float PosY { get; set; }

        public float PosZ { get; set; }

        public byte[] RawData { get; set; }

        public Status Status { get; set; }

        public short TPCurrent { get; set; }

        public bool MyClaim { get; set; }

        public bool HasAggroed { get; set; }

        public bool IsDead { get; set; }

        public bool PartyClaim { get; set; }

        public double YDifference { get; set; }
    }
}
