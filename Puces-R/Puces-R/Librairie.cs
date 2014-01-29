using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Puces_R
{
    public class Librairie
    {
        public static SqlConnection Connexion
        {
            get
            {
                return new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2;");
            }
        }
    }
}