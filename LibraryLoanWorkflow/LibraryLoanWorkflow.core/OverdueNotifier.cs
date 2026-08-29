namespace LibraryLoanWorkflow.Core;

public sealed class OverdueNotifier : IOverdueNotifier
{
    // Handles notification for an overdue loan.
    public void Notify(string memberId, DateOnly dueDate)
    {
        Console.WriteLine($"Member {memberId} has an overdue loan. Due date: {dueDate}");
    }
}
