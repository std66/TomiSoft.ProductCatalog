using AutoMapper;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Models;

namespace TomiSoft.ProductCatalog.Server.Mapping {
    public class LocalizedCategoryWithProductCountBMToCategoryInfoWithProductCountDtoProfile : Profile {
        public LocalizedCategoryWithProductCountBMToCategoryInfoWithProductCountDtoProfile() {
            CreateMap<LocalizedCategoryWithProductCountBM, CategoryInfoWithProductCountDto>()
                .ForMember(nameof(CategoryInfoWithProductCountDto.Name), opt => opt.MapFrom(src => src.LocalizedName));
        }
    }
}
