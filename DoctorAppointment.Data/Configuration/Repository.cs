namespace DoctorAppointment.Data.Configuration
{

    public class Repository
    {
        public DatabaseSettings Database { get; set; }
    }

    public class DatabaseSettings
    {
        public DoctorsSettings Doctors { get; set; }
        public PatientsSettings Patients { get; set; }
        public AppointmentsSettings Appointments { get; set; }
    }

    public class DoctorsSettings
    {
        public int LastId { get; set; }
        public string Path { get; set; }
    }

    public class PatientsSettings
    {
        public int LastId { get; set; }
        public string Path { get; set; }
    }

    public class AppointmentsSettings
    {
        public int LastId { get; set; }
        public string Path { get; set; }
    }
}
