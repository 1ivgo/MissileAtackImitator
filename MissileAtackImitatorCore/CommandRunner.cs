namespace MissileAtackImitatorCoreNS
{
    using System.Diagnostics;

    public static class CommandRunner
    {
        public static string[] ExecuteCommand(string filename, string args)
        {
            var processInfoStart = new ProcessStartInfo()
            {
                FileName = filename,
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            string[] result = new string[2];
            using (var process = Process.Start(processInfoStart))
            {
                using (var sr = process.StandardOutput)
                {
                    result[0] = sr.ReadToEnd();
                }

                using (var sr = process.StandardError)
                {
                    result[1] = sr.ReadToEnd();
                }
            }

            return result;
        }

        private async static void ExecuteCommandAsync()
        {

        }
    }
}
