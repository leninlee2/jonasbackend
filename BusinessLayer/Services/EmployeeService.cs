using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResultInfo> DeleteEmployee(string employeeCode)
        {
            ResultInfo resultInfo = new ResultInfo();//treated result:
            try
            {
                var result = await _employeeRepository.DeleteEmployee(employeeCode);
                resultInfo = new ResultInfo() { Result = null, Status = !result };
                return resultInfo;
            }
            catch (Exception ex)
            {
                resultInfo.Message = ex.Message;//could be a treated message
                _logger.LogError(ex.Message);
            }
            return resultInfo;
        }

        public async Task<ResultInfo> GetAllEmployees()
        {
            ResultInfo resultInfo = new ResultInfo();//treated result:
            try
            {
                var res = await _employeeRepository.GetAll();
                var employees = _mapper.Map<IEnumerable<Employee>>(res);
                resultInfo = new ResultInfo() { Result = employees, Status = true };
                return resultInfo;
            }
            catch (Exception ex)
            {
                resultInfo.Message = ex.Message;//could be a treated message
                _logger.LogError(ex.Message);
            }
            return resultInfo;
        }

        public async Task<ResultInfo> GetEmployeeByCode(string employeeCode)
        {
            ResultInfo resultInfo = new ResultInfo();//treated result:
            try
            {
                var result = await _employeeRepository.GetByCode(employeeCode);
                var employee = _mapper.Map<Employee>(result);
                resultInfo = new ResultInfo() { Result = employee, Status = true };
                return resultInfo;
            }
            catch (Exception ex)
            {
                resultInfo.Message = ex.Message;//could be a treated message
                _logger.LogError(ex.Message);
            }
            return resultInfo;
        }

        public async Task<ResultInfo> SaveEmployee(Employee employee)
        {
            ResultInfo resultInfo = new ResultInfo();//treated result:
            try
            {
                var result = await _employeeRepository.SaveEmployee(employee);
                resultInfo = new ResultInfo() { Result = employee, Status = !result };
                return resultInfo;
            }
            catch (Exception ex)
            {
                resultInfo.Message = ex.Message;//could be a treated message
                _logger.LogError(ex.Message);
            }
            return resultInfo;
        }
    }
}
