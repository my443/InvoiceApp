using InvoiceApp.Data;
using InvoiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceApp.ViewModels
{
    public class EmployeeViewModel
    {
        private List<Employee> _employees = new List<Employee>();
        private readonly ApplicationDbContext _appDbContext;

        public EmployeeViewModel(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // Create a new employee
        public void AddEmployee(string fullName, string eeId, string emailAddress, int approvalLimit, bool isProcessor)
        {
            Employee newEmployee = new Employee
            {
                FullName = fullName,
                EE_ID = eeId,
                EmailAddress = emailAddress,
                ApprovalLimit = approvalLimit,
                IsProcessor = isProcessor
            };

            _appDbContext.Employees.Add(newEmployee);
            _appDbContext.SaveChanges();
        }

        // Read an employee by ID
        public Employee GetEmployee(int id)
        {
            return _appDbContext.Employees.FirstOrDefault(e => e.Id == id);
        }

        // Update an existing employee
        public void UpdateEmployee(int id, string fullName, string eeId, string emailAddress, int approvalLimit, bool isProcessor)
        {
            Employee employeeToUpdate = _appDbContext.Employees.FirstOrDefault(e => e.Id == id);
            if (employeeToUpdate != null)
            {
                employeeToUpdate.FullName = fullName;
                employeeToUpdate.EE_ID = eeId;
                employeeToUpdate.EmailAddress = emailAddress;
                employeeToUpdate.ApprovalLimit = approvalLimit;
                employeeToUpdate.IsProcessor = isProcessor;

                _appDbContext.SaveChanges();
            }
        }

        // Delete an employee by ID
        public void DeleteEmployee(int id)
        {
            Employee employeeToDelete = _appDbContext.Employees.FirstOrDefault(e => e.Id == id);
            if (employeeToDelete != null)
            {
                _appDbContext.Employees.Remove(employeeToDelete);
                _appDbContext.SaveChanges();
            }
        }

        // Get the list of employees
        public List<Employee> GetEmployeeList()
        {
            _employees = _appDbContext.Employees.ToList();
            return _employees;
        }
    }
}

