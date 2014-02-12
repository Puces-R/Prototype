using System;
using System.IO;
using System.Web;
using System.Data.SqlClient;
using Puces_R;

namespace Puces_R
{
    public class TelechargementMessage : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        private SqlConnection connexion = Librairie.Connexion;

        public void ProcessRequest(HttpContext context)
        {
            string mediaName = null;
            string nomFichier = null;

            if (context.Session["NoDownload"] == null)
            {
                return;
            }
            else
            {
                SqlCommand cmdNomFichier = new SqlCommand("SELECT FichierJoint FROM PPMessages WHERE NoMessage = @no", connexion);
                cmdNomFichier.Parameters.AddWithValue("no", context.Session["NoDownload"]);
                connexion.Open();
                nomFichier = (string)cmdNomFichier.ExecuteScalar();
                connexion.Close();
                mediaName = "msg" + context.Session["NoDownload"] + "_" + nomFichier;
                context.Session.Remove("NoDownload");
            }

            if (string.IsNullOrEmpty(mediaName))
            {
                return;
            }

            string destPath = System.Web.HttpContext.Current.Server.MapPath("~/MsgDownload/" + mediaName);
            FileInfo fi = new FileInfo(destPath);

            if (fi.Exists)
            {
                System.Web.HttpContext.Current.Response.ClearHeaders();
                System.Web.HttpContext.Current.Response.ClearContent();
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Length", fi.Length.ToString());
                System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + nomFichier + "\"");
                System.Web.HttpContext.Current.Response.BinaryWrite(ReadByteArryFromFile(destPath));
                System.Web.HttpContext.Current.Response.End();
            }
        }

        private byte[] ReadByteArryFromFile(string destPath)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(destPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(destPath).Length;
            buff = br.ReadBytes((int)numBytes);
            return buff;
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