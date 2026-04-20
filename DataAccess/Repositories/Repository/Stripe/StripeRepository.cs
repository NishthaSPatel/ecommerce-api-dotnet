using DataAccess.Context;
using DataAccess.Repositories.IRepository.Stripe;

namespace DataAccess.Repositories.Repository.Stripe
{
    public class StripeRepository(CoreFunctionalityContext context) : RepositoryBase(context), IStripeRepository
    {
    }
}