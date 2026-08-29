namespace LibraryLoanWorkflow.Core;
public sealed class DueDateCalculator
{
    public DateOnly Calculate(Loan loan)
        => loan.BorrowedOn.AddDays(30);
}