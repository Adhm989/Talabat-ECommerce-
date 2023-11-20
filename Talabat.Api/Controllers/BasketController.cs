using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.DTOs;
using Talabat.Api.Errors;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;

namespace Talabat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string id)
        {
            var basket  = await _basketRepository.GetBasketAsync(id);
            return basket is null ? new CustomerBasket(id) : basket;    
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasketAsync(CustomerBasketDTO basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDTO, CustomerBasket>(basket);
            var updatedOrCreatedBasket = await _basketRepository.UpdateBasketAsync(mappedBasket);
            if (updatedOrCreatedBasket is null) return BadRequest(new ApiResponse(400));

            return Ok(updatedOrCreatedBasket);

        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeletbasketAsync(string id)
        {
            return await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
