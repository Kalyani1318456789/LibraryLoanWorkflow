using LibraryLoanWorkflow.Core;

namespace UnitTests;

public class DueDateCalculatorTests
{
    [Fact]
    public void Calculate_ReturnsDate30DaysAfterBorrowedOn()
    {
        // Arrange
        var loan = new Loan(
            "Book1",
            "Member1",
            new DateOnly(2026, 8, 1));

        var calculator = new DueDateCalculator();

        // Act
        var actualDueDate = calculator.Calculate(loan);

        // Assert
        Assert.Equal(
            new DateOnly(2026, 8, 31),
            actualDueDate);
    }
    [Fact]
    public void Calculate_WorksAcrossMonths()
    {
        // Arrange
        var loan = new Loan(
            "Book2",
            "Member2",
            new DateOnly(2026, 8, 20));

        var calculator = new DueDateCalculator();

        // Act
        var actualDueDate = calculator.Calculate(loan);

        // Assert
        Assert.Equal(
            new DateOnly(2026, 9, 19),
            actualDueDate);
    }

    [Fact]
    public void Calculate_WorksAcrossYears()
    {
        // Arrange
        var loan = new Loan(
            "Book3",
            "Member3",
            new DateOnly(2026, 12, 15));

        var calculator = new DueDateCalculator();

        // Act
        var actualDueDate = calculator.Calculate(loan);

        // Assert
        Assert.Equal(
            new DateOnly(2027, 1, 14),
            actualDueDate);
    }
}