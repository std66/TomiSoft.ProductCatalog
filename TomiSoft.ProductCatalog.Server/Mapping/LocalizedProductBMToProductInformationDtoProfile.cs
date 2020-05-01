using AutoMapper;
using System.Collections.Generic;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Models;

namespace TomiSoft.ProductCatalog.Server.Mapping {
    public class LocalizedProductBMToProductInformationDtoProfile : Profile {
        public LocalizedProductBMToProductInformationDtoProfile() {
            CreateMap<LocalizedProductBM, ProductInformationDto>()
                .ForMember(nameof(ProductInformationDto.Barcode), opt => opt.MapFrom(nameof(LocalizedProductBM.Barcode)))
                .ForMember(nameof(ProductInformationDto.Name), opt => opt.MapFrom(nameof(LocalizedProductBM.LocalizedName)))
                .ForMember(nameof(ProductInformationDto.Images), opt => opt.MapFrom(input => new List<ProductImageDto>()))
                .ForMember(nameof(ProductInformationDto.Manufacturer), opt => opt.MapFrom(
                    input => new ManufacturerInfoDto() {
                        Address = input.Manufacturer.Location.Address,
                        CountryCode = input.Manufacturer.Location.CountryCode,
                        ManufacturerId = input.Manufacturer.ManufacturerId,
                        Name = input.Manufacturer.Name,
                        Website = input.Manufacturer.WebsiteUri.ToString()
                    }    
                ))
                .ForMember(nameof(ProductInformationDto.Category), opt => opt.MapFrom(
                    input => new CategoryDto() {
                        CategoryId = input.Category.CategoryId,
                        Name = input.Category.LocalizedName
                    }
                ));
        }
    }
}
