using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySerializer.Models
{
    public class Version
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && obj is Version version &&
                   Name == version.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }
    }
}
