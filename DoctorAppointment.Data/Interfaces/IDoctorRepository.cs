using DoctorAppointment.Domain.Entities;


namespace DoctorAppointment.Data.Interfaces
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        // you can add more specific doctor's methods
    }
}
