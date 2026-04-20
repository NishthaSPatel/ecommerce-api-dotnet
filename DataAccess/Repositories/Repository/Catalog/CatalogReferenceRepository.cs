using DataAccess.Context;
using DataAccess.Repositories.IRepository.Catalog;

namespace DataAccess.Repositories.Repository.Catalog
{
    public class CatalogReferenceRepository(CoreFunctionalityContext context) : RepositoryBase(context), ICatalogReferenceRepository
    {
    }
}