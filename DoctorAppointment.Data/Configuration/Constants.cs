namespace MyDoctorAppointment.Data.Configuration
{
    public static class Constants
    {
        public static string solutionPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        public static readonly string AppSettingsPath = $"{solutionPath}\\DoctorAppointment.Data\\Configuration\\appsettings.json";
    }
}
