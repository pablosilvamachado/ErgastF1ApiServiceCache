using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgastF1ApiServiceCache.Domain.Models
{
    // Estrutura para os dados da API
    public class MRData
    {
        public string Series { get; set; }
        public string Total { get; set; }       
        public SeasonTable SeasonTable { get; set; }
    }       
}
