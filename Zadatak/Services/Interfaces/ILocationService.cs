using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak.Models;

namespace Zadatak.Services.Interfaces
{
    public interface ILocationService
    {
        IQueryable<LocationDTO> GetLocations();
        Task<Object> GetResponseFromServiceWithQuery(GetResponseFromServiceWithQueryDto model);
        Task<Object> GetResponseFromService(LocationDTO model);
    }
}
