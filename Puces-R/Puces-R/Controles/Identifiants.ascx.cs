﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class Identifiants : System.Web.UI.UserControl
    {

        SqlConnection connexion = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2;");

        public string Adresse
        {
            get
            {
                return tbCourriel.Adresse;
            }
        }

        public string MotDePasse
        {
            get
            {
                return tbMDP.MotDePasse;
            }
        }

        protected void validerCourrielIdentique(object sender, ServerValidateEventArgs e)
        {
            if (!tbCourriel.IsValid || tbCourriel.Adresse == tbCourrielConfirmation.Text.ToLower())
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
        }
    }
}