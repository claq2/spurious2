using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spurious2.Core.Reading.Domain
{
    public class Subdivision
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public decimal Density { get; set; }
        public string Centre { get; set; }
        public long Volume { get; set; }
    }
}
