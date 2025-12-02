namespace DoctorAppointment.UI
{
    public static class InputExtensions
    {
        public static byte ReadValidNumber(this string prompt, int min = 0, int max = 50)
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
        public static string ReadValidString(this string prompt)
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

        public static T ReadValidEnum<T>(this string title) where T : struct, Enum
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
}
