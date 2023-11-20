using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.IRepositories;
using Talabat.Core.IServices;
using Talabat.Core.Specifications.Order_Specifications;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository; 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;
        //private readonly IGenericRepository<Product> _productRepository; 
        //private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepository;
        //private readonly IGenericRepository<Order> _orderRepository;


        public OrderService(IBasketRepository basketRepository
              , IUnitOfWork unitOfWork
              , IPaymentService paymentService
            ///,IGenericRepository<Product> productRepository,
            ///IGenericRepository<DeliveryMethod> deliveryMethodRepository,
            ///IGenericRepository<Order> orderRepository
            )
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            ///_productRepository = productRepository;
            ///_deliveryMethodRepository = deliveryMethodRepository;
            ///_orderRepository = orderRepository;
        }

        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, Address shippingAddress, int deliveryMethodId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket is null) return null;

            var orderItems = new List<OrderItem>();
                foreach (var item in basket.Items)
                {
                    var productRepo = _unitOfWork.Repository<Product>();
                    if (productRepo != null)
                    {
                        var product = await productRepo.GetByIdAsync(item.Id);
                        var productItemOrdered = new ProductItemOrdered(product.Id , product.Name , product.PictureUrl);
                        var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                        orderItems.Add(orderItem);
                    }

                }

            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            DeliveryMethod deliveryMethod = new DeliveryMethod();
            var deliveryMethodRepo = _unitOfWork.Repository<DeliveryMethod>();
            if (deliveryMethodRepo != null)
                 deliveryMethod = await deliveryMethodRepo.GetByIdAsync(deliveryMethodId);


            var spec = new OrderWithPaymentIntentIdSpecs(basket.PaymentIntentId);
            var ExistingOrder = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);

            if (ExistingOrder != null)
            {
                _unitOfWork.Repository<Order>().Delete(ExistingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basket.Id);
            }

            var order = new Order(buyerEmail,shippingAddress, deliveryMethod, orderItems,subTotal ,basket.PaymentIntentId); 

            var orderRepo = _unitOfWork.Repository<Order>();
            if (orderRepo != null)
            {
                await orderRepo.Add(order);
            }
            var res = await _unitOfWork.Complete();

            return res > 0 ? order : null;

            
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliveryMethods;
        }

        public async Task<Order?> GetOrderByIdForUserAsnyc(string buyerEmail, int orderId)
        {
            var spec = new OrderSpecifications(buyerEmail, orderId);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec); 
            if(order is null)return null;
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecifications(buyerEmail);

            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return orders;
        }
    }
}
