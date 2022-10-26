using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        // GET api/<controller>
        [HttpGet]
        public async Task<ResultInfo> Get()
        {
            var items = await _employeeService.GetAllEmployees();
            return items;
        }

        // GET api/<controller>/5
        [HttpGet]
        public async Task<ResultInfo> Get(string id)
        {
            var item = await _employeeService.GetEmployeeByCode(id);
            return item;
        }

        // POST api/<controller>
        public async Task<ResultInfo> Post([FromBody] Employee employee)
        {
            var response = await _employeeService.SaveEmployee(employee);
            return response;
        }

        // PUT api/<controller>/5
        public async Task<ResultInfo> Put(int id, [FromBody] Employee employee)
        {
            var response = await _employeeService.SaveEmployee(employee);
            return response;
        }

        // DELETE api/<controller>/5
        public async Task<ResultInfo> Delete(int id)
        {
            var response = await _employeeService.DeleteEmployee(id.ToString());
            return response;
        }
    }
}