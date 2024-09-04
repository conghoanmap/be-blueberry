using blueberry.Dtos.Order;
using blueberry.Extensions;
using blueberry.Interfaces;
using blueberry.Mappers;
using blueberry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace blueberry.Controllers
{
    [Route("api/order")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<AppUser> _userManager;
        public OrderController(IOrderRepository orderRepository, UserManager<AppUser> userManager)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderRequest orderRequest)
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);

            var order = OrderMapper.RequestToModel(orderRequest);
            order.AppUserId = appUser.Id;

            var result = await _orderRepository.CreateAsync(order);
            return Ok(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var userId = appUser.Id;

            var orders = await _orderRepository.getAllAsync(userId);
            var orderDtos = orders.Select(order => order.ModelToDisplay()).ToList();
            return Ok(orderDtos);
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetails([FromRoute] int orderId)
        {
            var orderDetails = await _orderRepository.GetOrderDetailsAsync(orderId);
            var orderDetailDtos = orderDetails.Select(orderDetail => orderDetail.ModelToDisplay()).ToList();
            return Ok(orderDetailDtos);
        }
        [HttpPost]
        [Route("{orderId}/{productId}")]
        public async Task<IActionResult> CreateOrderDetail([FromRoute] int orderId, [FromRoute] string productId, [FromBody] int quantity)
        {
            var orderDetail = OrderMapper.RequestToModel(quantity);
            orderDetail.OrderId = orderId;
            orderDetail.ProductId = productId;

            var result = await _orderRepository.CreateOrderDetailAsync(orderDetail);
            return Ok(result);
        }
    }
}