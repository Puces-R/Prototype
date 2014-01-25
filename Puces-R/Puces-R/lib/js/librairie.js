/*
	attacheBalise, afficheOuMasqueBalise, objetBalise, formDYN, hiddenDYN, divDYN, pDYN, brDYN, spanDYN, 
	labelDYN, retourneObjet, selectDYN, optionDYN, buttonDYN, inputDYN, imgDYN, tableDYN, trDYN, tdDYN
*/	  
	  /*
      |-------------------------------------------------------------------------------------|
      | attacheBalise(10-jan-2012; 07-jan-2013)
      |-------------------------------------------------------------------------------------|
      */
      function attacheBalise(objBalise, strIDAttache_objAttache) {
         if (typeof(strIDAttache_objAttache) == 'string') {
            if (strIDAttache_objAttache == '') {
               /* Attachement � la balise BODY */
               document.body.appendChild(objBalise);
            }
            else {
               /* Attachement � la balise sp�cifi�e sous forme de cha�ne */
               objetBalise(strIDAttache_objAttache).appendChild(objBalise);
            }
         }
         else {
            /* Attachement � la balise sp�cifi�e sous forme d'objet */
            strIDAttache_objAttache.appendChild(objBalise);
         }
      }
      /*
      |-------------------------------------------------------------------------------------|
      | afficheOuMasqueBalise (02-ao�-2011)
      |-------------------------------------------------------------------------------------|
      */
      function afficheOuMasqueBalise(strIDBalise_objBalise, binVisible) {
         var objBalise = typeof (strIDBalise_objBalise) == 'string' ? 
            document.getElementById(strIDBalise_objBalise) : strIDBalise_objBalise;
         objBalise.style.display = binVisible ? 'inline' : 'none';
     }

     function afficheOuMasqueBalise(strIDBalise_objBalise) {
         var objBalise = typeof (strIDBalise_objBalise) == 'string' ?
            document.getElementById(strIDBalise_objBalise) : strIDBalise_objBalise;

         objBalise.style.display =  (objBalise.style.display == 'none' ? 'inline' : 'none');
     }
      /*
      |-------------------------------------------------------------------------------------|
      | objetBalise (28-mai-2011)
      |-------------------------------------------------------------------------------------|
      */
      function objetBalise(strIDBalise) {
         var objBalise = document.getElementById(strIDBalise);
         if (!objBalise) {
            alert('Attention... balise ' + strIDBalise + ' inexistante !');
         }
         return (objBalise);
      }
      /*
      |----------------------------------------------------------------------------------------|
      | formDYN (08-jan-2012; 07-jan-2013) SOLUTION DE L'EXERCICE 1
      |----------------------------------------------------------------------------------------|
      */
      function formDYN(strID, strIDAttache_objAttache, strMETHOD, strACTION, strCLASS, binVisible) {
         /* Cr�ation d'une instance de la balise */
         var objFORM = document.createElement('form');
         /* Assignation des diff�rents attributs */
         with (objFORM) {
            /* Assignation d'un ID � la balise */
            id = strID;
            /* Assignation de la m�thode, si applicable */
            if (strMETHOD != null && (strMETHOD =='get' || strMETHOD == 'post')) {
               method = strMETHOD;
            }
            /* Assignation de l'action rattach�e, si applicable */
            if (strACTION != null && strACTION != '') {
               action = strACTION;
            }
            /* Assignation d'un style � la balise, si applicable */
            if (strCLASS != null && strCLASS != '') {
               className = strCLASS;
            }
         }
         /* Attachement de la balise enfant � la balise parent */
         attacheBalise(objFORM, strIDAttache_objAttache);
         /* Affichage ou masquage de la balise nouvellement cr��e, si applicable */
         if (binVisible != null) {
            afficheOuMasqueBalise(strID, binVisible);
         }
         /* Retour d'une r�f�rence sur l'instance de la balise */
         return(objFORM);
         } 

	 /*
      |----------------------------------------------------------------------------------------|
      | Ajout des fonctions cr�es dams la serie 2 Vendredi 1 Fervrier 2013
      |----------------------------------------------------------------------------------------|
      */
    function hiddenDYN(strID, strIDAttache_objAttache, strValeur)
	{
		/* Cr�ation d'une instance de la balise */
         var objHIDDEN = document.createElement('input');
         /* Assignation des diff�rents attributs */
         with (objHIDDEN) {
            /* Assignation d'un ID et d'un name  la balise */
            id = strID;
			name = strID;
			
			type = 'hidden';
            /* Assignation de la valeur, si applicable */
            if (strValeur != null) {
               value = strValeur;
            }            
         }
         /* Attachement de la balise enfant a la balise parent */
         attacheBalise(objHIDDEN, strIDAttache_objAttache);         
         /* Retour d'une r�f�rence sur l'instance de la balise */
         return(objHIDDEN);
	}
	
	function divDYN(strID, strIDAttache_objAttache, strCLASS, binVisible)
	{
		/* Cr�ation d'une instance de la balise */
         var objDIV = document.createElement('div');
         /* Assignation des diff�rents attributs */
         with (objDIV) {            
            id = strID;
			if (strCLASS != null) {
				className = strCLASS;
			}
         }
         /* Attachement de la balise enfant a la balise parent */
         attacheBalise(objDIV, strIDAttache_objAttache);    

         if (binVisible != null) {
            afficheOuMasqueBalise(strID, binVisible);
         }		 
         /* Retour d'une r�f�rence sur l'instance de la balise */
         return(objDIV);
	}
	
	function pDYN(strID, strIDAttache_objAttache, strCLASS, binVisible, strContenu)
	{
		 /* Cr�ation d'une instance de la balise */
         var objP = document.createElement('p');
         /* Assignation des diff�rents attributs */
         with (objP) {
            /* Assignation d'un ID � la balise */
            id = strID;
            /* Assignation du contenu, si applicable */
            if (strContenu != null) {
              innerHTML = strContenu;
            }
            /* Assignation du class, si applicable */
            if (strCLASS != null) {
				className = strCLASS;
			}
         }
         /* Attachement de la balise enfant � la balise parent */
         attacheBalise(objP, strIDAttache_objAttache);
         /* Affichage ou masquage de la balise nouvellement cr��e, si applicable */
         if (binVisible != null) {
            afficheOuMasqueBalise(strID, binVisible);
         }
         /* Retour d'une r�f�rence sur l'instance de la balise */
         return(objP);
	}
	
	function brDYN(intNbBR, strIDAttache_objAttache, strCLASS)
	{
		var objBR;
		for(var i = 0; i < intNbBR; i++)
		{
			objBR = document.createElement('br');
			/* Assignation du class, si applicable */
            if (strCLASS != null) {
				objBR.className = strCLASS;
			}
			attacheBalise(objBR, strIDAttache_objAttache);
		}
	}

	function spanDYN(strID, strIDAttache_objAttache, strCLASS, binVisible, strContenu) {
	   /* Cr�ation d'une instance de la balise */
	   var objSPAN = document.createElement('span');
	   /* Assignation des diff�rents attributs */
	   with (objSPAN) {
		  /* Assignation d'un ID � la balise, si applicable */
		  if (strID != '') {
			 id = strID;
		  }
		  /* Assignation d'un style � la balise, si applicable */
		  if (strCLASS != null && strCLASS != '') {
			 className = strCLASS;
		  }
		  /* Assignation d'un contenu, si applicable */
		  if (strContenu != null) {
			 innerHTML = strContenu;
		  }
	   }
	   /* Attachement de la balise enfant � la balise parent */
	   attacheBalise(objSPAN, strIDAttache_objAttache);
	   /* Affichage ou masquage de la balise nouvellement cr��e;
		  Le test varie parce que afficheOuMasqueBalise ne peut �tre appel�e si ID est vide! */
	   if (binVisible != null && strID != '') {
		  afficheOuMasqueBalise(strID, binVisible);
	   }
	   /* Retour d'une r�f�rence sur l'instance de la balise */
	   return(objSPAN);
	}

	function labelDYN(strIDAttache_objAttache, strContenu) {
	   /* Cr�ation d'une instance de la balise */
	   var objLABEL = document.createElement('label');
	   /* Assignation d'un contenu */
	   objLABEL.innerHTML = strContenu;
	   /* Attachement de la balise enfant � la balise parent */
	   attacheBalise(objLABEL, strIDAttache_objAttache);
	   /* Retour d'une r�f�rence sur l'instance de la balise (m�me si ne repr�sente aucun int�r�t !) */
	   return(objLABEL);
	}
	
	function retourneObjet(strID_objBalise) {
	   var objBalise = typeof(strID_objBalise) == 'string' ? 
		  document.getElementById(strID_objBalise) : strID_objBalise;
	   return objBalise;
	}
	
	function selectDYN(strID, strIDAttache_objAttache, strCLASS, onChange, binVisible) {
	   /* Cr�ation d'une instance de la balise */
	   var objSELECT = document.createElement('select');
	   /* Assignation des diff�rents attributs */
	   with (objSELECT) {
		  /* Assignation d'un ID � la balise, si applicable */
		  if (strID != '') {
			 id = strID;
			 name = strID;
		  }
		  /* Assignation d'un style � la balise, si applicable */
		  if (strCLASS != null && strCLASS != '') {
			 className = strCLASS;
		  }
	   }
	   /* Attachement de la balise enfant � la balise parent */
	   attacheBalise(objSELECT, strIDAttache_objAttache);
	   /* Affichage ou masquage de la balise nouvellement cr��e;
		  Le test varie parce que afficheOuMasqueBalise ne peut �tre appel�e si ID est vide! */
	   if (binVisible != null && strID != '') {
		  afficheOuMasqueBalise(strID, binVisible);
	   }	   
	   if (onChange != null)
	   {
			onchange = function() {onChange(objSELECT);};
	   }
	   
	   /* Retour d'une r�f�rence sur l'instance de la balise */
	   return(objSELECT);
	}
	
	function optionDYN(strIDAttache_objAttache, strValue, strContenu) {
	   /* Cr�ation d'une instance de la balise */
	   var objOPTION = document.createElement('option');
	   /* Assignation des diff�rents attributs */
	   
	   with (objOPTION) {		 
		   /* Assignation de la valeur, si applicable */
		  if (strValue != '') {
			 objOPTION.value = strValue;
		  } else alert('Champ value de la balise option sans name!');
		  
		   /* Assignation d'un contenu, si applicable */
		  if (strContenu != null) {
			 objOPTION.text = strContenu;
		  }
	   }
	   
	   /* Attachement de la balise enfant � la balise parent */
	   //attacheBalise(objOPTION, strIDAttache_objAttache);			
		objSELECT = retourneObjet(strIDAttache_objAttache);
		objSELECT.options.add(objOPTION);
	   	  
	   /* Retour d'une r�f�rence sur l'instance de la balise */
	   return(objOPTION);
	}
	
	/*
|----------------------------------------------------------------------------------------|
| buttonDYN (07-jul-2011; 14-jan-2012; 15-f�v-2013)
|----------------------------------------------------------------------------------------|
*/
function buttonDYN(strID, strIDAttache_objAttache, strCLASS, strVALUE, binActif, binVisible, fonctionOnClick) {
   /* Cr�ation d'une instance de la balise */
   var objINPUT = document.createElement('input');
   /* Assignation des diff�rents attributs */
   with (objINPUT) {
      /* Assignation d'un ID et d'un NAME � la balise, si applicable */
      if (strID != '') {
         id = strID;
         name = strID;
      }
      /* D�finition du type de balise INPUT */
      type = 'button';
      /* Assignation d'un style � la balise, si applicable (ne peut �tre null, puisque value est exig�) */
      if (strCLASS != '') {
         className = strCLASS;
      }
      /* Assignation d'une valeur � la balise */
      value = strVALUE;
      /* Activation ou d�sactivation du bouton */
      if (binActif != null) {
         disabled = !binActif;
      }
      /* Assignation d'une fonction pour ONCLICK, ONMOUSEOVER et ONMOUSEOUT */
      if (fonctionOnClick != null) {
         onclick = function() { fonctionOnClick(this); }
      }
   }
   /* Attachement de la balise enfant � la balise parent */
   attacheBalise(objINPUT, strIDAttache_objAttache);
   /* Affichage ou masquage de la balise nouvellement cr��e;
      Le test varie parce que afficheOuMasqueBalise ne peut �tre appel�e si ID est vide! */
   if (binVisible != null && strID != '') {
      afficheOuMasqueBalise(strID, binVisible);
   }
   /* Retour d'une r�f�rence sur l'instance de la balise */
   return(objINPUT);
}

/*
|----------------------------------------------------------------------------------------|
| inputDYN (11-jul-2011; 20-jan-2012; 15-f�v-2013)
|----------------------------------------------------------------------------------------|
*/
function inputDYN(strID, strIDAttache_objAttache, strCLASS, strVALUE, strTYPE, strMAXLENGTH, binActif, intSIZE, binVisible) {
   /* Cr�ation d'une instance de la balise */
   var objINPUT = document.createElement('input');
   /* Assignation des diff�rents attributs */
   with (objINPUT) {
      /* Assignation d'un ID et d'une NAME � la balise */
      id = strID;
      name = strID;
      /* D�finition du type de balise INPUT */
      type = 'text';
      /* Assignation d'un style � la balise, si applicable */
      if (strCLASS != null && strCLASS != '') {
         className = strCLASS;
      }
      /* Assignation d'une valeur � la balise, si applicable */
      if (strVALUE != null && strVALUE != '') {
         value = strVALUE;
      }
      /* Assignation d'une largeur maximum au contenu de la balise, si applicable */
      if (strMAXLENGTH != '') {
         maxLength = strMAXLENGTH;
      }
      /* Activation ou d�sactivation du bouton */
      if (binActif != null) {
         disabled = !binActif;
      }
	  
	  if (intSIZE != null) {
         size = intSIZE;
      }
	  
	   if (strTYPE != null && strTYPE != '') {
         type = strTYPE;
      }
   }
   /* Attachement de la balise enfant � la balise parent */
   attacheBalise(objINPUT, strIDAttache_objAttache);
   /* Affichage ou masquage de la balise nouvellement cr��e;
      Le test varie parce que afficheOuMasqueBalise ne peut �tre appel�e si ID est vide! */
   if (binVisible != null && strID != '') {
      afficheOuMasqueBalise(strID, binVisible);
   }
   /* Retour d'une r�f�rence sur l'instance de la balise */
   return(objINPUT);
}

	function imgDYN(strID, strIDAttache_objAttache, strCLASS, strURL, intHeight, intWidth, binVisible)
	{
		/* Cr�ation d'une instance de la balise */
         var objIMG = document.createElement('img');
         /* Assignation des diff�rents attributs */
         with (objIMG) {            
            id = strID;
			if (strCLASS != null) {
				className = strCLASS;
			}
			src = strURL;
			height = intHeight;
			width = intWidth;
         }
         /* Attachement de la balise enfant a la balise parent */
         attacheBalise(objIMG, strIDAttache_objAttache);    

         if (binVisible != null) {
            afficheOuMasqueBalise(strID, binVisible);
         }		 
         /* Retour d'une r�f�rence sur l'instance de la balise */
         return(objIMG);
	}

function tableDYN(strID, strIDAttache_objAttache, strCLASS, intBORDER, binVisible)
{
	/* Cr�ation d'une instance de la balise */
	 var objTABLE = document.createElement('table');
	 /* Assignation des diff�rents attributs */
	 with (objTABLE) {            
		id = strID;
		if (strCLASS != null) {
			className = strCLASS;
		}
		
		if (intBORDER != null) {
			border = intBORDER;
		}
	 }
	 /* Attachement de la balise enfant a la balise parent */
	 attacheBalise(objTABLE, strIDAttache_objAttache);    

	 if (binVisible != null) {
		afficheOuMasqueBalise(strID, binVisible);
	 }		 
	 /* Retour d'une r�f�rence sur l'instance de la balise */
	 return(objTABLE);
}

function trDYN(strID, strIDAttache_objAttache, strCLASS, binVisible)
	{
		/* Cr�ation d'une instance de la balise */
         var objTR = document.createElement('tr');
         /* Assignation des diff�rents attributs */
         with (objTR) {            
            id = strID;
			if (strCLASS != null) {
				className = strCLASS;
			}
         }
         /* Attachement de la balise enfant a la balise parent */
         attacheBalise(objTR, strIDAttache_objAttache);    

         if (binVisible != null) {
            afficheOuMasqueBalise(strID, binVisible);
         }		 
         /* Retour d'une r�f�rence sur l'instance de la balise */
         return(objTR);
	}
	
	function tdDYN(strID, strIDAttache_objAttache, strCLASS, strContenu, binVisible)
	{
		/* Cr�ation d'une instance de la balise */
         var objTD = document.createElement('td');
         /* Assignation des diff�rents attributs */
         with (objTD) {            
            id = strID;
			if (strCLASS != null) {
				className = strCLASS;
			}
			/* Assignation du contenu, si applicable */
            if (strContenu != null) {
              innerHTML = strContenu;
            }
         }
         /* Attachement de la balise enfant a la balise parent */
         attacheBalise(objTD, strIDAttache_objAttache);    

         if (binVisible != null) {
            afficheOuMasqueBalise(strID, binVisible);
         }		 
         /* Retour d'une r�f�rence sur l'instance de la balise */
         return(objTD);
	}
	
	
/*
|--------------------------------------------------------------------------------------------------------------|
| b (04-ao�-2009; 28-mai-2011)
|--------------------------------------------------------------------------------------------------------------|
| Attribue une valeur ou r�cup�re le contenu actuel d'une balise de type INPUT,
| IMG, P ou SPAN. Validation effectu�e sur la pr�sence ou non de la balise.
|--------------------------------------------------------------------------------------------------------------|
| strIDBalise  : Nom de la balise dont on veut r�cup�rer le contenu ou lui en
|                   attribuer un
| strValeur       : Si pr�sente, valeur � attribuer
|--------------------------------------------------------------------------------------------------------------|
| Fonction requise : Aucune
|--------------------------------------------------------------------------------------------------------------|
*/
function b(strIDBalise,strValeur) {
      var objBalise = document.getElementById(strIDBalise);
      if (!objBalise) {
            alert('Attention... balise ' + strIDBalise + ' inexistante !');
      }
      else {
            if (objBalise.value != undefined) {
                  /* Balise INPUT */
                  if (strValeur != undefined) {
                        objBalise.value = strValeur;
                  }
                  else {
                        return(objBalise.value);
                  }
            }
            else {
                  if (objBalise.src != undefined) {
                        /* Balise IMG */
                        if (strValeur != undefined) {
                              objBalise.src = strValeur;
                        }
                        else {
                              return(objBalise.src);
                        }
                  }
                  else {
                        /* Balise P ou SPAN */
                        if (strValeur != undefined) {
                              objBalise.innerHTML = strValeur;
                        }
                        else {
                              return(objBalise.innerHTML);
                        }
                  }
            }
      }
          }

    /*
    |-------------------------------------------------------------------------------------|
    | afficheOuMasqueInfoVendeur (23-Jan-2014)
    |-------------------------------------------------------------------------------------|
    */
    function afficheOuMasqueInfoVendeur(objBalise) {
        objBalise.nextSibling.nextSibling.style.display = (objBalise.nextSibling.nextSibling.style.display == '' ? 'table-row' : '');
    }

    function afficher_accepter(objBalise) {
        //alert(objBalise.parentNode.parentNode.nextSibling.nextSibling.firstChild.nextSibling.innerHTML);
        //alert(objBalise.parentNode.parentNode.nextSibling.nextSibling.firstChild.nextSibling.style.display);
        objBalise.parentNode.parentNode.nextSibling.nextSibling.firstChild.nextSibling.style.display = 'table-cell';
        objBalise.parentNode.parentNode.style.display = "none";
        //objBalise.parentNode.parentNode.nextSibling.style.display = "none";
    }

    function afficher_refuser(objBalise) {
        objBalise.parentNode.parentNode.nextSibling.nextSibling.nextSibling.nextSibling.firstChild.nextSibling.style.display = 'table-cell';
        objBalise.parentNode.parentNode.style.display = "none";
        //objBalise.parentNode.parentNode.nextSibling.style.display = "none";
    }

    function annuler_acceptation(objBalise) {
        objBalise.parentNode.parentNode.style.display = "none";
        objBalise.parentNode.parentNode.parentNode.previousSibling.previousSibling.style.display = "table-row";
        //objBalise.parentNode.parentNode.nextSibling.style.display = "none";
    }
    function annuler_refus(objBalise) {
        objBalise.parentNode.parentNode.style.display = "none";
        objBalise.parentNode.parentNode.parentNode.previousSibling.previousSibling.previousSibling.previousSibling.style.display = "table-row";
        //objBalise.parentNode.parentNode.nextSibling.style.display = "none";
    }    