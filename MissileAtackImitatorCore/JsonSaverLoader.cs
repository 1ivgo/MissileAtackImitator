namespace MissileAtackImitatorCoreNS
{
    using System.IO;
    using System.Runtime.Serialization.Json;

    public static class JsonSaverLoader
    {
        public static void Save<T>(T obj, string filename)
        {
            if (obj == null)
            {
                throw new System.ArgumentNullException(nameof(obj));
            }

            if (string.IsNullOrEmpty(filename))
            {
                throw new System.ArgumentNullException(nameof(filename));
            }

            using (var fs = new FileStream(filename, FileMode.Create))
            {
                new DataContractJsonSerializer(typeof(T)).WriteObject(fs, obj);
            }
        }

        public static T Load<T>(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new System.ArgumentNullException(nameof(filename));
            }

            using (var fs = new FileStream(filename, FileMode.Open))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(fs);
            }
        }
    }
}
