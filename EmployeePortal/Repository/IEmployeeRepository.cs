using EmployeePortal.Models;

namespace EmployeePortal.Repository
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetAllEmployees();

        public Task<Employee> GetEmployeeByIdAsync(int id);

        public Task AddEmployeeAsync(Employee employee);

        public Task UpdateEmployeeAsync(Employee employee);

        public Task<bool> DeleteEmployeeAsync(int id);
    }
}