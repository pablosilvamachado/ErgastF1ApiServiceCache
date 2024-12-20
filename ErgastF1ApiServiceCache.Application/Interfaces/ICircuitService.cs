using ErgastF1ApiServiceCache.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgastF1ApiServiceCache.Application.Interfaces
{
    public interface ICircuitService
    {
        Task<List<Circuit>> GetAsync();
    }
}
