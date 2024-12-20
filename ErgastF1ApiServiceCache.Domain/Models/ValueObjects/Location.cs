using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgastF1ApiServiceCache.Domain.Models.ValueObjects
{
    public class Location
    {
        public string lat { get; set; }
        public string lng { get; set; }
        public string locality { get; set; }
        public string country { get; set; }
    }
}
