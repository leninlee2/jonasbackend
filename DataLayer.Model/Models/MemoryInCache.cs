using DataAccessLayer.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Models
{
    public class MemoryInCache : IMemoryInCache
    {
        public Dictionary<Tuple<string, string>, DataEntity> DatabaseInstance { get; set; }

        public MemoryInCache()
        {
            if (DatabaseInstance == null)
                DatabaseInstance = new Dictionary<Tuple<string, string>, DataEntity>();
        }
    }
}
