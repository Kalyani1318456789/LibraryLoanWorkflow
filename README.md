# Library Loan Workflow

## 1. Problem Specification

This project implements a small library loan workflow using the
Single Responsibility Principle (SRP).

The system is responsible for:

- Representing a library loan.
- Calculating the due date for a loan.
- Persisting loan information.
- Notifying a member when a loan is overdue.
- Coordinating these responsibilities through a small service.

A loan contains:

- Book ID
- Member ID
- Borrowed date

The library rule used in this implementation is that a loan is due
30 days after the borrowing date.

A loan is considered overdue when the current date is later than
the calculated due date.

There is no console application. The unit-test project acts as the
executable/test client for the class library.

---

## 2. Architecture

The project is divided into two projects:

### LibraryLoanWorkflow.Core

This is the class library containing the production implementation.

It contains:

- `Loan`
- `DueDateCalculator`
- `ILoanRepository`
- `LoanRepository`
- `IOverdueNotifier`
- `OverdueNotifier`
- `LibraryLoanService`

### UnitTests

This project contains automated tests for the class library.

It uses xUnit to test the behaviour of the production classes.

---

## 3. Design

The design follows the Single Responsibility Principle.

Each class has one primary responsibility.

### Loan

Represents the data associated with a library loan.

It contains:

- Book ID
- Member ID
- Borrowed date

### DueDateCalculator

Responsible only for calculating the due date.

The current rule is:

`Due Date = Borrowed Date + 30 days`

The calculation is isolated so that changes to the library's due-date
rules do not require changes to the loan service.

### ILoanRepository / LoanRepository

`ILoanRepository` defines the persistence boundary.

`LoanRepository` stores loans and their calculated due dates in memory.

The service does not directly manage the storage collection.

### IOverdueNotifier / OverdueNotifier

`IOverdueNotifier` defines the notification boundary.

`OverdueNotifier` is responsible for notifying a member when a loan
has become overdue.

The service therefore does not need to know how notifications are
actually delivered.

### LibraryLoanService

The service coordinates the other responsibilities.

For a new loan it:

1. Requests the due date from `DueDateCalculator`.
2. Saves the loan and due date through `ILoanRepository`.
3. Returns the calculated due date.

For overdue checking it:

1. Calculates the due date.
2. Compares it with the supplied current date.
3. Notifies the member through `IOverdueNotifier` if the loan is overdue.

---

## 4. Design Sketch

```text
                    +----------------------+
                    |         Loan         |
                    |----------------------|
                    | BookId               |
                    | MemberId             |
                    | BorrowedOn           |
                    +----------+-----------+
                               |
                               |
                               v
                    +----------------------+
                    | LibraryLoanService    |
                    |----------------------|
                    | ProcessLoan()         |
                    | CheckOverdue()        |
                    +----+-------------+----+
                         |             |
              calculates|             |saves
                         |             |
                         v             v
              +----------------+   +------------------+
              |DueDateCalculator|   |ILoanRepository  |
              +----------------+   +--------+---------+
                                             |
                                             v
                                     +---------------+
                                     |LoanRepository |
                                     +---------------+

                    CheckOverdue()
                          |
                          v
                 +-------------------+
                 | IOverdueNotifier  |
                 +---------+---------+
                           |
                           v
                 +-------------------+
                 |  OverdueNotifier  |
                 +-------------------+