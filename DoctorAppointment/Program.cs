using DoctorAppointment.UI;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.Services;

namespace MyDoctorAppointment
{
    public class DoctorAppointment
    {
        private readonly IDoctorService _doctorService;

        public DoctorAppointment()
        {
            _doctorService = new DoctorService();
        }

        public void Menu()
        {
            while (true)
            {
                // add Enum for menu items and describe menu
                Console.WriteLine("\n=== Doctor Appointment Menu ===");
                Console.WriteLine("1. Show all doctors");
                Console.WriteLine("2. Add new doctor");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out int choice) || !Enum.IsDefined(typeof(MenuOption), choice))
                {
                    Console.WriteLine("Invalid choice. Try again.");
                    continue;
                }

                switch ((MenuOption)choice)
                {
                    case MenuOption.ShowAllDoctors:
                        ShowAllDoctors();
                        break;

                    case MenuOption.AddDoctor:
                        AddDoctor();
                        break;

                    case MenuOption.Exit:
                        Console.WriteLine("Exiting...");
                        return;
                }
            }
        }

        private void ShowAllDoctors()
        {
            var doctors = _doctorService.GetAll();
  
            Console.WriteLine("Current doctors list: ");
            var docs = _doctorService.GetAll();

            if (!doctors.Any())
            {
                Console.WriteLine("No doctors found.");
                return;
            }

            foreach (var doc in docs)
            {
                Console.WriteLine($"{doc.Id}. {doc.Name} {doc.Surname} - {doc.DoctorType}, {doc.Experience} years exp.");
            }
        }

        private void AddDoctor()
        {
            Console.WriteLine("Adding doctor: ");

            var newDoctor = new Doctor
            {
                Name = "Vasyl",
                Surname = "Petrenko",
                Experience = 10,
                DoctorType = Domain.Enums.DoctorTypes.Dentist
            };

            _doctorService.Create(newDoctor);
            Console.WriteLine($"Doctor {newDoctor.Name} {newDoctor.Surname} added successfully!");

        }

    }

    public static class Program
    {
        public static void Main()
        {
            var doctorAppointment = new DoctorAppointment();
            doctorAppointment.Menu();
        }
    }
}