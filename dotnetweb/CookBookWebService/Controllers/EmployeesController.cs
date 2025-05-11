using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Employee management operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private static readonly List<Employee> _employees = new List<Employee>()
    {
        new Employee { Id = 1, Name = "John Doe", Position = "Engineer" },
        new Employee { Id = 2, Name = "Jane Smith", Position = "Developer" }
    };

    /// <summary>
    /// Retrieves all employees.
    /// </summary>
    /// <returns>List of employees.</returns>
    [HttpGet(Name = "GetEmployees")]
    public IActionResult GetEmployees()
    {
        return Ok(_employees);
    }

    /// <summary>
    /// Retrieves a specific employee by unique id.
    /// </summary>
    /// <param name="id">Employee id.</param>
    /// <returns>Employee details.</returns>
    [HttpGet("{id}", Name = "GetEmployeeById")]
    public IActionResult GetEmployee(int id)
    {
        var employee = _employees.FirstOrDefault(e => e.Id == id);
        if (employee == null)
            return NotFound($"Employee with id {id} was not found.");

        return Ok(employee);
    }

    /// <summary>
    /// Creates a new employee record.
    /// </summary>
    /// <param name="employee">Employee object.</param>
    /// <returns>Newly created employee details.</returns>
    [HttpPost(Name = "CreateEmployee")]
    public IActionResult CreateEmployee([FromBody] Employee employee)
    {
        if (employee == null)
            return BadRequest();

        var existingEmployee = _employees.FirstOrDefault(e => e.Id == employee.Id);
        if (existingEmployee != null)
            return Conflict($"Employee with id {employee.Id} already exists.");

        _employees.Add(employee);
        return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
    }

    /// <summary>
    /// Updates an existing employee fully.
    /// </summary>
    /// <param name="id">Employee id.</param>
    /// <param name="employee">Updated employee object.</param>
    /// <returns>No content on success.</returns>
    [HttpPut("{id}", Name = "UpdateEmployee")]
    public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
    {
        if (employee == null || employee.Id != id)
            return BadRequest();

        var existingEmployee = _employees.FirstOrDefault(e => e.Id == id);

        if (existingEmployee == null)
            return NotFound($"Employee with id {id} was not found.");

        existingEmployee.Name = employee.Name;
        existingEmployee.Position = employee.Position;

        return NoContent();
    }

    /// <summary>
    /// Deletes an employee by id.
    /// </summary>
    /// <param name="id">Employee id.</param>
    /// <returns>No content on success.</returns>
    [HttpDelete("{id}", Name = "DeleteEmployee")]
    public IActionResult DeleteEmployee(int id)
    {
        var employee = _employees.FirstOrDefault(e => e.Id == id);

        if (employee == null)
            return NotFound($"Employee with id {id} was not found.");

        _employees.Remove(employee);
        return NoContent();
    }

    /// <summary>
    /// Partially updates (PATCH) an existing employee's details.
    /// </summary>
    /// <param name="id">Employee id.</param>
    /// <param name="patchDto">Employee details to update.</param>
    /// <returns>No content on success.</returns>
    [HttpPatch("{id}", Name = "PatchEmployee")]
    public IActionResult PatchEmployee(int id, [FromBody] EmployeePatchDto patchDto)
    {
        if (patchDto == null)
            return BadRequest();

        var existingEmployee = _employees.FirstOrDefault(x => x.Id == id);
        if (existingEmployee == null)
            return NotFound($"Employee with id {id} was not found.");

        if (!string.IsNullOrEmpty(patchDto.Name))
            existingEmployee.Name = patchDto.Name;

        if (!string.IsNullOrEmpty(patchDto.Position))
            existingEmployee.Position = patchDto.Position;

        return NoContent();
    }
}