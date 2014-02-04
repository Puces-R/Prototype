using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Puces_R
{
    public class ParametresGet
    {
        Dictionary<string, string> dico;

        public string Parametres
        {
            get
            {
                string strRetour = "";
                foreach (string s in dico.Keys)
                {
                    strRetour += strRetour == "" ? "?" : "&";
                    strRetour += s + (dico[s] != null ? "=" + dico[s] : "");
                }
                return strRetour;
            }
        }

        public ParametresGet()
        {
            dico = new Dictionary<string, string>();
        }

        public ParametresGet(string RawUrl)
        {
            string[] separate = RawUrl.Split(new char[] { '?' }, 2);
            string strParams = "";
            if (separate.Length == 2)
            {
                strParams = separate[1];
                string[] list = strParams.Split(new char[] { '&' });
                dico = list.ToDictionary(key => key.Split(new char[] { '=' }, 2)[0], value => value.Split(new char[] { '=' }, 2).Length == 2 ? value.Split(new char[] { '=' }, 2)[1] : null);
            }
            else
            {
                dico = new Dictionary<string, string>();
            }
        }

        public ParametresGet(string RawUrl, string[] keys) : this(RawUrl)
        {
            Filter(keys);
        }

        public void Remove(string key)
        {
            dico.Remove(key);
        }

        public void Set(string key, string value)
        {
            if (dico.Keys.Contains(key))
            {
                dico[key] = value;
            }
            else
            {
                dico.Add(key, value);
            }
        }

        public string Get(string key)
        {
            return dico[key];
        }

        public void Filter(string[] keys)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            foreach (string key in dico.Keys)
            {
                if (keys.Contains(key))
                {
                    d.Add(key, dico[key]);
                }
            }
            dico = d;
        }

    }
}