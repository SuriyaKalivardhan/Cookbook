/// <summary>
/// Employee data model.
/// </summary>
public class Employee
{
    /// <summary>
    /// Unique identifier of the employee.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Full name of the employee.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Job position of the employee.
    /// </summary>
    public string Position { get; set; }
}

/// <summary>
/// DTO used for partially updating an employee.
/// </summary>
public class EmployeePatchDto
{
    /// <summary>
    /// Employee name. Optional for patching.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Employee position. Optional for patching.
    /// </summary>
    public string Position { get; set; }
}
