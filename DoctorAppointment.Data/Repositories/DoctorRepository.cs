using DoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Domain.Entities;
using Newtonsoft.Json;

namespace MyDoctorAppointment.Data.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public override string Path { get; set; }

        public override int LastId { get; set; }

        public DoctorRepository()
        {
            AppSettings result = ReadFromAppSettings();

            Path = System.IO.Path.Combine(Constants.solutionPath, result.Database.Doctors.Path);
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
            AppSettings result = ReadFromAppSettings();
            result.Database.Doctors.LastId = LastId;

            File.WriteAllText(Constants.AppSettingsPath, JsonConvert.SerializeObject(result, Formatting.Indented));
        }
    }
}
