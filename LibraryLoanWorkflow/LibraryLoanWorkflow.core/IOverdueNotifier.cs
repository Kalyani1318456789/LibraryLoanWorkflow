namespace LibraryLoanWorkflow.Core;

public interface IOverdueNotifier
{
    // Notifies a member when their loan is overdue.
    void Notify(string memberId, DateOnly dueDate);
}
