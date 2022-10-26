using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET api/<controller>
        [HttpGet]
        public async Task<ResultInfo> Get()
        {
            var items = await _companyService.GetAllCompanies();
            return items;
        }

        // GET api/<controller>/5
        [HttpGet]
        public async Task<ResultInfo> Get(string id)
        {
            var item = await _companyService.GetCompanyByCode(id);
            return item;
        }

        // POST api/<controller>
        public async Task<ResultInfo> Post([FromBody] Company company)
        {
            var response = await _companyService.SaveCompany(company);
            return response;
        }

        // PUT api/<controller>/5
        public async Task<ResultInfo> Put(int id, [FromBody] Company company)
        {
            company.SiteId = id.ToString();
            var response = await _companyService.SaveCompany(company);
            return response;
        }

        // DELETE api/<controller>/5
        public async Task<ResultInfo> Delete(int id)
        {
            var response = await _companyService.DeleteCompany(id);
            return response;
        }
    }
}