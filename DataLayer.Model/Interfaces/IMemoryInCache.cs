using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Interfaces
{
    public interface IMemoryInCache
    {
        Dictionary<Tuple<string, string>, DataEntity> DatabaseInstance { get; set; }
    }
}
