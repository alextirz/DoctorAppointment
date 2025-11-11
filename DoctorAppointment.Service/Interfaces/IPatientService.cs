using MyDoctorAppointment.Domain.Entities;

namespace MyDoctorAppointment.Service.Interfaces
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
