using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.ViewModel;
using System;
using System.Threading.Tasks;

namespace AcademiaFlowAPI.Services.Interfaces
{
    public interface IApiClienteservice : IService<ApiClienteViewModel, long>
    {
        Task<ApiClienteViewModel?> GetByClientId(Guid clientId);
    }
}