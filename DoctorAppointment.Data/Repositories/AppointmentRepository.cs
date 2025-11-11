using DoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;

namespace DoctorAppointment.Data.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public override string Path { get; set; }
        public override int LastId { get; set; }

        public AppointmentRepository()
        {
            dynamic result = ReadFromAppSettings();

            Path = System.IO.Path.Combine(Constants.solutionPath, result.Database.Appointments.Path.Value);
            LastId = result.Database.Appointments.LastId;
        }
        public override void ShowInfo(Appointment appointment)
        {
            Console.WriteLine($"Appointment Id: {appointment.Id}, for patient {appointment.Patient.Name} {appointment.Patient.Surname} \nto Doctor {appointment.Doctor.Name} {appointment.Doctor.Surname} - {appointment.Doctor.DoctorType} \nfrom {appointment.DateTimeFrom} to {appointment.DateTimeFrom}. Description: {appointment.Description}");
        }

        protected override void SaveLastId()
        {
            dynamic result = ReadFromAppSettings();
            result.Database.Appointments.LastId = LastId;

            File.WriteAllText(Constants.AppSettingsPath, result.ToString());
        }
    }
}
