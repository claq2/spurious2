using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spurious2.Core.SubdivisionImporting.Domain
{
    public class SubdivisionPopulation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }

        public SubdivisionPopulation()
        {
        }

        public override string ToString()
        {
            return $"{this.Name} Pop: {this.Population}";
        }
    }
}
