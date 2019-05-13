using System.IO;
using System.Runtime.Serialization;

namespace MissileAtackImitatorCoreNS
{
    [DataContract]
    public class ImitationResponse
    {
        [DataMember(Order = 0)]
        public ScenePoints AircraftTrajectory { get; private set; }

        [DataMember(Order = 1)]
        public ScenePoints MissileTrajectory { get; private set; }

        public void GetResponse(string filename)
        {
            var response = JsonSaverLoader.Load<ImitationResponse>(filename);
            AircraftTrajectory = response.AircraftTrajectory;
            MissileTrajectory = response.MissileTrajectory;
            File.Delete(filename);
        }
    }
}
