using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Puces_R
{
    /// <summary>
    /// Description résumée de Deconnexion
    /// </summary>
    public class Deconnexion : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Session.Clear();
            context.Response.Redirect("Default.aspx");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}