using Services.DTO.RequestDTO.Catalog;
using Services.DTO.ResponseDTO.Catalog;
using Services.IService.Catalog;
using Stripe;
using Stripe.Checkout;

#nullable disable

namespace Services.Service.Catalog
{
    public class CatalogService() : ICatalogService
    {
        public List<CheckoutDTO> GetCheckouts(string[] entities)
        {
            throw new NotImplementedException();
        }

        public CheckoutDTO GetCheckout(long id, string[] entities)
        {
            throw new NotImplementedException();
        }

        public StripeList<Session> GetStripeCheckouts()
        {
            throw new NotImplementedException();
        }

        public Session GetStripeCheckout(string id)
        {
            throw new NotImplementedException();
        }

        public Task<CheckoutDTO> CreateCheckout(CheckoutModel orderModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeactivateCheckout(long id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCheckout(long id, CheckoutModel orderModel)
        {
            throw new NotImplementedException();
        }
    }
}