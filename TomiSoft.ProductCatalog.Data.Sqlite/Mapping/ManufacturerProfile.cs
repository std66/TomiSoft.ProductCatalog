using AutoMapper;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.Data.Sqlite.Entities;

namespace TomiSoft.ProductCatalog.Data.Sqlite.Mapping {
    internal class ManufacturerProfile : Profile {
        public ManufacturerProfile() {
            AddEntityToBusinessModelMapper();
            AddBusinessModelToEntityMapper();
        }

        private void AddEntityToBusinessModelMapper() {
            CreateMap<EManufacturer, ManufacturerBM>()
                .ForCtorParam("manufacturerId", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
                .ForCtorParam("location", opt => opt.MapFrom((src, context) =>
                    new ManufacturerLocationBM(
                        src.CountryCode,
                        src.Address
                    )
                ))
                .ForCtorParam("websiteUri", opt => opt.MapFrom(src => src.WebsiteUri))
                .ForCtorParam("logo", opt => opt.MapFrom((src, context) => {
                    if (src.LogoData != null && src.LogoMimeType != null)
                        return new ManufacturerLogoBM(
                            src.LogoData,
                            src.LogoMimeType
                        );
                    else
                        return null;
                }));
        }

        private void AddBusinessModelToEntityMapper() {
            CreateMap<ManufacturerBM, EManufacturer>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.ManufacturerId))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.CountryCode, opt => opt.MapFrom(src => src.Location.CountryCode))
                .ForMember(x => x.Address, opt => opt.MapFrom(src => src.Location.Address))
                .ForMember(x => x.WebsiteUri, opt => opt.MapFrom(src => src.WebsiteUri))
                .ForMember(x => x.LogoData, opt => opt.MapFrom(src => src.Logo.Data))
                .ForMember(x => x.LogoMimeType, opt => opt.MapFrom(src => src.Logo.MimeType));
        }
    }
}
