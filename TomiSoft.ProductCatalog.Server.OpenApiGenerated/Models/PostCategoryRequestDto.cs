/*
 * TomiSoft Product Database Api
 *
 * This document describes the API provided by the TomiSoft.ProductCatalog.Server microservice. This microservice provides information about products, based on barcode.
 *
 * The version of the OpenAPI document: 1.0
 * Contact: sinkutamas@gmail.com
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Converters;

namespace TomiSoft.ProductCatalog.Server.OpenApiGenerated.Models
{ 
    /// <summary>
    /// Provides information about a new category that needs to be saved
    /// </summary>
    [DataContract]
    public class PostCategoryRequestDto : IEquatable<PostCategoryRequestDto>
    {
        /// <summary>
        /// A key-value pair object representing the product name for a specific language.
        /// </summary>
        /// <value>A key-value pair object representing the product name for a specific language.</value>
        [DataMember(Name="CategoryName", EmitDefaultValue=false)]
        public Dictionary<string, string> CategoryName { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PostCategoryRequestDto {\n");
            sb.Append("  CategoryName: ").Append(CategoryName).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((PostCategoryRequestDto)obj);
        }

        /// <summary>
        /// Returns true if PostCategoryRequestDto instances are equal
        /// </summary>
        /// <param name="other">Instance of PostCategoryRequestDto to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PostCategoryRequestDto other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    CategoryName == other.CategoryName ||
                    CategoryName != null &&
                    other.CategoryName != null &&
                    CategoryName.SequenceEqual(other.CategoryName)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (CategoryName != null)
                    hashCode = hashCode * 59 + CategoryName.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(PostCategoryRequestDto left, PostCategoryRequestDto right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PostCategoryRequestDto left, PostCategoryRequestDto right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
