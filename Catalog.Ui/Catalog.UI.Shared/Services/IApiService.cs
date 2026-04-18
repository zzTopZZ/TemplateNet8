using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.UI.Shared.Services
{
    public interface IApiService
    {
        Task<T> GetAsync<T>(string route);
        Task<T> PostAsync<T>(string route, object data);
        Task<System.Net.Http.HttpResponseMessage> UpdateAsync(string route, object data);
        Task<System.Net.Http.HttpResponseMessage> DeleteAsync(string route);
        
    }
}
