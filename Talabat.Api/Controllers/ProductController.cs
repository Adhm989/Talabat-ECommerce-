using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.DTOs;
using Talabat.Api.Errors;
using Talabat.Api.Helper;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.Core.Specifications;
using static StackExchange.Redis.Role;

namespace Talabat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ///private readonly IGenericRepository<Product> _ProductRepo;
        ///private readonly IGenericRepository<ProductBrand> _brandsRepo;
        ///private readonly IGenericRepository<ProductType> _typesRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(///IGenericRepository<Product> productRepo, 
                                 ///IGenericRepository<ProductBrand> brandsRepo,
                                 ///IGenericRepository<ProductType> typesRepo,
                                 IMapper mapper, IUnitOfWork unitOfWork)
        {
            ///_ProductRepo = productRepo;
            ///_brandsRepo = brandsRepo;
            ///_typesRepo = typesRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToRreturnDto>>> GetAllProducts([FromQuery]ProductSpecParam specParam)
        {
            //var products = await _ProductRepo.GetAllAsync();
            //return Ok(products);
            var spec = new ProductWithBrandAndTypeSpecification(specParam);
            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToRreturnDto>>(products);
            var countSpec = new ProductWithSpecForCount(specParam); 
            var count = await _unitOfWork.Repository<Product>().GetCountWithSpecAsync(countSpec);
            return Ok(new Pagination<ProductToRreturnDto>(specParam.PageSize,specParam.PageIndex,data,count));

        }

        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<ProductToRreturnDto>> GetProduct(int id)
        {
            //var product = await _ProductRepo.GetByIdAsync(id);
            //if (product == null)
            //{
            //    return NotFound("Not found in database");
            //}
            //return Ok(product);
            var spec = new ProductWithBrandAndTypeSpecification(id); 

            var product = await _unitOfWork.Repository<Product>().GetByIdWithSpecAsync(spec);
            if (product == null)
                return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Product,ProductToRreturnDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllProductsBrands()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            if (brands == null)
                return NotFound(new ApiResponse(404));
            return Ok(brands);
        }
        [HttpGet("brand/{id}")]
        public async Task<ActionResult<ProductBrand>> GetBrandByIdAsync(int id)
        {
            var brand = await _unitOfWork.Repository<ProductBrand>().GetByIdAsync(id);
            if (brand == null) return NotFound(new ApiResponse(404));

            return Ok(brand);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllProductsTypes()
        {
            var types = await _unitOfWork.Repository<ProductType>().GetAllAsync();
            if (types == null)
                return NotFound(new ApiResponse(404));
            return Ok(types);
        }
        [HttpGet("types/{id}")]
        public async Task<ActionResult<ProductType>> GetTypeByIdAsync(int id)
        {
            var type = await _unitOfWork.Repository<ProductType>().GetByIdAsync(id);
            if (type == null) return NotFound(new ApiResponse(404));

            return Ok(type);
        }
    }
}
