using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming
{
    public class TCity
    {
        public string Name { get; set; }
        public string RegionName { get; set; }
        public int Population { get; set; }
        public List<TLibrary> Libraries = new List<TLibrary>();
    }
}
