
using TaskManager.Commands;
using TaskManager.Interfaces;
using TaskManager.Models;
using TaskManager.Utils;

const string FILEPATH = "db.json";

string? userInput;
string menuSelection = "";

List<Assignment> assignments = JsonFileManager.ReadFromJson(FILEPATH);
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
            ICommand<bool> addAssignmentCommand = new AddAssignmentCommand();
            display.ShowAddAssignmentResult(addAssignmentCommand.Execute(user));
            break;
        case "2":
            ICommand<bool> modifyAssignmentCommand = new ModifyAssignmentCommand();
            display.ShowModifyAssignmentResult(modifyAssignmentCommand.Execute(user));
            break;
        case "3":
            ICommand<bool> closeAssignmentCommand = new CloseAssignmentCommand();
            display.ShowCloseAssignmentResult(closeAssignmentCommand.Execute(user));
            break;
        case "4":
            ICommand<bool> deleteAssignmentCommand = new DeleteAssignmentCommand();
            display.ShowDeleteAssignmentResult(deleteAssignmentCommand.Execute(user));
            break;
        case "5":
            ICommand<List<Assignment>> showAllAssignments = new ShowAllAssignmentsCommand();
            display.ShowAssignments(showAllAssignments.Execute(user));
            break;
        case "6":
            ICommand<List<Assignment>> showAssignmentsInRange = new ShowAssignmentsByPeriodCommand();
            display.ShowAssignments(showAssignmentsInRange.Execute(user));
            break;
        case "7":
            ICommand<List<Assignment>> showActiveAssignments = new ShowActiveAssignmentsCommand();
            display.ShowAssignments(showActiveAssignments.Execute(user));
            break;
        case "8":
            ICommand<List<Assignment>> showExpiredAssignments = new ShowExpiredAssignmentsCommand();
            display.ShowAssignments(showExpiredAssignments.Execute(user));
            break;
        case "9":
            ICommand<List<Assignment>> showCompletedAssignments = new ShowCompletedAssignmentsCommand();
            display.ShowAssignments(showCompletedAssignments.Execute(user));
            break;
        case "10":
            ICommand<List<Assignment>> showAssignmentsByDueDate = new ShowAssignmentsByDueDate();
            display.ShowAssignments(showAssignmentsByDueDate.Execute(user));
            break;
        default:
            break;
    }

} while (menuSelection != "exit");

JsonFileManager.WriteToJson(user, FILEPATH);

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