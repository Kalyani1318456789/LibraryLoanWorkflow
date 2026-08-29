namespace LibraryLoanWorkflow.Core;
public sealed class LibraryLoanService
{
    private readonly DueDateCalculator _dueDateCalculator;
    private readonly ILoanRepository _loanRepository;
    private readonly IOverdueNotifier _overdueNotifier;
    public LibraryLoanService(
        DueDateCalculator dueDateCalculator,
        ILoanRepository loanRepository,
        IOverdueNotifier overdueNotifier)
    {
        _dueDateCalculator = dueDateCalculator;
        _loanRepository = loanRepository;
        _overdueNotifier = overdueNotifier;
    }
    public DateOnly ProcessLoan(Loan loan)
    {
        // Calculate the due date using the dedicated calculator.
        var dueDate = _dueDateCalculator.Calculate(loan);

        // Save the loan and its calculated due date.
        _loanRepository.Save(loan, dueDate);

        return dueDate;
    }
    public void CheckOverdue(Loan loan, DateOnly currentDate)
    {
        var dueDate = _dueDateCalculator.Calculate(loan);

        if (currentDate > dueDate)
        {
            // Notify the member only when the current date is past the due date.
            _overdueNotifier.Notify(loan.MemberId, dueDate);
        }
    }
}
