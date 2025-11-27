using DoctorAppointment.Data.Configuration;
using DoctorAppointment.Data.Interfaces;
using DoctorAppointment.Domain.Entities;

namespace DoctorAppointment.Data.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        private readonly ISerializationService serializationService;
        public override string Path { get; set; }
        public override int LastId { get; set; }

        public PatientRepository(string appSettings, ISerializationService serializationService) : base(appSettings, serializationService)
        {
            this.serializationService = serializationService;
            var result = ReadFromAppSettings();

            Path = System.IO.Path.Combine(AppConstants.solutionPath, result.Database.Patients.Path);
            LastId = result.Database.Patients.LastId;
        }
        public override void ShowInfo(Patient patient)
        {
            Console.WriteLine(
                $"{patient.Id} {patient.Name} {patient.Surname}" +
                $", Illness: {patient.IllnessType}" +
                $"{(patient.Phone == null ? "" : ", Phone: " + patient.Phone)}" +
                $"{(patient.Email == null ? "" : ", Email: " + patient.Email)}" +
                $"{(string.IsNullOrWhiteSpace(patient.AdditionalInfo) ? "" : ", Info: " + patient.AdditionalInfo)}" +
                $"{(string.IsNullOrWhiteSpace(patient.Address) ? "" : ", Address: " + patient.Address)}"
            );
        }

        protected override void SaveLastId()
        {
            var result = ReadFromAppSettings();
            result.Database.Patients.LastId = LastId;

            serializationService.Serialize(AppSettings, result);        }
    }
}
