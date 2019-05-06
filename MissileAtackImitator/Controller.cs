using System.Diagnostics;
using MissileAtackImitatorCoreNS;

namespace MissileAtackImitator
{
    internal class Controller
    {
        private const string filePathToPython = @"python.exe";
        private const string filePathToScript = @"trajectorygenerator.py";

        internal ScenePoints GetTrajectory(ScenePoints userPoints)
        {
            string filePathRequest = "ImitationRequest.json";
            string filePathResponse = "ImitationResponse.json";

            new ImitationRequest(500, userPoints).DoRequest(filePathRequest);

            var processInfoStart = new ProcessStartInfo()
            {
                FileName = filePathToPython,
                Arguments = string.Format("{0} {1} {2}", filePathToScript, filePathRequest, filePathResponse),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using (var process = Process.Start(processInfoStart))
            {
                using (var sr = process.StandardOutput)
                {
                    string result = sr.ReadToEnd();
                }
            }

            return new ImitationResponse().GetResponse(filePathResponse);
        }
    }
}
