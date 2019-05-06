using System.Runtime.Serialization;

namespace MissileAtackImitatorCoreNS
{
    [DataContract]
    public class ImitationResponse
    {
        [DataMember(Order = 0)]
        public ScenePoints AircraftTrajectory { get; private set; }

        public ScenePoints GetResponse(string filePath)
        {
            var response = JsonSaverLoader.Load<ImitationResponse>(filePath);
            AircraftTrajectory = response.AircraftTrajectory;
            return AircraftTrajectory;
        }
    }
}
