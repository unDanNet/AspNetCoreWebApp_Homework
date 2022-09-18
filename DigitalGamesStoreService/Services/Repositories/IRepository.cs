namespace DigitalGamesStoreService.Services.Repositories
{
    public interface IRepository<TModel, TId> where TId : struct
    {
        public IEnumerable<TModel> GetAll(bool loadInnerData = false);

        public TModel GetById(TId id, bool loadInnerData = false);

        public TId Create(TModel data);

        public void Update(TModel data);

        public bool Delete(TId id);
    }
}

