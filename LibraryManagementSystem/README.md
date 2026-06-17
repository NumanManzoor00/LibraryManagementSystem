# Library Management System

A desktop application built with **C# (.NET 10)** and **Windows Forms (WinForms)** backed by **Microsoft SQL Server**, built using the same layered architecture as the reference `HelpDeskTicketSystem` project for this course.

---

## Architecture & Design Notes

This project deliberately mirrors the `HelpDeskTicketSystem` reference solution's architecture and conventions so the two projects are directly comparable:

| HelpDeskTicketSystem | Library Management System | Relationship |
|---|---|---|
| `User` | `Member` | Standalone entity, no foreign key |
| `Admin` | `Librarian` | Standalone staff directory, no foreign key |
| `Ticket` | `Book` | Standalone catalog item, no foreign key (a book isn't "raised by" anyone, so unlike `Ticket` it has zero FKs rather than one) |
| `Response` | `Loan` | Two foreign keys (`BookId` + `MemberId`), joined back to their parent tables with `LEFT JOIN`, exactly like `Response` joins to `Ticket` + `Admin` |

Both projects share the same two-project layout (`App.Core` business logic + `App.WindowsApp` presentation), the same ADO.NET data-access pattern (raw `SqlConnection`/`SqlCommand`/`SqlDataReader` with parameterized queries, no ORM), the same mode-driven popup form pattern (`Add` / `Edit` / `View` enums), and the same dual charting strategy (hand-drawn GDI+ charts on the Dashboard, plus a separate LiveCharts2 page).

One behavioral enhancement beyond the reference project: `LoanForm` keeps a Book's `Status` in sync with its loan lifecycle automatically — issuing a loan marks the book `Borrowed`, and marking a loan `Returned` marks the book `Available` again — which is richer business-logic validation than the simple required-field checks used elsewhere.

---

## Project Structure

```
LibraryManagementSystem/
├── LibraryManagementSystem.sln
├── LibraryDB_Setup.sql
│
├── App.Core/                        ← Business Logic Layer
│   ├── Enums/
│   │   ├── BookGenre.cs, BookStatus.cs, LoanStatus.cs
│   ├── Models/
│   │   ├── Member.cs, Librarian.cs, Book.cs, Loan.cs
│   ├── Interfaces/
│   │   ├── IMemberService.cs        ← includes GetAllMembersAsync()
│   │   ├── ILibrarianService.cs     ← includes GetAllLibrariansAsync()
│   │   ├── IBookService.cs          ← includes GetAllBooksAsync()
│   │   └── ILoanService.cs          ← includes GetAllLoansAsync()
│   └── Services/
│       ├── MemberService.cs, LibrarianService.cs
│       ├── BookService.cs, LoanService.cs
│
└── App.WindowsApp/                  ← Presentation Layer
    ├── Program.cs
    ├── App.config                   ← connection string here
    ├── Resources/                   ← plain PNG icons (no embedded-resource indirection)
    │   ├── icons8-dashboard-32.png, icons8-books-32.png, icons8-members-32.png
    │   ├── icons8-librarian-32.png, icons8-loans-32.png, icons8-charts-32.png
    │   └── app.png
    ├── Forms/
    │   ├── FormModeEnums.cs
    │   ├── DashboardForm            ← shell + sidebar + StatusStrip
    │   ├── BookForm, MemberForm, LibrarianForm, LoanForm
    └── Views/
        ├── DashboardView            ← summary stats + GDI+ charts (async)
        ├── LiveChartView            ← LiveCharts2 Pie + Bar charts
        ├── BookView                 ← async load, search, status filter, sort
        ├── MemberView                ← async load, search, sort
        ├── LibrarianView             ← async load, search, sort
        └── LoanView                  ← async load, search, status filter, sort
```

---

## Setup Instructions

### 1. Prerequisites
- Visual Studio 2022 (v17.12+) or Rider with **.NET 10 SDK** installed
- SQL Server (LocalDB / Express / Developer Edition)

### 2. Database Setup
1. Open **SQL Server Management Studio (SSMS)** or **Azure Data Studio**
2. Connect to your SQL Server instance
3. Run `LibraryDB_Setup.sql`

### 3. Configure Connection String
Edit `App.WindowsApp/App.config`:
```xml
<connectionStrings>
  <add name="LibraryDB"
       connectionString="Server=YOUR_SERVER;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True;"
       providerName="Microsoft.Data.SqlClient" />
</connectionStrings>
```
Replace `YOUR_SERVER` (e.g. `localhost`, `.\SQLEXPRESS`, `(localdb)\MSSQLLocalDB`).

### 4. Build & Run
```bash
dotnet build LibraryManagementSystem.sln
dotnet run --project App.WindowsApp
```
Or open in Visual Studio → set `App.WindowsApp` as startup project → F5.

### 5. Replacing Icons
Drop a replacement PNG into `App.WindowsApp/Resources/` with the same filename, then update the matching entry in `Properties/Resources.resx` if the filename changes. Icons are loaded directly through the standard `Properties.Resources` mechanism — there's no extra embedded-resource helper class to keep in sync.

---

## Feature Checklist (mapped to the grading rubric)

| Rubric Criterion | Marks | Status | Notes |
|---|---|---|---|
| Project Setup & Architecture | 5 | ✅ | Two-project solution (`App.Core` class library + `App.WindowsApp` WinForms app) referenced via `ProjectReference`, mirroring the reference solution exactly |
| Database & Connection | 5 | ✅ | `LibraryDB_Setup.sql` creates all 4 tables; connection string lives in `App.config` and is read via `ConfigurationManager` |
| Data Access Layer (ADO.NET) | 8 | ✅ | Every entity has an interface + service using `SqlConnection`/`SqlCommand`/`SqlDataReader` inside `using` blocks, parameterized with `AddWithValue` (no string concatenation), plus `GetAllAsync()` on every service |
| UI Navigation & CRUD | 10 | ✅ | `DashboardForm` sidebar shell swaps `UserControl` views; all 4 entities have full Add/Edit/View/Delete/Refresh toolbars over a `DataGridView` |
| Validation & UX | 2 | ✅ | Required-field checks with `MessageBox` feedback on every form, plus `LoanForm` additionally validates due-date-after-issue-date and auto-syncs the linked book's status |
| Charting Module | 10 | ✅ | Two independent chart implementations: hand-drawn GDI+ Pie/Bar on the Dashboard, and a LiveCharts2 Pie/Bar page, both backed by live data |
| Code Quality & Organization | 2 | ✅ | Single consistent Designer.cs coding convention used throughout (no mixed hand-written/designer-generated styles); no unused/dead resource-loading code |
| Demo & Individual Viva | 8 | — | Assessed live; not determinable from source code |

### Bonus features (capped at +5)
- [x] Search + filter (text search on every list view; status-filter dropdown on Books and Loans)
- [x] Dashboard view with summary statistics
- [x] Async (`GetAllAsync()` used by every list view)
- [x] Status bar (active section, record count, current time)
- [x] Column sorting (click any header to sort ascending/descending)
- [x] Loading indicator (Refresh buttons disable and relabel "Loading..." consistently across **every** view, including Members/Librarians — not just the views with extra filters)

---

## Technology Stack

| Component     | Technology                        |
|---------------|-----------------------------------|
| Language      | C# (.NET 10)                      |
| UI Framework  | Windows Forms (WinForms)          |
| Database      | Microsoft SQL Server              |
| Data Access   | Microsoft.Data.SqlClient (ADO.NET)|
| Charting      | LiveChartsCore.SkiaSharpView.WinForms |
| Icons         | Plain PNG resources via `Properties.Resources` |
| Configuration | App.config / ConfigurationManager |
| Target OS     | Windows                           |

---

## Notes on Differences from the Reference Project

This project was built by studying `HelpDeskTicketSystem` and replicating its architecture, so a few small issues noticed in that codebase were deliberately corrected here rather than copied over:

1. **SQL setup script is actually included.** The reference project's README documents a `HelpDeskDB_Setup.sql` setup step, but that file isn't present in the delivered zip. `LibraryDB_Setup.sql` here is real and matches the column names used in every service's SQL text.
2. **No dead icon-loading code.** The reference project ships an `AppIcons.cs` helper and a `Resources/Icons/` folder of PNGs that are never actually referenced anywhere in the UI (the toolbar buttons use plain Unicode glyphs instead). This project only keeps the icon assets it actually wires up, through the standard `Properties.Resources` designer mechanism.
3. **Sidebar icons are visible.** The reference project's sidebar icons are black glyphs on a transparent background, displayed against a dark navy sidebar — effectively invisible. The icons here were drawn in a light color specifically so they read clearly against the same dark sidebar.
4. **The window icon is actually set.** `DashboardForm.Icon` is assigned from the embedded `app` bitmap at startup; the reference project never sets `Form.Icon` at all.
5. **One Designer.cs coding convention throughout.** The reference project mixes two styles — some Designer.cs files use the older `this.`-qualified pattern, others use the modern unqualified style enabled by global usings. Every generated file in this project uses the latter consistently.
6. **No build artifacts in the zip.** `bin/`, `obj/`, and `.vs/` folders are excluded from this delivery; they should never be committed or shipped in a source zip.
