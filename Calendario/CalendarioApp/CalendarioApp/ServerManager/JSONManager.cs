using Newtonsoft.Json;

namespace CalendarioApp.ServerManager
{
    public static class JSONManager
    {
        public static T Deserialize<T>(string json) => JsonConvert.DeserializeObject<T>(json);
        public static string Serialize(object obj) => JsonConvert.SerializeObject(obj);
    }
}
