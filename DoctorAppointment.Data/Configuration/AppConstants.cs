namespace MyDoctorAppointment.Data.Configuration
{
    public static class AppConstants
    {
        public static string solutionPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        public static readonly string JsonAppSettingsPath = $"{solutionPath}\\DoctorAppointment.Data\\Configuration\\appsettings.json";
        public static readonly string XmlAppSettingsPath = $"{solutionPath}\\DoctorAppointment.Data\\Configuration\\appsettings.xml";
    }
}
