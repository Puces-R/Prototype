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
        private bool _brouillon = false;
        

        public bool Brouillon
        {
            set
            {
                _brouillon = value;
            }
        }

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
            set
            {
                cb.Checked = value;
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
            Int64 no = ((LigneMessage)((Control)sender).Parent.Parent.Parent).NoMessage;
            ParametresGet paramGet = new ParametresGet(Request.RawUrl);
            string boite = paramGet.Get("Boite");
            string boiteTxt = "Retour à la boîte principale";
            switch (boite)
            {
                case "2":
                    boiteTxt = "Retour aux messages archivés";
                    break;
                case "3":
                    boiteTxt = "Retour à la corbeille";
                    break;
                case "-1":
                    boiteTxt = "Retour aux messages envoyés";
                    break;
                case "-2":
                    boiteTxt = "Retour aux brouillons";
                    break;
            }
            Response.Redirect(Chemin.Ajouter((_brouillon ? "EnvoyerMessage.aspx?NoMessage=" : "VoirMessage.aspx?No=") + no, boiteTxt), true);
        }
    }
}