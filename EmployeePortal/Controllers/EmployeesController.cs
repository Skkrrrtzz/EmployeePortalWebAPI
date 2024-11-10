using EmployeePortal.Models;
using EmployeePortal.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            try
            {
                var employees = await _employeeRepository.GetAllEmployees();
                return Ok(employees);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

                if (employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }
            catch (EmployeeNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _employeeRepository.AddEmployeeAsync(employee);
                return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest("Employee ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _employeeRepository.UpdateEmployeeAsync(employee);
                return Ok();
            }
            catch (EmployeeNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var success = await _employeeRepository.DeleteEmployeeAsync(id);
                if (!success)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}