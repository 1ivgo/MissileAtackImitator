using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace MissileAtackImitatorCoreNS
{
    [DataContract]
    public class ImitationRequest
    {
        [DataMember(Order = 0)]
        public int StepsCount { get; set; }

        [DataMember(Order = 1)]
        public List<Point> AircraftPoints { get; set; }

        public ImitationRequest(int stepsCount, List<Point> aircraftPoints)
        {
            StepsCount = stepsCount;
            AircraftPoints = aircraftPoints;
        }

        public void DoRequest(string filePath)
        {
            JsonSaverLoader.Save(this, filePath);
        }
    }
}
