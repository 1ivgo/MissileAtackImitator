using System.IO;
using System.Runtime.Serialization;

namespace MissileAtackImitatorCoreNS
{
    [DataContract]
    public class ImitationResponse
    {
        [DataMember(Order = 0)]
        public ScenePoints AircraftTrajectory { get; private set; }

        public ScenePoints GetResponse(string filename)
        {
            var response = JsonSaverLoader.Load<ImitationResponse>(filename);
            AircraftTrajectory = response.AircraftTrajectory;
            File.Delete(filename);
            return AircraftTrajectory;
        }
    }
}
