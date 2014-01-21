/* Copie des tables et des données*/

SELECT * INTO BD6B8_424R.dbo.PPArticlesEnPanier FROM BD6B8PetitesPucesH2014Effective.dbo.PPArticlesEnPanier
SELECT * INTO BD6B8_424R.dbo.PPCategories FROM BD6B8PetitesPucesH2014Effective.dbo.PPCategories
SELECT * INTO BD6B8_424R.dbo.PPClients FROM BD6B8PetitesPucesH2014Effective.dbo.PPClients
SELECT * INTO BD6B8_424R.dbo.PPCommandes FROM BD6B8PetitesPucesH2014Effective.dbo.PPCommandes
SELECT * INTO BD6B8_424R.dbo.PPDetailsCommandes FROM BD6B8PetitesPucesH2014Effective.dbo.PPDetailsCommandes
SELECT * INTO BD6B8_424R.dbo.PPHistoriquePaiements FROM BD6B8PetitesPucesH2014Effective.dbo.PPHistoriquePaiements
SELECT * INTO BD6B8_424R.dbo.PPPoidsLivraisons FROM BD6B8PetitesPucesH2014Effective.dbo.PPPoidsLivraisons
SELECT * INTO BD6B8_424R.dbo.PPProduits FROM BD6B8PetitesPucesH2014Effective.dbo.PPProduits
SELECT * INTO BD6B8_424R.dbo.PPTaxeFederale FROM BD6B8PetitesPucesH2014Effective.dbo.PPTaxeFederale
SELECT * INTO BD6B8_424R.dbo.PPTaxeProvinciale FROM BD6B8PetitesPucesH2014Effective.dbo.PPTaxeProvinciale
SELECT * INTO BD6B8_424R.dbo.PPTypesLivraison FROM BD6B8PetitesPucesH2014Effective.dbo.PPTypesLivraison
SELECT * INTO BD6B8_424R.dbo.PPTypesPoids FROM BD6B8PetitesPucesH2014Effective.dbo.PPTypesPoids
SELECT * INTO BD6B8_424R.dbo.PPVendeurs FROM BD6B8PetitesPucesH2014Effective.dbo.PPVendeurs
SELECT * INTO BD6B8_424R.dbo.PPVendeursClients FROM BD6B8PetitesPucesH2014Effective.dbo.PPVendeursClients


/* Création des clefs primaires */

ALTER TABLE BD6B8_424R.dbo.PPArticlesEnPanier ADD PRIMARY KEY(NoPanier)
ALTER TABLE BD6B8_424R.dbo.PPCategories ADD PRIMARY KEY(NoCategorie)
ALTER TABLE BD6B8_424R.dbo.PPClients ADD PRIMARY KEY(NoClient)
ALTER TABLE BD6B8_424R.dbo.PPCommandes ADD PRIMARY KEY(NoCommande)
ALTER TABLE BD6B8_424R.dbo.PPDetailsCommandes ADD PRIMARY KEY(NoDetailCommandes)
ALTER TABLE BD6B8_424R.dbo.PPHistoriquePaiements ADD PRIMARY KEY(NoHistorique)
ALTER TABLE BD6B8_424R.dbo.PPPoidsLivraisons ADD PRIMARY KEY(CodeLivraison, CodePoids)
ALTER TABLE BD6B8_424R.dbo.PPProduits ADD PRIMARY KEY(NoProduit)
ALTER TABLE BD6B8_424R.dbo.PPTaxeFederale ADD PRIMARY KEY(NoTPS)
ALTER TABLE BD6B8_424R.dbo.PPTaxeProvinciale ADD PRIMARY KEY(NoTVQ)
ALTER TABLE BD6B8_424R.dbo.PPTypesLivraison ADD PRIMARY KEY(CodeLivraison)
ALTER TABLE BD6B8_424R.dbo.PPTypesPoids ADD PRIMARY KEY(CodePoids)
ALTER TABLE BD6B8_424R.dbo.PPVendeurs ADD PRIMARY KEY(NoVendeur)
ALTER TABLE BD6B8_424R.dbo.PPVendeursClients ADD PRIMARY KEY(NoVendeur, NoClient, DateVisite)

/* Création des clefs étrangères */

ALTER TABLE BD6B8_424R.dbo.PPArticlesEnPanier ADD FOREIGN KEY(NoClient) REFERENCES BD6B8_424R.dbo.PPClients(NoClient)
ALTER TABLE BD6B8_424R.dbo.PPArticlesEnPanier ADD FOREIGN KEY(NoVendeur) REFERENCES BD6B8_424R.dbo.PPVendeurs(NoVendeur)
ALTER TABLE BD6B8_424R.dbo.PPArticlesEnPanier ADD FOREIGN KEY(NoProduit) REFERENCES BD6B8_424R.dbo.PPProduits(NoProduit)

ALTER TABLE BD6B8_424R.dbo.PPCommandes ADD FOREIGN KEY(NoClient) REFERENCES BD6B8_424R.dbo.PPClients(NoClient)
ALTER TABLE BD6B8_424R.dbo.PPCommandes ADD FOREIGN KEY(NoVendeur) REFERENCES BD6B8_424R.dbo.PPVendeurs(NoVendeur)
ALTER TABLE BD6B8_424R.dbo.PPCommandes ADD FOREIGN KEY(TypeLivraison) REFERENCES BD6B8_424R.dbo.PPTypesLivraison(CodeLivraison)

ALTER TABLE BD6B8_424R.dbo.PPDetailsCommandes ADD FOREIGN KEY(NoCommande) REFERENCES BD6B8_424R.dbo.PPCommandes(NoCommande)
ALTER TABLE BD6B8_424R.dbo.PPDetailsCommandes ADD FOREIGN KEY(NoProduit) REFERENCES BD6B8_424R.dbo.PPProduits(NoProduit)

-- À vérifier
ALTER TABLE BD6B8_424R.dbo.PPHistoriquePaiements ADD FOREIGN KEY(NoVendeur) REFERENCES BD6B8_424R.dbo.PPVendeurs(NoVendeur)
ALTER TABLE BD6B8_424R.dbo.PPHistoriquePaiements ADD FOREIGN KEY(NoClient) REFERENCES BD6B8_424R.dbo.PPClients(NoClient)
ALTER TABLE BD6B8_424R.dbo.PPHistoriquePaiements ADD FOREIGN KEY(NoCommande) REFERENCES BD6B8_424R.dbo.PPCommandes(NoCommande)

ALTER TABLE BD6B8_424R.dbo.PPPoidsLivraisons ADD FOREIGN KEY(CodeLivraison) REFERENCES BD6B8_424R.dbo.PPTypesLivraison(CodeLivraison)
ALTER TABLE BD6B8_424R.dbo.PPPoidsLivraisons ADD FOREIGN KEY(CodePoids) REFERENCES BD6B8_424R.dbo.PPTypesPoids(CodePoids)

ALTER TABLE BD6B8_424R.dbo.PPProduits ADD FOREIGN KEY(NoVendeur) REFERENCES BD6B8_424R.dbo.PPVendeurs(NoVendeur)
ALTER TABLE BD6B8_424R.dbo.PPProduits ADD FOREIGN KEY(NoCategorie) REFERENCES BD6B8_424R.dbo.PPCategories(NoCategorie)

ALTER TABLE BD6B8_424R.dbo.PPVendeursClients ADD FOREIGN KEY(NoVendeur) REFERENCES BD6B8_424R.dbo.PPVendeurs(NoVendeur)
ALTER TABLE BD6B8_424R.dbo.PPVendeursClients ADD FOREIGN KEY(NoClient) REFERENCES BD6B8_424R.dbo.PPClients(NoClient)