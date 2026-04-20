using Services.DTO.RequestDTO.Catalog;
using Services.DTO.ResponseDTO.Catalog;
using Stripe;

namespace Services.IService.Catalog
{
    public interface ICatalogService
    {
        #region Checkout

        List<CheckoutDTO> GetCheckouts(string[] entities);
        CheckoutDTO GetCheckout(long id, string[] entities);
        StripeList<Stripe.Checkout.Session> GetStripeCheckouts();
        Stripe.Checkout.Session GetStripeCheckout(string id);
        Task<CheckoutDTO> CreateCheckout(CheckoutModel orderModel);
        Task<bool> UpdateCheckout(long id, CheckoutModel orderModel);
        Task<bool> DeactivateCheckout(long id);

        #endregion
    }
}