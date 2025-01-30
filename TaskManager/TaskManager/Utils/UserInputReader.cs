using System.Globalization;
using TaskManager.Models;

namespace TaskManager.Utils
{
    public class UserInputReader
    {
        public string GetTitle(Guid id)
        {
            const string displayMessage = "Enter a title:";
            string title = GetInput(displayMessage);

            if(title == null || title.Trim() == "")
            {
                title = $"Assignment{id}";
            }

            return title;
        }

        public string GetTitle()
        {
            const string displayMessage = "Enter new title (leave empty to keep the same):";
            string title = GetInput(displayMessage);

            return title;
        }

        public string GetDescription()
        {
            const string displayMessage = "Enter a description:";
            string description = GetInput(displayMessage);

            return description;
        }

        public string GetDescription(string displayMessage)
        {
            string description = GetInput(displayMessage);

            return description;
        }

        public DateTime GetDueDate()
        {
            DateTime date = DateTime.UtcNow.AddDays(1);
            bool isValid = false;

            while (!isValid)
            {
                Console.Write("Please, enter a due date (dd/MM/yyyy): ");
                string? userInput = Console.ReadLine();

                isValid = DateTime.TryParseExact(userInput, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

                if (!isValid)
                    Console.WriteLine("Invalid date format. Try again.");
            }

            return date;
        }

        public Guid GetAssignmentId(List<Assignment> assignments, string displayMessage)
        {
            int choice = GetAssignmentChoice(assignments, displayMessage);

            return assignments[choice - 1].Id;
        }

        private string GetInput(string displayMessage)
        {
            string? userInput;
            bool validEntry = false;

            do
            {
                Console.WriteLine(displayMessage);

                userInput = Console.ReadLine();
                if (userInput != null)
                {
                    userInput = userInput.Trim();
                    validEntry = true;
                }
            } while (!validEntry);

            return userInput;
        }

        private int GetAssignmentChoice(List<Assignment> assignments, string displayMessage)
        {
            bool validEntry = false;
            int choice;

            Console.WriteLine(displayMessage);

            for (int i = 0; i < assignments.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {assignments[i].Title}");
            }

            do
            {
                Console.WriteLine(displayMessage);

                if (int.TryParse(Console.ReadLine(), out choice) && (choice >= 1 && choice <= assignments.Count))
                {
                    validEntry = true;
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }  
            }
            while (!validEntry);

            return choice;
        }
    }
}
