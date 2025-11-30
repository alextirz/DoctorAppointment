namespace MyDoctorAppointment.Domain.Entities
{
    public class Appointment : Auditable
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public DateTime DateTimeFrom { get; set; }

        public DateTime DateTimeTo { get; set; }

        public string? Description { get; set; }
    }
}
