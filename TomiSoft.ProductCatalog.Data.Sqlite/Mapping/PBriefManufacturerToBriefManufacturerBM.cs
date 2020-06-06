using AutoMapper;
using System;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.BusinessModels.Concepts;
using TomiSoft.ProductCatalog.Data.Sqlite.Entities.Projections;

namespace TomiSoft.ProductCatalog.Data.Sqlite.Mapping {
    class PBriefManufacturerToBriefManufacturerBM : Profile {
        public PBriefManufacturerToBriefManufacturerBM() {
            CreateMap<PBriefManufacturer, BriefManufacturerBM>()
                .ForCtorParam("name", opt => opt.MapFrom(input => input.Name))
                .ForCtorParam("manufacturerId", opt => opt.MapFrom(input => new ManufacturerIdBM(input.Id)))
                .ForCtorParam("websiteUri", opt => opt.MapFrom(input => input.WebsiteUrl == null ? null : new Uri(input.WebsiteUrl)))
                .ForCtorParam("location", opt => opt.MapFrom(
                    input => new ManufacturerLocationBM(
                        input.LocationCountryCode,
                        input.LocationAddress
                    )
                ));
        }
    }
}
