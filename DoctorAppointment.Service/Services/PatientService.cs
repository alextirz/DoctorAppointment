using DoctorAppointment.Data.Interfaces;
using DoctorAppointment.Data.Repositories;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Service.Interfaces;

namespace DoctorAppointment.Service.Services
{
    public class PatientService : IPatientService
    {
        private readonly PatientRepository _patientRepository;

        public PatientService(string appSettings, ISerializationService serializationService)
        {
            _patientRepository = new PatientRepository(appSettings, serializationService);
        }

        public Patient Create(Patient patient)
        {
            return _patientRepository.Create(patient);
        }

        public bool Delete(int id)
        {
            return _patientRepository.Delete(id);
        }

        public Patient Update(int id, Patient patient)
        {
            return _patientRepository.Update(id, patient);
        }

        Patient? IPatientService.Get(int id)
        {
            return _patientRepository.GetById(id);
        }

        IEnumerable<Patient> IPatientService.GetAll()
        {
            return _patientRepository.GetAll();
        }
    }
}
