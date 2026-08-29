namespace LibraryLoanWorkflow.Core;

public interface ILoanRepository
{
    // Saves the loan together with its calculated due date.
    void Save(Loan loan, DateOnly dueDate);
}
