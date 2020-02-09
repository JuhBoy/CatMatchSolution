using CatMatch.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatMatch.Extensions.Database
{
    public static class CatDbSetExtensions
    {
        public static IQueryable<Cat> IncludeSubModels(this DbSet<Cat> dbSet)
        {
            return dbSet.Include(c => c.Rank).Include(c => c.Informations);
        }
    }
}
