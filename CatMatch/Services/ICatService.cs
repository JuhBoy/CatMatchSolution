using CatMatch.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatMatch.Services
{
    public interface ICatService
    {
        Task<Cat> Get(int id, RequestOptions options);
        Task<IEnumerable<Cat>> GetAll(RequestOptions options);
        Task InjectCats();
    }
}