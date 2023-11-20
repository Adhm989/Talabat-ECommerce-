using AutoMapper;
using Talabat.Api.DTOs;
using Talabat.Core.Entities;

namespace Talabat.Api.Helper
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToRreturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductToRreturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";

            return string.Empty;
        }
    }
}
