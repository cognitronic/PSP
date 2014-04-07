using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace psp.core.domain
{
    public class IAPIResponse
    {
        string Message { get; set; }
        
        string ReturnUrl { get; set; }
    }
}
