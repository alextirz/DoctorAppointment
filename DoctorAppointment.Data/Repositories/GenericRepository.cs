using DoctorAppointment.Data.Configuration;
using DoctorAppointment.Data.Interfaces;
using DoctorAppointment.Domain.Entities;

namespace DoctorAppointment.Data.Repositories
{
    public abstract class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : Auditable
    {
        public string AppSettings { get; private set; }
        public ISerializationService SerializationService { get; private set; }
        public abstract string Path { get; set; }

        public abstract int LastId { get; set; }

        public GenericRepository(string appSettings, ISerializationService serializationService)
        {
            AppSettings = appSettings;
            SerializationService = serializationService;
        }

        public TSource Create(TSource source)
        {
            source.Id = ++LastId;
            source.CreatedAt = DateTime.Now;

            var objects = GetAll().Append(source).ToList();
            SerializationService.Serialize(Path, objects);

            SaveLastId();

            return source;
        }

        public bool Delete(int id)
        {
            if (GetById(id) is null)
                return false;

            SerializationService.Serialize(Path, GetAll().Where(x => x.Id != id));

            return true;
        }

        public IEnumerable<TSource> GetAll()
        {
            return SerializationService.Deserialize<List<TSource>>(Path);
        }

        public TSource? GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public TSource Update(int id, TSource source)
        {
            source.UpdatedAt = DateTime.Now;
            source.Id = id;

            SerializationService.Serialize(Path, GetAll().Select(x => x.Id == id ? source : x));
            return source;
        }

        public abstract void ShowInfo(TSource source);

        protected abstract void SaveLastId();

        protected Repository ReadFromAppSettings()
        {
            return SerializationService.Deserialize<Repository>(AppSettings);
        }
    }
}
