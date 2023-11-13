using Application.DTOModels.Models.Admin.Pagination;
using Application.DTOModels.Models.User.Order;
using Application.DTOModels.Response.User;
using Application.Services.Interfaces.IServices;
using Application.Services.Interfaces.IServices.User;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPIKurs.Controllers.User
{
    //[Authorize]
    public class OrderController : Controller
    {
        private readonly IPaginationService _paginationService;
        private readonly IOrderService _orderService;

        public OrderController(IPaginationService paginationService, IOrderService orderService)
        {
            _paginationService = paginationService;
            _orderService = orderService;
        }

        [HttpGet("User/Order/Product")]
        public async Task<IActionResult> UserGetProductWithPaginationAsync([FromQuery] UserProductQueryParametersDto productModel)
        {
            return Ok(await _paginationService.UserGetProductWithPaginationAsync(productModel));
        }

        [HttpGet("User/Order/")]
        public async Task<IActionResult> UserGetOrderWithPaginationAsync([FromQuery] UserOrderQueryParametersDto orderModel)
        {
            return Ok(await _paginationService.UserGetOrderWithPaginationAsync(orderModel));
        }

        [HttpPost("User/Order/")]
        public async Task<IActionResult> CreateOrderAsync([FromQuery] OrderCreateDto orderModel)
        {
            return Ok(await _orderService.CreateOrderAsync(orderModel));
        }

        [HttpPut("User/Order/")]
        public async Task<IActionResult> EditOrderAsync(OrderEditDto orderModel)
        {
            return Ok(await _orderService.EditOrderAsync(orderModel.OrderId, orderModel));
        }

        [HttpDelete("User/Order/")]
        public async Task<IActionResult> DeleteOrderAsync(int orderId)
        {
            return Ok(await _orderService.DeleteOrderAsync(orderId));
        }

    }
}
