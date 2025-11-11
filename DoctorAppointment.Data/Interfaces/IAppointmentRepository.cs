using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Domain.Entities;

namespace DoctorAppointment.Data.Interfaces
{
    internal interface IAppointmentRepository : IGenericRepository<Appointment>
    {
    }
}
