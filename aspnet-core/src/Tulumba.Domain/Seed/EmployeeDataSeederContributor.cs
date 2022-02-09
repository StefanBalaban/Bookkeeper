using System;
using System.Threading.Tasks;
using Tulumba.Entities.Employee;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

public class EmployeeDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Employee, Guid> _employeeRepistory;

    public EmployeeDataSeederContributor(IRepository<Employee, Guid> EmployeeRepository)
    {
        _employeeRepistory = EmployeeRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _employeeRepistory.GetCountAsync() <= 0)
        {
            await _employeeRepistory.InsertAsync(
                new Employee
                {
                    Name = "Stefan Balaban",
                    SalaryType = SalaryType.Dnevnica,
                    SalaryAmount = 30,
                    Active = true
                },
                true
            );
            await _employeeRepistory.InsertAsync(
                new Employee
                {
                    Name = "Adnan Čehić",
                    SalaryType = SalaryType.Mjesecno,
                    SalaryAmount = 1500,
                    Active = true
                },
                true
            );
        }
    }
}