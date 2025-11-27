using DoctorAppointment.Data.Interfaces;
using Newtonsoft.Json;

namespace DoctorAppointment.Service.Services
{
    public class JsonDataSerializerService : ISerializationService
    {
        public T Deserialize<T>(string path)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "[]");
            }

            var json = File.ReadAllText(path);

            if (string.IsNullOrWhiteSpace(json))
            {
                File.WriteAllText(path, "[]");
                json = "[]";
            }

            return JsonConvert.DeserializeObject<T>(json)!;
        }

        public void Serialize<T>(string path, T data)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(data, Formatting.Indented));
        }
    }
}
