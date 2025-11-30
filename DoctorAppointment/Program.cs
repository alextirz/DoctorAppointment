using DoctorAppointment.Data.Interfaces;
using DoctorAppointment.Service.Services;
using DoctorAppointment.UI;
using Microsoft.VisualBasic;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Domain.Enums;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.Services;
using MyDoctorAppointment.Data.Configuration;

namespace MyDoctorAppointment
{
    public class DoctorAppointment
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService;

        public DoctorAppointment(string appSettings, ISerializationService serializationService)
        {
            _doctorService = new DoctorService(appSettings, serializationService);
            _patientService = new PatientService(appSettings, serializationService);
            _appointmentService = new AppointmentService(appSettings, serializationService);
        }

        public void Menu()
        {
            while (true)
            {
                Console.WriteLine("\n=== Doctor Appointment Menu ===");
                Console.WriteLine("1. Show all doctors");
                Console.WriteLine("2. Add new doctor");
                Console.WriteLine("3. Add new patient");
                Console.WriteLine("4. Add new appointment");
                Console.WriteLine("5. Exit");
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

                    case MenuOption.AddPatient:
                        AddPatient();
                        break;

                    case MenuOption.AddAppointment:
                        AddAppointment();
                        break;

                    case MenuOption.Exit:
                        Console.WriteLine("Exiting...");
                        return;
                }
            }
        }

        private void AddAppointment()
        {
            Console.WriteLine("Adding appointment.");
            Doctor doctor = _doctorService.GetAll().LastOrDefault();
            if (doctor == null) 
            {
                doctor = new Doctor
                {
                    Name = "Vasyl",
                    Surname = "Petrenko",
                    Experience = 25,
                    DoctorType = DoctorTypes.Dentist
                };
                _doctorService.Create(doctor);
            }

            Patient patient = _patientService.GetAll().LastOrDefault();
            if (patient == null)
            {
                patient = new Patient
                {
                    Name = "Oksana",
                    Surname = "B",
                    IllnessType = IllnessTypes.Ambulance,
                };
                _patientService.Create(patient);
            }

            var appointment = new Appointment
            {
                PatientId = patient.Id,
                DoctorId = doctor.Id,
                DateTimeFrom = DateTime.Now,
                DateTimeTo = DateTime.Now.AddMinutes(30),
            };

           _appointmentService.Create(appointment);
            Console.WriteLine($"Appointment added for {patient.Name} {patient.Surname} to {doctor.DoctorType} for {appointment.DateTimeFrom}.");
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
            var doctorType = ReadValidEnum<DoctorTypes>("doctor type");

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
        private void AddPatient()
        {
            Console.WriteLine("Adding patient.");

            string name = ReadValidString("Enter name: ");
            string surname = ReadValidString("Enter surname: ");
            var illnessType = ReadValidEnum<IllnessTypes>("illness type");
            string address = ReadValidString("Enter address: ");

            var patient = new Patient
            {
                Name = name,
                Surname = surname,
                IllnessType = illnessType,
                Address = address,
                Phone = "1234234132",
            };

            _patientService.Create(patient);
            Console.WriteLine($"Patient {name} {surname} added successfully!");
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

        private byte ReadValidNumber(string prompt, int min = 0, int max = 50)
        {
            byte experience;
            while (true)
            {
                Console.Write(prompt);
                if (byte.TryParse(Console.ReadLine(), out experience) && experience >= min && experience < max)
                    break;

                Console.WriteLine($"Invalid number. Please enter a value between {min} and {max}.");
            }

            return experience;
        }
        private T ReadValidEnum<T>(string title) where T : struct, Enum
        {
            Console.WriteLine($"\nChoose {title}:");

            foreach (var value in Enum.GetValues(typeof(T)))
            {
                Console.WriteLine($"{Convert.ToInt32(value)}. {value}");
            }

            while (true)
            {
                Console.Write("Enter number: ");
                if (int.TryParse(Console.ReadLine(), out int num) &&
                    Enum.IsDefined(typeof(T), num))
                {
                    return (T)Enum.ToObject(typeof(T), num);
                }

                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }

    public static class Program
    {
        public static void Main()
        {
            var dbType = AskDatabaseType();
            var doctorAppointment = CreateDoctorAppointment(dbType);

            doctorAppointment.Menu();
        }

        private static DatabaseType AskDatabaseType()
        {
            Console.WriteLine("Choose database type:");
            Console.WriteLine("1 - JSON");
            Console.WriteLine("2 - XML");

            while (true)
            {
                Console.Write("Your choice: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out int value) &&
                    Enum.IsDefined(typeof(DatabaseType), value))
                {
                    return (DatabaseType)value;
                }

                Console.WriteLine("Invalid option. Try again.");
            }
        }

        private static DoctorAppointment CreateDoctorAppointment(DatabaseType dbType)
        {
            switch (dbType)
            {
                case DatabaseType.Json:
                    return new DoctorAppointment(
                        AppConstants.JsonAppSettingsPath,
                        new JsonDataSerializerService()
                    );

                case DatabaseType.Xml:
                    return new DoctorAppointment(
                        AppConstants.XmlAppSettingsPath,
                        new XmlDataSerializerService()
                    );

                default:
                    throw new ArgumentOutOfRangeException(nameof(dbType));
            }
        }
    }


}