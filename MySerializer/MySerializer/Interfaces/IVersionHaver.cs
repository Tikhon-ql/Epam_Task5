using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySerializer.Interfaces
{
    public interface IVersionHaver
    {
       Version Version { get; set; } 
    }
}