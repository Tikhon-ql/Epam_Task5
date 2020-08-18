using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySerializer.Interfaces
{
    /// <summary>
    /// Interface of version owner
    /// </summary>
    public interface IVersionHaver
    {
       Version Version { get; set; } 
    }
}