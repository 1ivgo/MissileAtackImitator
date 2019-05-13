using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace MissileAtackImitatorCoreNS
{
    [DataContract]
    public struct ImitationRequest
    {
        [DataMember]
        public int StepsCount;

        [DataMember]
        public List<Point> AircraftPoints;

        [DataMember]
        public MissileInfo Missile;

        public struct MissileInfo
        {
            public Point LaunchPoint;
            public Point Direction;
            public double VelocityModule;
        }
    }
}
