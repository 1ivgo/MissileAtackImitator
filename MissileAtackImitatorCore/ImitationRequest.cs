using System.Runtime.Serialization;

namespace MissileAtackImitatorCoreNS
{
    [DataContract]
    public class ImitationRequest
    {
        [DataMember(Order = 0)]
        public int StepsCount { get; set; }

        [DataMember(Order = 1)]
        public ScenePoints AircraftPoints { get; set; }

        public ImitationRequest(int stepsCount, ScenePoints aircraftPoints)
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
