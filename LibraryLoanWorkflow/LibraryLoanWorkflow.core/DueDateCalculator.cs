namespace LibraryLoanWorkflow.Core;
public sealed class DueDateCalculator
{
    // A library loan is due 30 days after the borrowed date.
    public DateOnly Calculate(Loan loan)
        => loan.BorrowedOn.AddDays(30);
}
