using AutoMapper;
using DataAccess.Models.Stripe;
using Services.DTO.RequestDTO.Stripe;
using Services.DTO.ResponseDTO.Stripe;

namespace Services.Mapping
{
    public class StripeMapperProfile : Profile
    {
        public StripeMapperProfile()
        {
            #region Stripe

            CreateMap<Customer, CustomerModel>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();

            CreateMap<Product, StripeProductModel>().ReverseMap();
            CreateMap<Product, StripeProductDTO>().ReverseMap();

            CreateMap<Payment, PaymentModel>().ReverseMap();
            CreateMap<Payment, PaymentDTO>().ReverseMap();

            CreateMap<PaymentStatus, PaymentStatusModel>().ReverseMap();
            CreateMap<PaymentStatus, PaymentStatusDTO>().ReverseMap();

            CreateMap<CheckoutPayment, CheckoutPaymentModel>().ReverseMap();
            CreateMap<CheckoutPayment, CheckoutPaymentDTO>().ReverseMap();

            #endregion
        }
    }
}