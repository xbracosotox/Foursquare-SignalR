using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Zadatak.Entity;
using Zadatak.Models;
using System.Net.Http;
using Zadatak.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Zadatak.Hubs;

namespace Zadatak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationEntitiesController : ControllerBase
    {
        private readonly LocationsDbContext _context;
        private readonly ILocationService _locationService;


        public LocationEntitiesController(LocationsDbContext context, ILocationService locationService)
        {
            _context = context;
            _locationService = locationService;
        }


        [HttpGet("/GetCheckedLocations")]
        public IQueryable<LocationDTO> GetLocations()
        {
            var location = _locationService.GetLocations();
            
            return location;
        }

        [HttpPost("/LatLongWithCategory")]
        public async Task<ActionResult<LocationEntity>> GetResponseFromServiceWithQuery([FromBody] GetResponseFromServiceWithQueryDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _locationService.GetResponseFromServiceWithQuery(model);
            return Ok(result);
        }

        [HttpPost("/LatLong")]
        public async Task<ActionResult<LocationEntity>> GetResponseFromService([FromBody] LocationDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _locationService.GetResponseFromService(model);
            return Ok(result);
        }

    }
}