using AutoMapper;
using TomiSoft.ProductCatalog.Data.Sqlite.Entities;
using TomiSoft.ProductCatalog.Data.Sqlite.Entities.Projections;

namespace TomiSoft.ProductCatalog.Data.Sqlite.Mapping {
    class ProjectionProfile : Profile {
        public ProjectionProfile() {
            CreateMap<Manufacturer, PBriefManufacturer>();
            CreateMap<Manufacturer, PManufacturerCompanyLogo>();
        }
    }
}
