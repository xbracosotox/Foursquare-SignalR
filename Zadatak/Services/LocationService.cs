using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Zadatak.Entity;
using Zadatak.Hubs;
using Zadatak.Models;
using Zadatak.Services.Interfaces;

namespace Zadatak.Services
{
    public class LocationService : ILocationService
    {
        private readonly LocationsDbContext _context;
        private readonly MyConfig _myConfig;
        private readonly IHubContext<ChatHub> _hubContext;

        public LocationService(LocationsDbContext context, MyConfig myConfig, IHubContext<ChatHub> hubcontext)
        {
            _context = context;
            _myConfig = myConfig;
            _hubContext = hubcontext;
        }
        public IQueryable<LocationDTO> GetLocations()
        {
            var location = from loc in _context.Locations

                           select new LocationDTO()
                           {
                               latitude = loc.latitude,
                               longitude = loc.longitude
                           };
            return location;
        }

        public async Task<Object> GetResponseFromServiceWithQuery(GetResponseFromServiceWithQueryDto model)
        {
            string timeFormatForUrl = DateTime.Now.ToString("yyyyMMdd");

            LocationEntity location = new LocationEntity();
            location.latitude = model.latitude;
            location.longitude = model.longitude;

            var loc = new LocationDTO();
            loc.latitude = model.latitude;
            loc.longitude = model.longitude;

            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("OnLocation", loc);
           
            HttpClient httpClient = new HttpClient();
            string rawStringJSON = await httpClient.GetStringAsync($"{_myConfig.foursquare} { model.latitude.Value.ToString(CultureInfo.InvariantCulture)},{ model.longitude.Value.ToString(CultureInfo.InvariantCulture)}&v={ timeFormatForUrl}&limit=10&name={ model.categoryQuery}");
            Object deserializedJSON = JsonConvert.DeserializeObject(rawStringJSON);
            return deserializedJSON;
        }

        public async Task<Object> GetResponseFromService(LocationDTO model)
        {

            string timeFormatForUrl = DateTime.Now.ToString("yyyyMMdd");

            LocationEntity location = new LocationEntity();
            location.latitude = model.latitude;
            location.longitude = model.longitude;

            var loc = new LocationDTO();
            loc.latitude = model.latitude;
            loc.longitude = model.longitude;

            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("OnLocation", loc);

            HttpClient httpClient = new HttpClient();
            string rawStringJSON = await httpClient.GetStringAsync($"{_myConfig.foursquare}{ model.latitude.Value.ToString(CultureInfo.InvariantCulture)},{ model.longitude.Value.ToString(CultureInfo.InvariantCulture)}&v={ timeFormatForUrl}&limit=10");
            Object deserializedJSON = JsonConvert.DeserializeObject(rawStringJSON);
            return deserializedJSON;

        }
    }
}
