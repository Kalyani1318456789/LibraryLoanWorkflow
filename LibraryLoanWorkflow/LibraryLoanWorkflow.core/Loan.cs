namespace LibraryLoanWorkflow.Core;

public sealed record Loan(
    string BookId,
    string MemberId,
    DateOnly BorrowedOn);