using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Service
{
    public interface INewsApiClient
    {
        Task<IEnumerable<NewsModel>> GetAll(string bearerToken);
    }
}
