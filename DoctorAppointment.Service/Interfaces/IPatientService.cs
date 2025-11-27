using DoctorAppointment.Domain.Entities;

namespace DoctorAppointment.Service.Interfaces
{
    public interface IPatientService
    {
        Patient Create(Patient patient);

        IEnumerable<Patient> GetAll();

        Patient? Get(int id);

        bool Delete(int id);

        Patient Update(int id, Patient patient);
    }
}
