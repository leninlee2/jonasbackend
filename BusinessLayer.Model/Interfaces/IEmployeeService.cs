using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model.Interfaces
{
    public interface IEmployeeService
    {
        Task<ResultInfo> GetAllEmployees();
        Task<ResultInfo> GetEmployeeByCode(string employeeCode);
        Task<ResultInfo> SaveEmployee(Employee employee);
        Task<ResultInfo> DeleteEmployee(string employeeCode);
    }
}
