using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Puces_R
{
    public class LectureXML
    {
        private string nom = "";
        private string numeroCouleur = "";
        private bool existe = false;

        public LectureXML(long noVendeur) 
        {
            String fichier = Librairie.lireXML(HttpContext.Current.Server.MapPath("~/XML/" + noVendeur.ToString() + ".xml"));
            String[] tab = fichier.Split('|');
            String couleur = tab[1];
            NomLogo=tab[2];
            Couleur = couleur;
            Existe = (tab[0] != String.Empty);
        }

        public bool Existe
        {
            get
            {
                return existe;
            }
            set
            {
                existe = value;
            }
        }

        public string NomLogo
        {
            get
            {
                return nom;
            }
            set
            {
                nom = value;
            }
        }

        public string Couleur
        {
            get
            {
                return numeroCouleur;
            }
            set
            {
                numeroCouleur = value;
            }
        }

       
    }
}