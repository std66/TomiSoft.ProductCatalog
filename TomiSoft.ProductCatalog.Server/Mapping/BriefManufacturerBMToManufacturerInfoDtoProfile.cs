using AutoMapper;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Models;

namespace TomiSoft.ProductCatalog.Server.Mapping {
    public class BriefManufacturerBMToManufacturerInfoDtoProfile : Profile {
        public BriefManufacturerBMToManufacturerInfoDtoProfile() {
            CreateMap<BriefManufacturerBM, ManufacturerInfoDto>()
                .ForMember(nameof(ManufacturerInfoDto.Address), opt => opt.MapFrom(input => input.Location.Address))
                .ForMember(nameof(ManufacturerInfoDto.CountryCode), opt => opt.MapFrom(input => input.Location.CountryCode))
                .ForMember(nameof(ManufacturerInfoDto.ManufacturerId), opt => opt.MapFrom(input => input.ManufacturerId))
                .ForMember(nameof(ManufacturerInfoDto.Name), opt => opt.MapFrom(input => input.Name))
                .ForMember(nameof(ManufacturerInfoDto.Website), opt => opt.MapFrom(input => input.WebsiteUri.ToString()));
        }
    }
}
