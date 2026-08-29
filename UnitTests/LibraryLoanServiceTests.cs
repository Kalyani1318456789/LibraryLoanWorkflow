using LibraryLoanWorkflow.Core;

namespace UnitTests;

public class LibraryLoanServiceTests
{
    [Fact]
    public void ProcessLoan_CalculatesAndSavesDueDate()
    {
        // Arrange
        var loan = new Loan(
            "Book1",
            "Member1",
            new DateOnly(2026, 8, 1));

        var calculator = new DueDateCalculator();
        var repository = new TestLoanRepository();
        var notifier = new TestOverdueNotifier();

        var service = new LibraryLoanService(
            calculator,
            repository,
            notifier);

        // Act
        var dueDate = service.ProcessLoan(loan);

        // Assert
        Assert.Equal(new DateOnly(2026, 8, 31), dueDate);

        Assert.Equal(loan, repository.SavedLoan);
        Assert.Equal(new DateOnly(2026, 8, 31), repository.SavedDueDate);
    }

    private sealed class TestLoanRepository : ILoanRepository
    {
        public Loan? SavedLoan { get; private set; }
        public DateOnly SavedDueDate { get; private set; }

        public void Save(Loan loan, DateOnly dueDate)
        {
            SavedLoan = loan;
            SavedDueDate = dueDate;
        }
    }
    [Fact]
    public void CheckOverdue_NotifiesWhenLoanIsOverdue()
    {
        // Arrange
        var loan = new Loan(
            "Book1",
            "Member1",
            new DateOnly(2026, 8, 1));

        var calculator = new DueDateCalculator();
        var repository = new TestLoanRepository();
        var notifier = new TestOverdueNotifier();

        var service = new LibraryLoanService(
            calculator,
            repository,
            notifier);

        // Act
        service.CheckOverdue(
            loan,
            new DateOnly(2026, 9, 1));

        // Assert
        Assert.Equal("Member1", notifier.NotifiedMemberId);
        Assert.Equal(
            new DateOnly(2026, 8, 31),
            notifier.NotifiedDueDate);
    }

    [Fact]
    public void CheckOverdue_DoesNotNotifyWhenLoanIsNotOverdue()
    {
        // Arrange
        var loan = new Loan(
            "Book2",
            "Member2",
            new DateOnly(2026, 8, 1));

        var calculator = new DueDateCalculator();
        var repository = new TestLoanRepository();
        var notifier = new TestOverdueNotifier();

        var service = new LibraryLoanService(
            calculator,
            repository,
            notifier);

        // Act
        service.CheckOverdue(
            loan,
            new DateOnly(2026, 8, 31));

        // Assert
        Assert.Null(notifier.NotifiedMemberId);
    }
    private sealed class TestOverdueNotifier : IOverdueNotifier
    {
        public string? NotifiedMemberId { get; private set; }
        public DateOnly NotifiedDueDate { get; private set; }

        public void Notify(string memberId, DateOnly dueDate)
        {
            NotifiedMemberId = memberId;
            NotifiedDueDate = dueDate;
        }
    }
}