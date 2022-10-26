using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbWrapper<Employee> _companyDbWrapper;

        public EmployeeRepository(IDbWrapper<Employee> companyDbWrapper)
        {
            _companyDbWrapper = companyDbWrapper;
        }

        public async Task<bool> DeleteEmployee(string employeeCode)
        {
            return await _companyDbWrapper.DeleteAsync(t => t.EmployeeCode.Equals(employeeCode));
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _companyDbWrapper.FindAllAsync();
        }

        public async Task<Employee> GetByCode(string employeeCode)
        {
            return (await _companyDbWrapper.FindAsync(t => t.EmployeeCode.Equals(employeeCode)))?.FirstOrDefault();
        }

        public async Task<bool> SaveEmployee(Employee employee)
        {
            var itemRepo = _companyDbWrapper.Find(t =>
                t.SiteId.Equals(employee.SiteId) && t.EmployeeCode.Equals(employee.EmployeeCode))?.FirstOrDefault();
            if (itemRepo != null)
            {
                itemRepo.CompanyCode = employee.CompanyCode;
                itemRepo.EmailAddress = employee.EmailAddress;
                itemRepo.EmployeeCode = employee.EmployeeCode;
                itemRepo.EmployeeName = employee.EmployeeName;
                itemRepo.EmployeeStatus = employee.EmployeeStatus;
                itemRepo.LastModified = employee.LastModified;
                itemRepo.Occupation = employee.Occupation;
                itemRepo.Phone = employee.Phone;
                return await _companyDbWrapper.UpdateAsync(itemRepo);
            }
            return await _companyDbWrapper.InsertAsync(employee);
        }
    }

}
