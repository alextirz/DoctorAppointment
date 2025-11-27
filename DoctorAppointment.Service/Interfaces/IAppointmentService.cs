using DoctorAppointment.Domain.Entities;

namespace DoctorAppointment.Service.Interfaces
{
    public interface IAppointmentService
    {
        Appointment Create(Appointment appointment);

        IEnumerable<Appointment> GetAll();

        Appointment? Get(int id);

        bool Delete(int id);

        Appointment Update(int id, Appointment appointment);
    }
}
