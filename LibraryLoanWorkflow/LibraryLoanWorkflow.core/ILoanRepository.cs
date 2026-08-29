namespace LibraryLoanWorkflow.Core;

public interface ILoanRepository
{
    void Save(Loan loan, DateOnly dueDate);
}