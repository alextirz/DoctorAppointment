using DoctorAppointment.Data.Configuration;
using DoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;

namespace DoctorAppointment.Data.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly ISerializationService serializationService;
        private readonly DoctorRepository doctorRepository;
        private readonly PatientRepository patientRepository;
        public override string Path { get; set; }
        public override int LastId { get; set; }

        public AppointmentRepository(string appSettings, ISerializationService serializationService) : base(appSettings, serializationService)
        {
            this.serializationService = serializationService;
            var result = ReadFromAppSettings();

            Path = System.IO.Path.Combine(AppConstants.solutionPath, result.Database.Appointments.Path);
            LastId = result.Database.Appointments.LastId;
            doctorRepository = new DoctorRepository(appSettings, serializationService);
            patientRepository = new PatientRepository(appSettings, serializationService);
        }

        public override void ShowInfo(Appointment appointment)
        {
            var doctor = doctorRepository.GetById(appointment.DoctorId);
            var patient = patientRepository.GetById(appointment.PatientId);

            Console.WriteLine(
                $"Appointment with Id {appointment.Id}, for the patient: {patient?.Name} {patient?.Surname}" +
                $"with Doctor: {doctor?.Name} {doctor?.Surname} - {doctor?.DoctorType}" +
                $", From: {appointment.DateTimeFrom}" +
                $", To: {appointment.DateTimeTo}" +
                $"{(string.IsNullOrWhiteSpace(appointment.Description) ? "" : $", Description: {appointment.Description}")}" +
                $", CreatedAt: {appointment.CreatedAt}, UpdatedAt: {appointment.UpdatedAt}");
        }

        protected override void SaveLastId()
        {
            Repository result = ReadFromAppSettings();
            result.Database.Appointments.LastId = LastId;

            serializationService.Serialize(AppSettings, result);
        }
    }
}