using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model.Models
{
    public class ResultInfo
    {
        public bool Status { get; set; } = false;
        public object Result { get; set; }

        public string Message { get; set; }
    }
}
