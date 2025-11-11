using DoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;

namespace DoctorAppointment.Data.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public override string Path { get; set; }
        public override int LastId { get; set; }

        public PatientRepository()
        {
            dynamic result = ReadFromAppSettings();

            Path = System.IO.Path.Combine(Constants.solutionPath, result.Database.Patients.Path.Value);
            LastId = result.Database.Patients.LastId;
        }
        public override void ShowInfo(Patient patient)
        {
            Console.WriteLine($"{patient.Id}. {patient.Name} {patient.Surname} - {patient.Email}, {patient.AdditionalInfo}.");
        }

        protected override void SaveLastId()
        {
            dynamic result = ReadFromAppSettings();
            result.Database.Patients.LastId = LastId;

            File.WriteAllText(Constants.AppSettingsPath, result.ToString());
        }
    }
}
