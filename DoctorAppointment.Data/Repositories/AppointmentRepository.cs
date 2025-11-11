using DoctorAppointment.Data.Configuration;
using DoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;
using Newtonsoft.Json;

namespace DoctorAppointment.Data.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public override string Path { get; set; }
        public override int LastId { get; set; }

        public AppointmentRepository()
        {
            AppSettings result = ReadFromAppSettings();

            Path = System.IO.Path.Combine(Constants.solutionPath, result.Database.Appointments.Path);
            LastId = result.Database.Appointments.LastId;
        }
        public override void ShowInfo(Appointment appointment)
        {
            Console.WriteLine(
                $"Appointment with Id {appointment.Id}, for the patient: {appointment.Patient.Name} {appointment.Patient.Surname}" +
                $"with Doctor: {appointment.Doctor.Name} {appointment.Doctor.Surname} - {appointment.Doctor.DoctorType}" +
                $", From: {appointment.DateTimeFrom}" +
                $", To: {appointment.DateTimeTo}" +
                $"{(string.IsNullOrWhiteSpace(appointment.Description) ? "" : $", Description: {appointment.Description}")}" +
                $", CreatedAt: {appointment.CreatedAt}, UpdatedAt: {appointment.UpdatedAt}"
   );
        }

        protected override void SaveLastId()
        {
            AppSettings result = ReadFromAppSettings();
            result.Database.Appointments.LastId = LastId;

            File.WriteAllText(Constants.AppSettingsPath, JsonConvert.SerializeObject(result, Formatting.Indented));
        }
    }
}