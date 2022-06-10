using AutoMapper;
using Core.Entities.OrdersAggregate;
using Core.Interfaces;
using ECommerce.Dto;
using ECommerce.Errors;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = "ssurielyugo@gmail.com";
            var address = this.mapper.Map<Address>(orderDto.ShipToAddress);
            var order = await orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);
            if (order == null)
            {
                return BadRequest(new ApiResponse(400, "Problem creating order"));
            }

            return Ok(order);
        }
    }
}
