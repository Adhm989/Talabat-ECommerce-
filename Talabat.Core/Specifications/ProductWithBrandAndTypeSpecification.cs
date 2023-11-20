using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpecification(ProductSpecParam specParam):base(P => 
            (string.IsNullOrEmpty(specParam.Search) || P.Name.ToLower().Contains(specParam.Search)) &&
            (!specParam.BrandId.HasValue || specParam.BrandId == P.ProductBrandId) &&
            (!specParam.TypeId.HasValue || specParam.TypeId == P.ProductTypeId)
        )
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);

            if (!string.IsNullOrEmpty(specParam.Sort))
            {
                switch (specParam.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }

            ApplyPagination(specParam.PageSize * (specParam.PageIndex-1) ,specParam.PageSize);
        }
        public ProductWithBrandAndTypeSpecification(int id): base (P => P.Id == id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }
    }
}
