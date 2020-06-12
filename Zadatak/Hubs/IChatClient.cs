using System.Threading.Tasks;
using Zadatak.Models;

namespace Zadatak.Hubs
{
    public interface IChatClient
    {
        Task OnLocation(LocationDTO model);
    }
}

