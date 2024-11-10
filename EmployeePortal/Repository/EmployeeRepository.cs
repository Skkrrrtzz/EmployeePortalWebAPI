using EmployeePortal.Data;
using EmployeePortal.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _appDbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var employee = await _appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                throw new EmployeeNotFoundException("Employee with specified ID not found.");
            }
            return employee;
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            await _appDbContext.Employees.AddAsync(employee);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            var existingEmployee = await _appDbContext.Employees.FindAsync(employee.Id);
            if (existingEmployee == null)
            {
                throw new EmployeeNotFoundException("Employee not found.");
            }

            _appDbContext.Entry(existingEmployee).CurrentValues.SetValues(employee);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _appDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return false;
            }

            _appDbContext.Employees.Remove(employee);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }

    [Serializable]
    internal class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException()
        {
        }

        public EmployeeNotFoundException(string? message) : base(message)
        {
        }

        public EmployeeNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}