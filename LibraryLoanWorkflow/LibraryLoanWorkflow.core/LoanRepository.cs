namespace LibraryLoanWorkflow.Core;

public sealed class LoanRepository : ILoanRepository
{
    private readonly List<(Loan Loan, DateOnly DueDate)> _loans = new();

    // Stores loans in memory for this implementation.
    public void Save(Loan loan, DateOnly dueDate)
    {
        _loans.Add((loan, dueDate));
    }
}
