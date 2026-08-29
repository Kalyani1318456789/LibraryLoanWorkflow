namespace LibraryLoanWorkflow.Core;

public interface IOverdueNotifier
{
    void Notify(string memberId, DateOnly dueDate);
}