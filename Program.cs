using MiniHRMS.Models;
using MiniHRMS.Services;

var employeeService = new EmployeeService();
var attendanceService = new AttendanceService();
bool running = true;

while (running)
{
    Console.WriteLine("\n===== Mini HRMS =====");
    Console.WriteLine("1. Add Employee");
    Console.WriteLine("2. View All Employees");
    Console.WriteLine("3. Update Employee");
    Console.WriteLine("4. Delete Employee");
    Console.WriteLine("5. Mark Attendance");
    Console.WriteLine("6. View Attendance");
    Console.WriteLine("7. Exit");
    Console.Write("Choose an option: ");
    string choice = Console.ReadLine() ?? "";

    switch (choice)
    {
        case "1":
            AddEmployee(employeeService);
            break;
        case "2":
            ViewAllEmployees(employeeService);
            break;
        case "3":
            UpdateEmployee(employeeService);
            break;
        case "4":
            DeleteEmployee(employeeService);
            break;
        case "5":
            MarkAttendance(employeeService, attendanceService);
            break;
        case "6":
            ViewAttendance(employeeService, attendanceService);
            break;
        case "7":
            running = false;
            Console.WriteLine("Goodbye!");
            break;
        default:
            Console.WriteLine("❌ Invalid option, try again.");
            break;
    }
}

void AddEmployee(EmployeeService service)
{
    Console.Write("Enter Full Name: ");
    string name = Console.ReadLine() ?? "";

    Console.Write("Enter Department: ");
    string dept = Console.ReadLine() ?? "";

    Console.Write("Enter Designation: ");
    string designation = Console.ReadLine() ?? "";

    Console.Write("Enter Joining Date (yyyy-mm-dd): ");
    DateTime joiningDate;
    while (!DateTime.TryParse(Console.ReadLine(), out joiningDate))
    {
        Console.Write("❌ Invalid date format. Please enter as yyyy-mm-dd: ");
    }

    Console.Write("Enter Email: ");
    string email = Console.ReadLine() ?? "";

    var employee = new Employee
    {
        FullName = name,
        Department = dept,
        Designation = designation,
        JoiningDate = joiningDate,
        Email = email
    };

    service.AddEmployee(employee);
}

void ViewAllEmployees(EmployeeService service)
{
    var employees = service.GetAllEmployees();

    if (employees.Count == 0)
    {
        Console.WriteLine("No employees found.");
        return;
    }

    Console.WriteLine("\n{0,-5} {1,-20} {2,-15} {3,-20} {4,-12} {5,-25}",
        "ID", "Name", "Department", "Designation", "Joining", "Email");
    Console.WriteLine(new string('-', 100));

    foreach (var emp in employees)
    {
        Console.WriteLine("{0,-5} {1,-20} {2,-15} {3,-20} {4,-12:yyyy-MM-dd} {5,-25}",
            emp.EmployeeId, emp.FullName, emp.Department, emp.Designation, emp.JoiningDate, emp.Email);
    }
}

void UpdateEmployee(EmployeeService service)
{
    ViewAllEmployees(service);

    Console.Write("\nEnter Employee ID to update: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("❌ Invalid ID.");
        return;
    }

    Console.Write("Enter New Full Name: ");
    string name = Console.ReadLine() ?? "";

    Console.Write("Enter New Department: ");
    string dept = Console.ReadLine() ?? "";

    Console.Write("Enter New Designation: ");
    string designation = Console.ReadLine() ?? "";

    Console.Write("Enter New Joining Date (yyyy-mm-dd): ");
    DateTime joiningDate;
    while (!DateTime.TryParse(Console.ReadLine(), out joiningDate))
    {
        Console.Write("❌ Invalid date format. Please enter as yyyy-mm-dd: ");
    }

    Console.Write("Enter New Email: ");
    string email = Console.ReadLine() ?? "";

    var employee = new Employee
    {
        FullName = name,
        Department = dept,
        Designation = designation,
        JoiningDate = joiningDate,
        Email = email
    };

    service.UpdateEmployee(id, employee);
}

void DeleteEmployee(EmployeeService service)
{
    ViewAllEmployees(service);

    Console.Write("\nEnter Employee ID to delete: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("❌ Invalid ID.");
        return;
    }

    Console.Write($"Are you sure you want to delete Employee ID {id}? (Y/N): ");
    string confirm = Console.ReadLine() ?? "";

    if (confirm.Trim().ToUpper() == "Y")
    {
        service.DeleteEmployee(id);
    }
    else
    {
        Console.WriteLine("Deletion cancelled.");
    }
}

void MarkAttendance(EmployeeService empService, AttendanceService attService)
{
    ViewAllEmployees(empService);

    Console.Write("\nEnter Employee ID: ");
    if (!int.TryParse(Console.ReadLine(), out int empId))
    {
        Console.WriteLine("❌ Invalid ID.");
        return;
    }

    Console.Write("Enter Attendance Date (yyyy-mm-dd): ");
    DateTime attDate;
    while (!DateTime.TryParse(Console.ReadLine(), out attDate))
    {
        Console.Write("❌ Invalid date format. Please enter as yyyy-mm-dd: ");
    }

    Console.Write("Enter Check-In Time (HH:mm, 24-hour): ");
    TimeSpan checkIn;
    while (!TimeSpan.TryParse(Console.ReadLine(), out checkIn))
    {
        Console.Write("❌ Invalid time format. Please enter as HH:mm: ");
    }

    Console.Write("Enter Check-Out Time (HH:mm, 24-hour): ");
    TimeSpan checkOut;
    while (!TimeSpan.TryParse(Console.ReadLine(), out checkOut))
    {
        Console.Write("❌ Invalid time format. Please enter as HH:mm: ");
    }

    Console.Write("Enter Status (Present/Absent/Leave): ");
    string status = Console.ReadLine() ?? "Present";

    var attendance = new Attendance
    {
        EmployeeId = empId,
        AttendanceDate = attDate,
        CheckIn = checkIn,
        CheckOut = checkOut,
        Status = status
    };

    attService.MarkAttendance(attendance);
}

void ViewAttendance(EmployeeService empService, AttendanceService attService)
{
    ViewAllEmployees(empService);

    Console.Write("\nEnter Employee ID to view attendance: ");
    if (!int.TryParse(Console.ReadLine(), out int empId))
    {
        Console.WriteLine("❌ Invalid ID.");
        return;
    }

    var records = attService.GetAttendanceByEmployee(empId);

    if (records.Count == 0)
    {
        Console.WriteLine("No attendance records found for this employee.");
        return;
    }

    Console.WriteLine("\n{0,-12} {1,-10} {2,-10} {3,-10}", "Date", "CheckIn", "CheckOut", "Status");
    Console.WriteLine(new string('-', 50));

    foreach (var att in records)
    {
        Console.WriteLine("{0,-12:yyyy-MM-dd} {1,-10} {2,-10} {3,-10}",
            att.AttendanceDate, att.CheckIn, att.CheckOut, att.Status);
    }
}