namespace LibraryLoanWorkflow.Core;

public sealed class LoanRepository : ILoanRepository
{
    private readonly List<(Loan Loan, DateOnly DueDate)> _loans = new();
    public void Save(Loan loan, DateOnly dueDate)
    {
        _loans.Add((loan, dueDate));
    }
}