# A19 | SOLID - Single Responsibility Principle | Library Loan Workflow

Separate due-date calculation, loan persistence, and overdue notification into separate responsibilities in a small library loan workflow.

## Problem Statement

This project implements a small library loan workflow to demonstrate the **Single Responsibility Principle (SRP)**.

The system is responsible for:

- Representing a library loan.
- Calculating the due date for a loan.
- Saving loan information.
- Checking whether a loan is overdue.
- Notifying a member when a loan is overdue.
- Coordinating these responsibilities through a small service.

A loan contains:

- `BookId`
- `MemberId`
- `BorrowedOn`

The loan period used in this implementation is **30 days**.

The main goal is to keep each responsibility separate so that a change to one part of the system does not require unrelated classes to change.

---

## Design Overview

The design separates the main responsibilities into individual classes and interfaces.

### `Loan`

`Loan` represents the data for a library loan.

It contains:

```text
BookId
MemberId
BorrowedOn
````

The `Loan` class only represents the loan data. It does not calculate due dates, save loans, or send notifications.

### `DueDateCalculator`

`DueDateCalculator` is responsible only for calculating the due date.

The current rule is:

```text
Due Date = Borrowed Date + 30 days
```

Keeping this rule in its own class makes it possible to change the loan period without changing the repository or notification code.

### `ILoanRepository`

`ILoanRepository` defines the operation required to save a loan.

The service depends on this abstraction rather than directly depending on a particular storage implementation.

### `LoanRepository`

`LoanRepository` is the current in-memory implementation of `ILoanRepository`.

It stores the loan together with its calculated due date.

### `IOverdueNotifier`

`IOverdueNotifier` defines the operation used to notify a member when their loan is overdue.

### `OverdueNotifier`

`OverdueNotifier` provides the current implementation of the overdue notification behaviour.

### `LibraryLoanService`

`LibraryLoanService` coordinates the different responsibilities.

When a loan is processed:

1. The service asks `DueDateCalculator` to calculate the due date.
2. The loan and due date are passed to `ILoanRepository`.
3. The calculated due date is returned.

When checking for an overdue loan:

1. The due date is calculated.
2. The current date is compared with the due date.
3. If the current date is after the due date, `IOverdueNotifier` is used to notify the member.

---

## Class Diagram

```text
                         ┌────────────────────┐
                         │        Loan        │
                         ├────────────────────┤
                         │ BookId             │
                         │ MemberId           │
                         │ BorrowedOn         │
                         └─────────┬──────────┘
                                   │
                                   │
                                   ▼
                    ┌──────────────────────────┐
                    │    LibraryLoanService    │
                    ├──────────────────────────┤
                    │ ProcessLoan()            │
                    │ CheckOverdue()           │
                    └───────┬──────────┬───────┘
                            │          │
                            │          │
                calculates │          │ saves
                            ▼          ▼
                 ┌────────────────┐  ┌──────────────────┐
                 │DueDateCalculator│  │ILoanRepository  │
                 └────────────────┘  └────────┬─────────┘
                                               │
                                               ▼
                                      ┌──────────────────┐
                                      │ LoanRepository   │
                                      └──────────────────┘

                    CheckOverdue()
                           │
                           ▼
                 ┌────────────────────┐
                 │ IOverdueNotifier   │
                 └─────────┬──────────┘
                           │
                           ▼
                  ┌──────────────────┐
                  │ OverdueNotifier  │
                  └──────────────────┘
```

---

## Single Responsibility Principle

The project demonstrates SRP by giving each component one main responsibility.

| Component            | Responsibility               |
| -------------------- | ---------------------------- |
| `Loan`               | Represents loan information  |
| `DueDateCalculator`  | Calculates the due date      |
| `LoanRepository`     | Stores loan information      |
| `OverdueNotifier`    | Handles overdue notification |
| `LibraryLoanService` | Coordinates the workflow     |

For example, the `LibraryLoanService` does not contain the actual due-date calculation logic or persistence logic. It delegates these operations to the appropriate components.

This keeps the classes smaller and makes the system easier to understand, test, and modify.

---

## Critical Analysis

### Specification

The specification requires each responsibility to be placed in a separate class, coordinated through a small service, with independent unit testing of the due-date rules.

The implementation follows these requirements by separating calculation, persistence, notification, and workflow coordination.

### Architecture

The architecture uses a small set of classes and interfaces rather than putting the complete workflow into one class.

`LibraryLoanService` acts as the coordinator, while `DueDateCalculator`, `LoanRepository`, and `OverdueNotifier` handle their individual responsibilities.

The repository and notification components are accessed through interfaces, which reduces direct coupling to their implementations.

### Design

The design follows SRP by giving the classes clear reasons to change.

For example:

* A change to the due-date rule affects `DueDateCalculator`.
* A change to storage affects `LoanRepository`.
* A change to notification behaviour affects `OverdueNotifier`.
* Changes to the overall workflow affect `LibraryLoanService`.

This makes the design easier to maintain than having all these responsibilities inside one large class.

### Implementation

The project is implemented as a **C# 8.0 compatible class library** targeting `.NET 8.0`.

The implementation uses simple classes and interfaces so that the SRP design remains easy to see.

No separate console application is included. The unit test project acts as the executive for the implementation.

### Testing

The project uses **xUnit** for unit testing.

The tests cover:

* Due date calculation for a normal date.
* Due date calculation across month boundaries.
* Due date calculation across year boundaries.
* Loan processing and saving.
* Overdue loan notification.
* Non-overdue loan behaviour.

The due-date calculation is tested independently from the other parts of the system.

### Documentation

This README documents the problem, design, class responsibilities, SRP application, testing approach, limitations, and commands required to build and test the project.

---

## Limitations

The current implementation is intentionally small and has some limitations:

* `LoanRepository` uses in-memory storage, so loans are lost when the application stops.
* The loan period is fixed at 30 days.
* `OverdueNotifier` is a simple implementation and does not connect to a real email or messaging system.
* The system does not currently support returning a book.
* Loan renewal is not implemented.
* Different types of books or members cannot currently have different loan periods.

These limitations are acceptable for the scope of this assignment because the main focus is demonstrating the **Single Responsibility Principle**.

---

## Project Structure

```text
LibraryLoanWorkflow/
│
├── LibraryLoanWorkflow.Core/
│   ├── Loan.cs
│   ├── DueDateCalculator.cs
│   ├── ILoanRepository.cs
│   ├── LoanRepository.cs
│   ├── IOverdueNotifier.cs
│   ├── OverdueNotifier.cs
│   ├── LibraryLoanService.cs
│   └── LibraryLoanWorkflow.Core.csproj
│
├── UnitTests/
│   ├── DueDateCalculatorTests.cs
│   ├── LibraryLoanServiceTests.cs
│   └── UnitTests.csproj
│
├── README.md
├── LICENSE
├── .gitignore
├── .gitattributes
└── LibraryLoanWorkflow.slnx
```

---

## Build and Test

### Using Visual Studio

1. Open the `LibraryLoanWorkflow.slnx` solution.
2. Select **Build → Build Solution**.
3. Open **Test Explorer**.
4. Select **Run All Tests**.

### Using the Command Line

Navigate to the repository root directory.

To build:

```bash
dotnet build
```

To run the unit tests:

```bash
dotnet test
```

The project should build successfully and all unit tests should pass.

---

## Test Summary

The current test suite contains:

```text
6 tests passed
0 failed
0 skipped
```

The tests have been successfully executed using the command line and Visual Studio Test Explorer.

---



