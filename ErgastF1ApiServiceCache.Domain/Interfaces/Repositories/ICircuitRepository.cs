using ErgastF1ApiServiceCache.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgastF1ApiServiceCache.Domain.Interfaces.Repositories
{
    public interface ICircuitRepository
    {
        Task<List<Circuit>> GetAsync();
    }
}
