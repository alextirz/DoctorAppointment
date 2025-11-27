using DoctorAppointment.Data.Configuration;
using DoctorAppointment.Data.Interfaces;
using DoctorAppointment.Domain.Entities;

namespace DoctorAppointment.Data.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        private readonly ISerializationService serializationService;
        public override string Path { get; set; }

        public override int LastId { get; set; }

        public DoctorRepository(string appSettings, ISerializationService serializationService) : base(appSettings, serializationService)
        {
            this.serializationService = serializationService;
            var result = ReadFromAppSettings();

            Path =  System.IO.Path.Combine(AppConstants.solutionPath, result.Database.Doctors.Path);
            LastId = result.Database.Doctors.LastId;
        }

        public override void ShowInfo(Doctor doctor)
        {
            Console.WriteLine(
                $"{doctor.Id} " +
                $"{doctor.Name} {doctor.Surname}" +
                $", Type: {doctor.DoctorType}" +
                $"{(doctor.Experience > 0 ? $", Experience: {doctor.Experience} years" : "")}" +
                $"{(doctor.Phone == null ? "" : ", Phone: " + doctor.Phone)}" +
                $"{(doctor.Email == null ? "" : ", Email: " + doctor.Email)}" +
                $"{(doctor.Salary > 0 ? $", Salary: {doctor.Salary}" : "")}");
        }

        protected override void SaveLastId()
        {
            var result = ReadFromAppSettings();
            result.Database.Doctors.LastId = LastId;

            serializationService.Serialize(AppSettings, result);
        }
    }
}
