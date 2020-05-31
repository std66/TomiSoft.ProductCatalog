using AutoMapper;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.Data.Sqlite.Entities;

namespace TomiSoft.ProductCatalog.Data.Sqlite.Mapping {
    internal class EManufacturerToManufacturerBMProfile : Profile {
        public EManufacturerToManufacturerBMProfile() {
            AddEntityToBusinessModelMapper();
            AddBusinessModelToEntityMapper();
        }

        private void AddEntityToBusinessModelMapper() {
            CreateMap<Manufacturer, ManufacturerBM>()
                .ForCtorParam("manufacturerId", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
                .ForCtorParam("location", opt => opt.MapFrom((src, context) =>
                    new ManufacturerLocationBM(
                        src.LocationCountrycode,
                        src.LocationAddress
                    )
                ))
                .ForCtorParam("websiteUri", opt => opt.MapFrom(src => src.WebsiteUrl))
                .ForCtorParam("logo", opt => opt.MapFrom((src, context) => {
                    if (src.CompanyLogo != null && src.CompanyLogoMimetype != null)
                        return new ManufacturerLogoBM(
                            src.CompanyLogo,
                            src.CompanyLogoMimetype
                        );
                    else
                        return null;
                }));
        }

        private void AddBusinessModelToEntityMapper() {
            CreateMap<ManufacturerBM, Manufacturer>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.ManufacturerId))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.LocationCountrycode, opt => opt.MapFrom(src => src.Location.CountryCode))
                .ForMember(x => x.LocationAddress, opt => opt.MapFrom(src => src.Location.Address))
                .ForMember(x => x.WebsiteUrl, opt => opt.MapFrom(src => src.WebsiteUri))
                .ForMember(x => x.CompanyLogo, opt => opt.MapFrom(src => src.Logo.Data))
                .ForMember(x => x.CompanyLogoMimetype, opt => opt.MapFrom(src => src.Logo.MimeType));
        }
    }
}
