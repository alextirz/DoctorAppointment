namespace MyDoctorAppointment.Data.Configuration
{
    public static class Constants
    {
        // заменить на путь валидный для вашей директории на пк (в будущем будем использовать относительный путь)
  
        public static string solutionPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        public static readonly string AppSettingsPath = $"{solutionPath}\\DoctorAppointment.Data\\Configuration\\appsettings.json";
    }
}
