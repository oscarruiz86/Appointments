namespace Application.Dtos.Employees
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public Guid TenantId { get; set; }
        public string Color { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
