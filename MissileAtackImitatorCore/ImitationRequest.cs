﻿namespace MissileAtackImitatorCoreNS
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.Serialization;

    [DataContract]
    public struct ImitationRequest
    {
        [DataMember]
        public int StepsCount;

        [DataMember]
        public List<Point> AircraftPoints;

        [DataMember]
        public MissileInfo Missiles;

        public struct MissileInfo
        {
            public Point LaunchPoint;
            public Point Direction;
            public double VelocityModule;
            public double PropCoeff;
            public string Inference; //Max-Min, Max-Prod
            public string Defuzzification; //Right-Max, Centroid
        }
    }
}
