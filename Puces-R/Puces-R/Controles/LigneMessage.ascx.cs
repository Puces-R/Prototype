using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puces_R
{
    public partial class LigneMessage : System.Web.UI.UserControl
    {
        private Int64 _noMessage = -1;

        public Int64 NoMessage
        {
            get
            {
                return _noMessage;
            }
            set
            {
                _noMessage = value;
            }
        }

        public bool Lu
        {
            set
            {
                trLu.Style.Add(HtmlTextWriterStyle.FontWeight, value ? "normal" : "bold");
            }
        }

        public bool Checked
        {
            get
            {
                return cb.Checked;
            }
        }

        public string De
        {
            set
            {
                lblFrom.Text = value;
            }
        }

        public string Sujet
        {
            set
            {
                lblSubject.Text = value;
            }
        }

        public DateTime Date
        {
            set
            {
                lblDate.Text = value.ToString("d MMMM yyyy à H\\hmm");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }


        protected void voirMessage(object sender, EventArgs e)
        {
            Session["NoMessage"] = ((LigneMessage)((Control)sender).Parent.Parent.Parent).NoMessage;
            Response.Redirect("VoirMessage.aspx", true);
        }
    }
}