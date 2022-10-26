using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;

namespace BusinessLayer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private ILogger<CompanyService> _logger;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper, ILogger<CompanyService> logger)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResultInfo> DeleteCompany(int id)
        {
            ResultInfo resultInfo = new ResultInfo();//treated result:
            try
            {
                var status = await _companyRepository.DeleteCompany(id.ToString());
                resultInfo = new ResultInfo() { Result = null, Status = !status };
                return resultInfo;
            }
            catch (Exception ex)
            {
                resultInfo.Message = ex.Message;//could be a treated message
                _logger.LogError(ex.Message);
            }
            return resultInfo;
        }

        public async Task<ResultInfo> GetAllCompanies()
        {
            ResultInfo resultInfo = new ResultInfo();//treated result:
            try
            {
                var res = await _companyRepository.GetAll();
                var endresult = _mapper.Map<IEnumerable<CompanyInfo>>(res);
                resultInfo = new ResultInfo() { Result = endresult, Status = true };
                return resultInfo;
            }
            catch(Exception ex)
            {
                resultInfo.Message = ex.Message;//could be a treated message
                _logger.LogError(ex.Message);
            }
            return resultInfo;
        }

        public async Task<ResultInfo> GetCompanyByCode(string companyCode)
        {
            ResultInfo resultInfo = new ResultInfo();//treated result:
            try
            {
                var result = await _companyRepository.GetByCode(companyCode);
                var companies = _mapper.Map<CompanyInfo>(result);
                resultInfo = new ResultInfo() { Result = companies, Status = true };
                return resultInfo;
            }
            catch (Exception ex)
            {
                resultInfo.Message = ex.Message;//could be a treated message
                _logger.LogError(ex.Message);
            }
            return resultInfo;
        }

        public async Task<ResultInfo> SaveCompany(Company company)
        {
            ResultInfo resultInfo = new ResultInfo();//treated result:
            try
            {
                var result = await _companyRepository.SaveCompany(company);
                resultInfo = new ResultInfo() { Result = null, Status = result };
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
