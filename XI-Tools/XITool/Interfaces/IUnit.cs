using FFACETools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroLimits.XITool.Interfaces
{
    public interface IUnit
    {
        int ID { get; set; }

        int ClaimedID { get; }

        double Distance { get; }

        FFACE.Position Position { get; }

        short HPPCurrent { get; }

        bool IsActive { get; }

        bool IsClaimed { get; }

        bool IsRendered { get; }

        string Name { get; }

        byte NPCBit { get; }

        NPCType NPCType { get; }

        int PetID { get; }

        float PosH { get; }

        float PosX { get; }

        float PosY { get; }

        float PosZ { get; }

        byte[] RawData { get; }

        Status Status { get; }

        short TPCurrent { get; }

        bool MyClaim { get; }

        bool HasAggroed { get; }

        bool IsDead { get; }

        bool PartyClaim { get; }

        double YDifference { get; }

        string ToString();
    }
}
