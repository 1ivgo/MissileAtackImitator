using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace MissileAtackImitatorCoreNS
{
    [DataContract]
    public struct ImitationResponse
    {
        [DataMember]
        public List<PointF> AircraftTrajectory;

        [DataMember]
        public MissileInfo UsualMissile; //Serega 

        [DataMember]
        public MissileInfo FuzzyMissile; //Ibragim

        public struct MissileInfo
        {
            public List<PointF> Trajectory;
            public bool IsHit;
        }
    }
}
