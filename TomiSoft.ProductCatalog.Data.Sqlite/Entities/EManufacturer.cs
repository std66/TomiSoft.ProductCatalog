using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TomiSoft.ProductCatalog.Data.Sqlite.Entities {
    [Table("manufacturer")]
    internal class EManufacturer : IEqualityComparer<EManufacturer> {
        [Column("id"), Required]
        public int Id { get; set; }

        [Column("name"), Required]
        public string Name { get; set; }

        [Column("location_countrycode"), Required]
        public string CountryCode { get; set; }

        [Column("location_zipcode"), Required]
        public int ZipCode { get; set; }

        [Column("location_address"), Required]
        public string Address { get; set; }

        [Column("website_url")]
        public string WebsiteUri { get; set; }

        public bool Equals(EManufacturer x, EManufacturer y) {
            if (x == null && y == null)
                return true;

            if (x == null && y != null || x != null && y == null)
                return false;

            return x.Id == y.Id;
        }

        public int GetHashCode(EManufacturer obj) {
            unchecked {
                return 7
                    * Id
                    * Name.GetHashCode()
                    * CountryCode.GetHashCode()
                    * ZipCode
                    * Address.GetHashCode();
            }
        }
    }
}
