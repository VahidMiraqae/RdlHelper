using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RdlHelper.Models.Models
{
    public class RdlParameterOption
    {
        public bool IsNullable { get; set; }
        public bool AllowsBlank { get; set; }
        public bool MultiValue { get; set; }
    }
}
