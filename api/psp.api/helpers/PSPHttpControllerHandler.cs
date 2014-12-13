using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace psp.api.helpers
{
    public class PSPHttpControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        public PSPHttpControllerHandler(RouteData routeData) : base(routeData)
        { }
    }
}