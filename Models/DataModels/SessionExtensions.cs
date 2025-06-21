using Newtonsoft.Json;

namespace MeetUpAR.Models.DataModels
{
    public static class SessionExtensions
    {
        public static void SetSessionObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T? GetSessionObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }

        public static void DeleteSessionObjectByKey(this ISession session, string key)
        {
            if (session.Keys.Contains(key))
            {
                session.Remove(key);
            }
        }
    }
}
