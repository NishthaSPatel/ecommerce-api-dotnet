using DataAccess.Context;
using DataAccess.Repositories.IRepository.Catalog;

namespace DataAccess.Repositories.Repository.Catalog
{
    public class CatalogRepository(CoreFunctionalityContext context) : RepositoryBase(context), ICatalogRepository
    {
    }
}