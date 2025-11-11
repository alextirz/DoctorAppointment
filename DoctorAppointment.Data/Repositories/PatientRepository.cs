using DoctorAppointment.Data.Configuration;
using DoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;
using Newtonsoft.Json;

namespace DoctorAppointment.Data.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public override string Path { get; set; }
        public override int LastId { get; set; }

        public PatientRepository()
        {
            AppSettings result = ReadFromAppSettings();

            Path = System.IO.Path.Combine(Constants.solutionPath, result.Database.Patients.Path);
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
            AppSettings result = ReadFromAppSettings();
            result.Database.Patients.LastId = LastId;

            File.WriteAllText(Constants.AppSettingsPath, JsonConvert.SerializeObject(result, Formatting.Indented)); 
        }
    }
}
