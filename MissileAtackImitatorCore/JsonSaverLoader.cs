using System.IO;
using System.Runtime.Serialization.Json;

namespace MissileAtackImitatorCoreNS
{
    public static class JsonSaverLoader
    {
        public static void Save<T>(T obj, string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create))
                new DataContractJsonSerializer(typeof(T)).WriteObject(fs, obj);
        }

        public static T Load<T>(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open))
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(fs);
        }
    }
}
