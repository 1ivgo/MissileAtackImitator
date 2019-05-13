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
        public List<PointF> MissileTrajectory;
    }
}
