namespace AcademiaFlowAPI.Interfaces
{
    public interface IService<TViewModel, TKey> where TViewModel : class
    {
        Task<IEnumerable<TViewModel>> GetAll(); 
        Task<TViewModel?> GetById(TKey id);
        Task<TViewModel> Create(TViewModel model);
        Task<bool> Update(TKey id, TViewModel model);
        Task<bool> Delete(TKey id);
    }
}
