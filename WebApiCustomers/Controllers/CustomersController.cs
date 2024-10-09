using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiCustomers.Repositories;
using WebApiCustomers.Dtos; 
using WebApiCustomers.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiCustomers.Controllers;
   
   [ApiController]
   [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        
         private readonly ICustomerRepository _customerRepository;
         private readonly IMapper _mapper;

        public CustomersController(ICustomerRepository repository, IMapper mapper)
        {
            _customerRepository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var listCustomers = await _customerRepository.GetAllAsync();
            var result = _mapper.Map<List<CustomerReadDto>>(listCustomers);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            var result = _mapper.Map<CustomerReadDto>(customer);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] CustomerCreateDto customerCreateDto)
        {
            var customerToInsert = _mapper.Map<Customer>(customerCreateDto);
            await _customerRepository.CreateAsync(customerToInsert);
            return CreatedAtAction("Get", new { id = customerToInsert.Id }, customerToInsert);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerUpdateDto customerUpdateDto)
        {
            if (id != customerUpdateDto.Id)
            {
                return BadRequest();
            }

            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            _mapper.Map(customerUpdateDto, customer);
            await _customerRepository.UpdateAsync(id, customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            await _customerRepository.DeleteAsync(id);
            return NoContent();
        }
        
        
    }

