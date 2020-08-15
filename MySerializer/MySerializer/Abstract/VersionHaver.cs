using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySerializer.Abstract
{
    [Serializable]
    public abstract class VersionHaver
    {
       public Version Version { get; set; } = new Version("1.0.0.0");
    }
}