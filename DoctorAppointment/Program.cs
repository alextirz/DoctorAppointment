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
            Console.WriteLine("Adding doctor.");

            string name = ReadValidString("Enter name: ");
            string surname = ReadValidString("Enter surname: ");
            byte experience = ReadValidNumber("Enter experience (0–50 years): ");
            var doctorType = ReadValidDoctorType();

            var newDoctor = new Doctor
            {
                Name = name,
                Surname = surname,
                Experience = experience,
                DoctorType = doctorType
            };

            _doctorService.Create(newDoctor);
            Console.WriteLine($"Doctor {name} {surname} added successfully!");
        }

        private string ReadValidString(string prompt)
        {
            string input;
            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Value cannot be empty. Try again.");
                    continue;
                }

                if (input.Length < 2)
                {
                    Console.WriteLine("Value must be at least 2 characters long.");
                    continue;
                }

                break;
            }

            return input;
        }

        private byte ReadValidNumber(string prompt)
        {
            byte experience;
            while (true)
            {
                Console.Write(prompt);
                if (byte.TryParse(Console.ReadLine(), out experience) && experience <= 0)
                    break;

                Console.WriteLine("Invalid number. Please enter a value between 0 and 50.");
            }

            return experience;
        }

        private Domain.Enums.DoctorTypes ReadValidDoctorType()
        {
            Console.WriteLine("Choose doctor type:");
            foreach (var type in Enum.GetValues(typeof(Domain.Enums.DoctorTypes)))
            {
                Console.WriteLine($"{(int)type}. {type}");
            }

            while (true)
            {
                Console.Write("Enter number: ");
                if (int.TryParse(Console.ReadLine(), out int num) &&
                    Enum.IsDefined(typeof(Domain.Enums.DoctorTypes), num))
                {
                    return (Domain.Enums.DoctorTypes)num;
                }

                Console.WriteLine("Invalid choice. Please enter a valid doctor type number.");
            }
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