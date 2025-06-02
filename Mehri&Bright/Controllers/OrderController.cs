using AutoMapper;
using Mehri_Bright.Entities;
using Mehri_Bright.Models;
using Mehri_Bright.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mehri_Bright.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        [HttpGet]
        public async Task<ActionResult<OrderDto>> GetAllOrders()
        {
            var orderEntities = await _orderRepository.GetAllOrdersAsync();
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orderEntities));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDto>> GetOrdersForCustomer(int id)
        {
            var customerExists = _customerRepository.CustomerExists(id);
            if (!customerExists)
            {
                return NotFound($"Customer with id {id} was not found");
            }

            var ordersForCustomer = await _orderRepository.GetOrdersForCustomerAsync(id);
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(ordersForCustomer));
        }

        [HttpGet("by-id/{orderId:int}")]
        public async Task<ActionResult<OrderDto>> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound($"There is no order with order number: {orderId}");
            }

            return Ok(_mapper.Map<OrderDto>(order));
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> AddOrder([FromBody] CreateOrderDto createOrderDto)
        {
            try
            {

                if (!_customerRepository.CustomerExists(createOrderDto.CustomerId))
                {
                    return NotFound($"Customer with id {createOrderDto.CustomerId} was not found");
                }

                if (createOrderDto.OrderProducts == null || !createOrderDto.OrderProducts.Any())
                {
                    return BadRequest("Order must contain at least one product");
                }

                var order = _mapper.Map<Order>(createOrderDto);

                await _orderRepository.AddOrderAsync(order);
                await _orderRepository.SaveChangesAsync();

                var createdOrder = await _orderRepository.GetOrderByIdAsync(order.Id);
                var orderDto = _mapper.Map<OrderDto>(createdOrder);

                return CreatedAtAction(nameof(GetOrdersForCustomer), new { id = order.CustomerId }, orderDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the order: {ex.Message}");
            }
        }
       
    }
}
