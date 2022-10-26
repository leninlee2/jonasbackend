using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface ICompanyService
    {
        Task<ResultInfo> GetAllCompanies();
        Task<ResultInfo> GetCompanyByCode(string companyCode);
        Task<ResultInfo> SaveCompany(Company company);
        Task<ResultInfo> DeleteCompany(int id);
    }
}
