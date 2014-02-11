use BD6B8_424R

/* Copie des tables et des données*/

SELECT * INTO PPArticlesEnPanier FROM BD6B8PetitesPucesH2014Effective.dbo.PPArticlesEnPanier
SELECT * INTO PPCategories FROM BD6B8PetitesPucesH2014Effective.dbo.PPCategories
SELECT * INTO PPClients FROM BD6B8PetitesPucesH2014Effective.dbo.PPClients
SELECT * INTO PPCommandes FROM BD6B8PetitesPucesH2014Effective.dbo.PPCommandes
SELECT * INTO PPDetailsCommandes FROM BD6B8PetitesPucesH2014Effective.dbo.PPDetailsCommandes
SELECT * INTO PPHistoriquePaiements FROM BD6B8PetitesPucesH2014Effective.dbo.PPHistoriquePaiements
SELECT * INTO PPPoidsLivraisons FROM BD6B8PetitesPucesH2014Effective.dbo.PPPoidsLivraisons
SELECT * INTO PPProduits FROM BD6B8PetitesPucesH2014Effective.dbo.PPProduits
SELECT * INTO PPTaxeFederale FROM BD6B8PetitesPucesH2014Effective.dbo.PPTaxeFederale
SELECT * INTO PPTaxeProvinciale FROM BD6B8PetitesPucesH2014Effective.dbo.PPTaxeProvinciale
SELECT * INTO PPTypesLivraison FROM BD6B8PetitesPucesH2014Effective.dbo.PPTypesLivraison
SELECT * INTO PPTypesPoids FROM BD6B8PetitesPucesH2014Effective.dbo.PPTypesPoids
SELECT * INTO PPVendeurs FROM BD6B8PetitesPucesH2014Effective.dbo.PPVendeurs
SELECT * INTO PPVendeursClients FROM BD6B8PetitesPucesH2014Effective.dbo.PPVendeursClients


/* Création des clefs primaires */

ALTER TABLE PPArticlesEnPanier ADD PRIMARY KEY(NoPanier)
ALTER TABLE PPCategories ADD PRIMARY KEY(NoCategorie)
ALTER TABLE PPClients ADD PRIMARY KEY(NoClient)
ALTER TABLE PPCommandes ADD PRIMARY KEY(NoCommande)
ALTER TABLE PPDetailsCommandes ADD PRIMARY KEY(NoDetailCommandes)
ALTER TABLE PPHistoriquePaiements ADD PRIMARY KEY(NoHistorique)
ALTER TABLE PPPoidsLivraisons ADD PRIMARY KEY(CodeLivraison, CodePoids)
ALTER TABLE PPProduits ADD PRIMARY KEY(NoProduit)
ALTER TABLE PPTaxeFederale ADD PRIMARY KEY(NoTPS)
ALTER TABLE PPTaxeProvinciale ADD PRIMARY KEY(NoTVQ)
ALTER TABLE PPTypesLivraison ADD PRIMARY KEY(CodeLivraison)
ALTER TABLE PPTypesPoids ADD PRIMARY KEY(CodePoids)
ALTER TABLE PPVendeurs ADD PRIMARY KEY(NoVendeur)
ALTER TABLE PPVendeursClients ADD PRIMARY KEY(NoVendeur, NoClient, DateVisite)

/* Créations des tables personnelles */

CREATE TABLE PPGestionnaires (
	NoGestionnaire bigint PRIMARY KEY,
	AdresseEmail varchar(100) NOT NULL,
	MotDePasse varchar(50) NOT NULL,
	Nom varchar(50),
	Prenom varchar(50),
	Rue varchar(50),
	Ville varchar(50),
	Province char(2),
	CodePostal varchar(7),
	Pays varchar(10),
	Tel1 varchar(20),
	Tel2 varchar(20),
	DateCreation smalldatetime,
	DateMAJ smalldatetime,
	NbConnexions smallint,
	DateDerniereConnexion smalldatetime
)

CREATE TABLE PPBoites (
	NoBoite smallint PRIMARY KEY,
	"Description" varchar(20) NOT NULL
)

CREATE TABLE PPCategoriesMessage (
	NoCategorie smallint PRIMARY KEY,
	"Description" varchar(20) NOT NULL,
	Couleur int NOT NULL CHECK (Couleur BETWEEN CONVERT(INT, 0x000000) AND CONVERT(INT, 0xFFFFFF))
)

CREATE TABLE PPMessages (
	NoMessage bigint PRIMARY KEY,
	NoExpediteur bigint NOT NULL,
	DateEnvoi datetime NOT NULL,
	Sujet varchar(50) NOT NULL,
	Contenu varchar(MAX) NOT NULL,
	FichierJoint varchar(50),
	Boite smallint NOT NULL FOREIGN KEY REFERENCES PPBoites(NoBoite),
	Categorie smallint NULL FOREIGN KEY REFERENCES PPCategoriesMessage(NoCategorie)
)

CREATE TABLE PPDestinatairesMessages (
	NoDestinataire bigint,
	NoMessage bigint FOREIGN KEY REFERENCES PPMessages(NoMessage),
	Lu bit NOT NULL,
	Boite smallint NOT NULL FOREIGN KEY REFERENCES PPBoites(NoBoite),
	Categorie smallint NULL FOREIGN KEY REFERENCES PPCategoriesMessage(NoCategorie), 
	CONSTRAINT PK_DestinatairesMessages PRIMARY KEY (NoDestinataire, NoMessage)
)

CREATE TABLE PPSuiviCompta (
	NoVendeur bigint,
	Mois smalldatetime,
	Montant smallmoney NOT NULL,
	DatePaiement smalldatetime,
	CONSTRAINT PK_SuiviCompta PRIMARY KEY (NoVendeur, Mois)
)

/* Création des clefs étrangères */

ALTER TABLE PPArticlesEnPanier ADD FOREIGN KEY(NoClient) REFERENCES PPClients(NoClient)
ALTER TABLE PPArticlesEnPanier ADD FOREIGN KEY(NoVendeur) REFERENCES PPVendeurs(NoVendeur)
ALTER TABLE PPArticlesEnPanier ADD FOREIGN KEY(NoProduit) REFERENCES PPProduits(NoProduit)

ALTER TABLE PPCommandes ADD FOREIGN KEY(NoClient) REFERENCES PPClients(NoClient)
ALTER TABLE PPCommandes ADD FOREIGN KEY(NoVendeur) REFERENCES PPVendeurs(NoVendeur)
ALTER TABLE PPCommandes ADD FOREIGN KEY(TypeLivraison) REFERENCES PPTypesLivraison(CodeLivraison)

ALTER TABLE PPDetailsCommandes ADD FOREIGN KEY(NoCommande) REFERENCES PPCommandes(NoCommande)
ALTER TABLE PPDetailsCommandes ADD FOREIGN KEY(NoProduit) REFERENCES PPProduits(NoProduit)

ALTER TABLE PPPoidsLivraisons ADD FOREIGN KEY(CodeLivraison) REFERENCES PPTypesLivraison(CodeLivraison)
ALTER TABLE PPPoidsLivraisons ADD FOREIGN KEY(CodePoids) REFERENCES PPTypesPoids(CodePoids)

ALTER TABLE PPProduits ADD FOREIGN KEY(NoVendeur) REFERENCES PPVendeurs(NoVendeur)
ALTER TABLE PPProduits ADD FOREIGN KEY(NoCategorie) REFERENCES PPCategories(NoCategorie)

ALTER TABLE PPVendeursClients ADD FOREIGN KEY(NoVendeur) REFERENCES PPVendeurs(NoVendeur)
ALTER TABLE PPVendeursClients ADD FOREIGN KEY(NoClient) REFERENCES PPClients(NoClient)

/* Ajout de vérification des données dans la BD

-- À faire (Je suis paresseux)