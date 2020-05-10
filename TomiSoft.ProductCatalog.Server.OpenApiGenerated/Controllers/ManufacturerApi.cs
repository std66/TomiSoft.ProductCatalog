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
    }
}
