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
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Attributes;
using Microsoft.AspNetCore.Authorization;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Models;

namespace TomiSoft.ProductCatalog.Server.OpenApiGenerated.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public abstract class ManufacturerApiController : ControllerBase
    { 
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Gets information about every known manufacturers</remarks>
        /// <response code="200">The server successfully fulfilled the request and returned all the available manufacturer&#39;s information</response>
        /// <response code="500">Server error occurred</response>
        [HttpGet]
        [Route("/Manufacturer")]
        [ValidateModelState]
        [ProducesResponseType(statusCode: 200, type: typeof(List<ManufacturerInfoDto>))]
        [ProducesResponseType(statusCode: 500, type: typeof(ErrorResultDto))]
        public abstract Task<IActionResult> GetAllManufacturers();

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Gets the company&#39;s logo image</remarks>
        /// <param name="manufacturerId">The ID of the requested manufacturer.</param>
        /// <response code="200">The request succeeded and the service returned an image file that represents the manufacturer&#39;s logo.</response>
        /// <response code="404">The following cases can cause this response. See the error code in the response body. 1. The requested manufacturer does not exist. The error code is \&quot;ManufacturerNotFound\&quot; 2. The requested manufacturer exists, but there is no company logo found. The error code is \&quot;ManufacturerLogoNotFound\&quot; </response>
        /// <response code="500">Server error occurred</response>
        [HttpGet]
        [Route("/Manufacturer/{manufacturerId}/CompanyLogo")]
        [ValidateModelState]
        [ProducesResponseType(statusCode: 200, type: typeof(System.IO.Stream))]
        [ProducesResponseType(statusCode: 404, type: typeof(ErrorResultDto))]
        [ProducesResponseType(statusCode: 500, type: typeof(ErrorResultDto))]
        public abstract Task<IActionResult> GetCompanyLogo([FromRoute][Required]long manufacturerId);

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Saves a new manufacturer</remarks>
        /// <param name="postManufacturerRequestDto"></param>
        /// <response code="201">The manufacturer has been successfully created.</response>
        /// <response code="500">Server error occurred</response>
        [HttpPost]
        [Route("/Manufacturer")]
        [ValidateModelState]
        [ProducesResponseType(statusCode: 201, type: typeof(PostManufacturerResponseDto))]
        [ProducesResponseType(statusCode: 500, type: typeof(ErrorResultDto))]
        public abstract Task<IActionResult> PostNewManufacturer([FromBody]PostManufacturerRequestDto postManufacturerRequestDto);
    }
}
