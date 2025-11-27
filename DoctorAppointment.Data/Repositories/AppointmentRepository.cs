using DoctorAppointment.Data.Configuration;
using DoctorAppointment.Data.Interfaces;
using DoctorAppointment.Domain.Entities;

namespace DoctorAppointment.Data.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly ISerializationService serializationService;
        public override string Path { get; set; }
        public override int LastId { get; set; }

        public AppointmentRepository(string appSettings, ISerializationService serializationService) : base(appSettings, serializationService)
        {
            this.serializationService = serializationService;
            var result = ReadFromAppSettings();

            Path = System.IO.Path.Combine(AppConstants.solutionPath, result.Database.Appointments.Path);
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
            Repository result = ReadFromAppSettings();
            result.Database.Appointments.LastId = LastId;

            serializationService.Serialize(AppSettings, result);
        }
    }
}