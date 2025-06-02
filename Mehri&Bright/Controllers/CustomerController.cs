using AutoMapper;
using Mehri_Bright.Entities;
using Mehri_Bright.Models;
using Mehri_Bright.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Mehri_Bright.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper) //ctor
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            var customerEntites = await _customerRepository.GetAllCustomersAsync();
            return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customerEntites));
        }

        [HttpGet("{id:int}", Name = "GetCustomer")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomerAsync(id);
            if (customer == null)
            {
                return NotFound($"Customer with id {id} was not found.");
            }

            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        [HttpGet("by-email/{email}", Name = "GetCustomerByEmail")]
        public async Task<IActionResult> GetCustomerByEmail(string email)
        {
            var customer = await _customerRepository.GetCustomerByEmailAsync(email);
            if (customer == null)
            {
                return NotFound($"Customer with email {email} was not found");
            }

            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerForCreationDto customer)
        {
            if (customer.PhoneNumber.IsNullOrEmpty()) 
            {
                return BadRequest("A phone number must be provided");
            }

            var customerEmailExists = _customerRepository.CustomerEmailExists(customer.Email);

            if (customerEmailExists == true)
            {
                return Conflict($"Email {customer.Email} is already registered as a customer");
            }

            var customerEntity = _mapper.Map<Entities.Customer>(customer);

            await _customerRepository.AddCustomer(customerEntity);

            await _customerRepository.SaveChangesAsync();

            var createdCustomerToReturn = _mapper.Map<Models.CustomerDto>(customerEntity);

            return CreatedAtRoute("GetCustomer",
                new
                {
                    id = customerEntity.Id
                },
                createdCustomerToReturn);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerToUpdateDto customer)
        {
            var customerToUpdate = await _customerRepository.GetCustomerAsync(id);
            if (customerToUpdate == null)
            {
                return NotFound($"Customer with id {id} was not found.");
            }

            var customerEmailExists = await _customerRepository.GetCustomerByEmailAsync(customer.Email);
            if (customerEmailExists != null && customerEmailExists.Email != customer.Email)
            {
                return BadRequest($"A customer with email {customer.Email} already exists in the system");
            }

            var customerEntity = _mapper.Map(customer, customerToUpdate);

            await _customerRepository.UpdateCustomerAsync(customerEntity);

            await _customerRepository.SaveChangesAsync();

            return Ok($"Customer with id {id} was updated successfully.");
        }
    }
}
