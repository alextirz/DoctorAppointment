using DoctorAppointment.Data.Interfaces;
using DoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;

namespace MyDoctorAppointment.Service.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly AppointmentRepository _appointmentRepository;

        public AppointmentService(string appSettings, ISerializationService serializationService)
        {
            _appointmentRepository = new AppointmentRepository(appSettings, serializationService);
        }

        public Appointment Create(Appointment appointment)
        {
            return _appointmentRepository.Create(appointment);
        }

        public bool Delete(int id)
        {
            return _appointmentRepository.Delete(id);
        }

        public Appointment Update(int id, Appointment appointment)
        {
            return _appointmentRepository.Update(id, appointment);
        }

        Appointment? IAppointmentService.Get(int id)
        {
            return _appointmentRepository.GetById(id);
        }

        IEnumerable<Appointment> IAppointmentService.GetAll()
        {
            return _appointmentRepository.GetAll();
        }
    }
}
