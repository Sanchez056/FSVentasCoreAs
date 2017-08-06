using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FSVentasCoreAs.Report
{
    public class ActionAsPdf
    {
        private RouteValueDictionary routeValuesDict;
        private object routeValues;
        private string action;

        public ActionAsPdf(string action)
            : base()
        {
            this.action = action;
        }

        public ActionAsPdf(string action, RouteValueDictionary routeValues)
            : this(action)
        {
            this.routeValuesDict = routeValues;
        }

        public ActionAsPdf(string action, object routeValues)
            : this(action)
        {
            this.routeValues = routeValues;
        }

       
    }
}
