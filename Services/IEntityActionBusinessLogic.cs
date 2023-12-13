namespace proj_csharp_kiminoyume.Services
{
    public interface IEntityActionBusinessLogic<M>
    {
        Task<List<M>> GetList();
        Task<M?> Create(M request);
        Task<M?> Update(M request);
        Task Delete(int id);
    }
}
