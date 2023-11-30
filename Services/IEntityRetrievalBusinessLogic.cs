namespace proj_csharp_kiminoyume.Services
{
    public interface IEntityRetrievalBusinessLogic<T>: IEntityActionBusinessLogic<T>
    {
        Task<T?> GetById(int id);
    }
}
