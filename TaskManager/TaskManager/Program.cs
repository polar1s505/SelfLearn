using TaskManager.Interfaces;
using TaskManager.Models;
using TaskManager.Utils;

const string FILEPATH = "db.json";

string? userInput;
string menuSelection = "";

List<Assignment> assignments = await JsonFileManager.ReadFromJsonAsync(FILEPATH);
User user = new User(assignments);

do
{
    user.UpdateAssignmentsStatus();

    DisplayMainMenu();

    userInput = Console.ReadLine();
    if (userInput != null)
    {
        menuSelection = userInput.ToLower().Trim();
    }


    DisplayCommandInfo display = new DisplayCommandInfo();

    switch (menuSelection)
    {
        case "1":
        case "2":
        case "3":
        case "4":
            ICommand<bool>? commandBool = CommandFactory.GetCommand<bool>(menuSelection);
            display.ShowAssignmentResult(commandBool!.Execute(user));
            break;
        case "5":
        case "6":
        case "7":
        case "8":
        case "9":
        case "10":
            ICommand<List<Assignment>>? commandList = CommandFactory.GetCommand<List<Assignment>>(menuSelection);
            display.ShowAssignments(commandList!.Execute(user));
            break;
        default:
            break;
    }

} while (menuSelection != "exit");

await JsonFileManager.WriteToJsonAsync(user, FILEPATH);

void DisplayMainMenu()
{
    Console.Clear();

    Console.WriteLine("Welcome to the Runner!\nYour favorite task manager!\nChoose an option:");

    Console.WriteLine("1. Add a new task");
    Console.WriteLine("2. Modify a task");
    Console.WriteLine("3. Close a task");
    Console.WriteLine("4. Delete a task");
    Console.WriteLine();

    Console.WriteLine("5. Display all task on the screen");
    Console.WriteLine("6. Display task for a specific period");
    Console.WriteLine("7. Display all active tasks");
    Console.WriteLine("8. Display all expired tasks");
    Console.WriteLine("9. Display all completed tasks");
    Console.WriteLine();

    Console.WriteLine("10. Sort all task by due date");


    Console.WriteLine();
    Console.WriteLine("Enter your selection number (or type Exit to exit the program)");
}