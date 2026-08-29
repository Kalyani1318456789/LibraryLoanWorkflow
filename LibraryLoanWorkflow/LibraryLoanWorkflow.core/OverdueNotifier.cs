namespace LibraryLoanWorkflow.Core;

public sealed class OverdueNotifier : IOverdueNotifier
{
    public void Notify(string memberId, DateOnly dueDate)
    {
        Console.WriteLine(
            $"Member {memberId} has an overdue loan. Due date: {dueDate}");
    }
}