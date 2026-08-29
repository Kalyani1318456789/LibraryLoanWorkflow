# A19 | Library Loan Workflow

A small library loan workflow implemented in C# to demonstrate separation of responsibilities and the Single Responsibility Principle.

## Project Overview

This project models the basic workflow of borrowing a book from a library.

The system handles:

- Representing a library loan.
- Calculating the due date.
- Saving loan information.
- Checking whether a loan is overdue.
- Notifying a member when a loan becomes overdue.
- Coordinating these operations through a service.

Each loan contains a Book ID, Member ID and the date on which the book was borrowed.

The loan period used in this implementation is **30 days**.

---

## Minimum Requirements

The implementation provides:

- A `Loan` model for representing loan information.
- A due-date calculation component.
- A repository abstraction for storing loans.
- An overdue notification abstraction.
- A service that coordinates the complete loan workflow.
- Unit tests for the implemented behaviour.
- A class library implementation with no separate console application.
- Command-line build and test support.

---

## Design Overview

The project separates the different responsibilities into individual classes and interfaces.

### Loan

`Loan` represents a library loan.

It contains:

- `BookId`
- `MemberId`
- `BorrowedOn`

### DueDateCalculator

`DueDateCalculator` is responsible for calculating when a loan is due.

The current rule is:

```text
Due Date = Borrowed Date + 30 days
