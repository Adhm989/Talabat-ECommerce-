using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithSpecForCount : BaseSpecification<Product>
    {
        public ProductWithSpecForCount(ProductSpecParam specParam) : base(P =>
            (string.IsNullOrEmpty(specParam.Search) || P.Name.ToLower().Contains(specParam.Search)) &&
            (!specParam.BrandId.HasValue || specParam.BrandId == P.ProductBrandId) &&
            (!specParam.TypeId.HasValue || specParam.TypeId == P.ProductTypeId))
        {
        }
    }
}
