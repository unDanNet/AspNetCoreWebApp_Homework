namespace DigitalGamesStoreService.Services.Repositories
{
    public interface IRepository<TModel, TId> where TId : struct
    {
        public IEnumerable<TModel> GetAll();

        public TModel GetById(TId id);

        public TId Create(TModel data);

        public void Update(TModel data);

        public void Delete(TId id);
    }
}

