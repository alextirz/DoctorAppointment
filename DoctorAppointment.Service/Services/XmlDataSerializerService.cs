using DoctorAppointment.Data.Interfaces;
using System.Xml.Serialization;

namespace DoctorAppointment.Service.Services
{
    public class XmlDataSerializerService : ISerializationService
    {
        public T Deserialize<T>(string path)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new FileStream(path, FileMode.OpenOrCreate))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        public void Serialize<T>(string path, T data)
        {
            var serializer = new XmlSerializer(typeof(T));
          //  using (var writer = new StreamWriter(path))
            using (var writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                serializer.Serialize(writer, data);
            }
        }
    }
}
