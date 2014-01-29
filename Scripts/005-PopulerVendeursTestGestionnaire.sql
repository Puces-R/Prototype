use BD6B8_424R

-- Suppression
DELETE FROM PPDetailsCommandes WHERE NoCommande / 1000 IN (5, 6)
DELETE FROM PPCommandes WHERE NoVendeur / 100 IN (5, 6)
DELETE FROM PPProduits WHERE NoVendeur / 100 IN (5, 6)
DELETE FROM PPVendeurs WHERE NoVendeur / 100 IN (5, 6)

-- Ajout vendeurs

-- Commandes
-- 1 an
INSERT INTO PPVendeurs(NoVendeur, NomAffaires, AdresseEmail, MotDePasse) values
(500, 'Moins 1 an commandes', '500@a.aa', 'a')
INSERT INTO PPVendeurs(NoVendeur, NomAffaires, AdresseEmail, MotDePasse) values
(501, '1 an commande', '501@a.aa', 'a')
INSERT INTO PPVendeurs(NoVendeur, NomAffaires, AdresseEmail, MotDePasse) values
(502, 'Plus 1 an commande', '502@a.aa', 'a')

-- 2 ans
INSERT INTO PPVendeurs(NoVendeur, NomAffaires, AdresseEmail, MotDePasse) values
(510, 'Moins 2 ans commandes', '510@a.aa', 'a')
INSERT INTO PPVendeurs(NoVendeur, NomAffaires, AdresseEmail, MotDePasse) values
(511, '2 ans commande', '511@a.aa', 'a')
INSERT INTO PPVendeurs(NoVendeur, NomAffaires, AdresseEmail, MotDePasse) values
(512, 'Plus 2 ans commande', '512@a.aa', 'a')

-- 3 ans
INSERT INTO PPVendeurs(NoVendeur, NomAffaires, AdresseEmail, MotDePasse) values
(520, 'Moins 3 ans commandes', '520@a.aa', 'a')
INSERT INTO PPVendeurs(NoVendeur, NomAffaires, AdresseEmail, MotDePasse) values
(521, '3 ans commande', '521@a.aa', 'a')
INSERT INTO PPVendeurs(NoVendeur, NomAffaires, AdresseEmail, MotDePasse) values
(522, 'Plus 3 ans commande', '522@a.aa', 'a')

-- Produits
-- 1 an
INSERT INTO PPVendeurs(NoVendeur, NomAffaires, AdresseEmail, MotDePasse) values
(600, 'Moins 1 an produits', '600@a.aa', 'a')
INSERT INTO PPVendeurs(NoVendeur, NomAffaires, AdresseEmail, MotDePasse) values
(601, '1 an produit', '601@a.aa', 'a')
INSERT INTO PPVendeurs(NoVendeur, NomAffaires, AdresseEmail, MotDePasse) values
(602, 'Plus 1 an produit', '602@a.aa', 'a')

-- 2 ans
INSERT INTO PPVendeurs(NoVendeur, NomAffaires, AdresseEmail, MotDePasse) values
(610, 'Moins 2 ans produits', '610@a.aa', 'a')
INSERT INTO PPVendeurs(NoVendeur, NomAffaires, AdresseEmail, MotDePasse) values
(611, '2 ans produit', '611@a.aa', 'a')
INSERT INTO PPVendeurs(NoVendeur, NomAffaires, AdresseEmail, MotDePasse) values
(612, 'Plus 2 ans produit', '612@a.aa', 'a')

-- 3 ans
INSERT INTO PPVendeurs(NoVendeur, NomAffaires, AdresseEmail, MotDePasse) values
(620, 'Moins 3 ans produits', '620@a.aa', 'a')
INSERT INTO PPVendeurs(NoVendeur, NomAffaires, AdresseEmail, MotDePasse) values
(621, '3 ans produit', '621@a.aa', 'a')
INSERT INTO PPVendeurs(NoVendeur, NomAffaires, AdresseEmail, MotDePasse) values
(622, 'Plus 3 ans produit', '622@a.aa', 'a')

-- Ajouts commandes

-- Commandes
-- 1 an
INSERT INTO PPCommandes values
(5000, 10000, 500, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(5001, 10000, 500, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(5002, 10000, 500, DATEADD(d, 1, DATEADD(yy, -1, GETDATE())), 1, 1, 1, 1, 1, 1, 'I', 1)

INSERT INTO PPCommandes values
(5010, 10000, 501, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(5011, 10000, 501, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(5012, 10000, 501, DATEADD(yy, -1, GETDATE()), 1, 1, 1, 1, 1, 1, 'I', 1)

INSERT INTO PPCommandes values
(5020, 10000, 502, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(5021, 10000, 502, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(5022, 10000, 502, DATEADD(d, -1, DATEADD(yy, -1, GETDATE())), 1, 1, 1, 1, 1, 1, 'I', 1)

-- 2 ans
INSERT INTO PPCommandes values
(5100, 10000, 510, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(5101, 10000, 510, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(5102, 10000, 510, DATEADD(d, 1, DATEADD(yy, -2, GETDATE())), 1, 1, 1, 1, 1, 1, 'I', 1)

INSERT INTO PPCommandes values
(5110, 10000, 511, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(5111, 10000, 511, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(5112, 10000, 511, DATEADD(yy, -2, GETDATE()), 1, 1, 1, 1, 1, 1, 'I', 1)

INSERT INTO PPCommandes values
(5120, 10000, 512, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(5121, 10000, 512, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(5122, 10000, 512, DATEADD(d, -1, DATEADD(yy, -2, GETDATE())), 1, 1, 1, 1, 1, 1, 'I', 1)

-- 3 ans
INSERT INTO PPCommandes values
(5200, 10000, 520, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(5201, 10000, 520, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(5202, 10000, 520, DATEADD(d, 1, DATEADD(yy, -3, GETDATE())), 1, 1, 1, 1, 1, 1, 'I', 1)

INSERT INTO PPCommandes values
(5210, 10000, 521, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(5211, 10000, 521, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(5212, 10000, 521, DATEADD(yy, -3, GETDATE()), 1, 1, 1, 1, 1, 1, 'I', 1)

INSERT INTO PPCommandes values
(5220, 10000, 522, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(5221, 10000, 522, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(5222, 10000, 522, DATEADD(d, -1, DATEADD(yy, -3, GETDATE())), 1, 1, 1, 1, 1, 1, 'I', 1)

-- Produits
-- 1 an
INSERT INTO PPCommandes values
(6000, 10000, 600, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(6001, 10000, 600, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(6002, 10000, 600, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)

INSERT INTO PPCommandes values
(6010, 10000, 601, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(6011, 10000, 601, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(6012, 10000, 601, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)

INSERT INTO PPCommandes values
(6020, 10000, 602, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(6021, 10000, 602, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(6022, 10000, 602, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)

-- 2 ans
INSERT INTO PPCommandes values
(6100, 10000, 610, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(6101, 10000, 610, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(6102, 10000, 610, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)

INSERT INTO PPCommandes values
(6110, 10000, 611, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(6111, 10000, 611, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(6112, 10000, 611, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)

INSERT INTO PPCommandes values
(6120, 10000, 612, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(6121, 10000, 612, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(6122, 10000, 612, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)

-- 3 ans
INSERT INTO PPCommandes values
(6200, 10000, 620, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(6201, 10000, 620, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(6202, 10000, 620, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)

INSERT INTO PPCommandes values
(6210, 10000, 621, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(6211, 10000, 621, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(6212, 10000, 621, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)

INSERT INTO PPCommandes values
(6220, 10000, 622, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(6221, 10000, 622, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)
INSERT INTO PPCommandes values
(6222, 10000, 622, '2000-01-01', 1, 1, 1, 1, 1, 1, 'I', 1)

-- Ajouts produits

-- Commande
-- 1 an
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(50000001, 500, 10, 'Produit 500-1', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(50000002, 500, 10, 'Produit 500-2', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(50000003, 500, 10, 'Produit 500-3', '', 1, 0, 0, 0, '2000-01-01')

INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(50100001, 501, 10, 'Produit 501-1', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(50100002, 501, 10, 'Produit 501-2', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(50100003, 501, 10, 'Produit 501-3', '', 1, 0, 0, 0, '2000-01-01')

INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(50200001, 502, 10, 'Produit 502-1', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(50200002, 502, 10, 'Produit 502-2', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(50200003, 502, 10, 'Produit 502-3', '', 1, 0, 0, 0, '2000-01-01')

-- 2 ans
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(51000001, 510, 10, 'Produit 510-1', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(51000002, 510, 10, 'Produit 510-2', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(51000003, 510, 10, 'Produit 510-3', '', 1, 0, 0, 0, '2000-01-01')

INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(51100001, 511, 10, 'Produit 511-1', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(51100002, 511, 10, 'Produit 511-2', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(51100003, 511, 10, 'Produit 511-3', '', 1, 0, 0, 0, '2000-01-01')

INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(51200001, 512, 10, 'Produit 512-1', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(51200002, 512, 10, 'Produit 512-2', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(51200003, 512, 10, 'Produit 512-3', '', 1, 0, 0, 0, '2000-01-01')

-- 3 ans
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(52000001, 520, 10, 'Produit 520-1', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(52000002, 520, 10, 'Produit 520-2', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(52000003, 520, 10, 'Produit 520-3', '', 1, 0, 0, 0, '2000-01-01')

INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(52100001, 521, 10, 'Produit 521-1', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(52100002, 521, 10, 'Produit 521-2', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(52100003, 521, 10, 'Produit 521-3', '', 1, 0, 0, 0, '2000-01-01')

INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(52200001, 522, 10, 'Produit 522-1', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(52200002, 522, 10, 'Produit 522-2', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(52200003, 522, 10, 'Produit 522-3', '', 1, 0, 0, 0, '2000-01-01')

-- Commandes
-- 1 an
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(60000001, 600, 10, 'Produit 600-1', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(60000002, 600, 10, 'Produit 600-2', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(60000003, 600, 10, 'Produit 600-3', '', 1, 0, 0, 0, DATEADD(d, 1, DATEADD(yy, -1, GETDATE())))

INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(60100001, 601, 10, 'Produit 601-1', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(60100002, 601, 10, 'Produit 601-2', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(60100003, 601, 10, 'Produit 601-3', '', 1, 0, 0, 0, DATEADD(yy, -1, GETDATE()))

INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(60200001, 602, 10, 'Produit 602-1', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(60200002, 602, 10, 'Produit 602-2', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(60200003, 602, 10, 'Produit 602-3', '', 1, 0, 0, 0, DATEADD(d, -1, DATEADD(yy, -1, GETDATE())))

-- 2 ans
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(61000001, 610, 10, 'Produit 610-1', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(61000002, 610, 10, 'Produit 610-2', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(61000003, 610, 10, 'Produit 610-3', '', 1, 0, 0, 0, DATEADD(d, 1, DATEADD(yy, -2, GETDATE())))

INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(61100001, 611, 10, 'Produit 611-1', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(61100002, 611, 10, 'Produit 611-2', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(61100003, 611, 10, 'Produit 611-3', '', 1, 0, 0, 0, DATEADD(yy, -2, GETDATE()))

INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(61200001, 612, 10, 'Produit 612-1', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(61200002, 612, 10, 'Produit 612-2', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(61200003, 612, 10, 'Produit 612-3', '', 1, 0, 0, 0, DATEADD(d, -1, DATEADD(yy, -2, GETDATE())))

-- 3 ans
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(62000001, 620, 10, 'Produit 620-1', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(62000002, 620, 10, 'Produit 620-2', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(62000003, 620, 10, 'Produit 620-3', '', 1, 0, 0, 0, DATEADD(d, 1, DATEADD(yy, -3, GETDATE())))

INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(62100001, 621, 10, 'Produit 621-1', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(62100002, 621, 10, 'Produit 621-2', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(62100003, 621, 10, 'Produit 621-3', '', 1, 0, 0, 0, DATEADD(yy, -3, GETDATE()))

INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(62200001, 622, 10, 'Produit 622-1', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(62200002, 622, 10, 'Produit 622-2', '', 1, 0, 0, 0, '2000-01-01')
INSERT INTO PPProduits(NoProduit, NoVendeur, NoCategorie, Nom, "Description", PrixDemande, NombreItems, Disponibilité, Poids, DateCreation) values
(62200003, 622, 10, 'Produit 622-3', '', 1, 0, 0, 0, DATEADD(d, -1, DATEADD(yy, -3, GETDATE())))

-- Ajouts details commande

-- Commandes

-- 1 an
INSERT INTO PPDetailsCommandes values
(5000, 5000, 50000001, 1, 1)
INSERT INTO PPDetailsCommandes values
(5001, 5001, 50000001, 1, 1)
INSERT INTO PPDetailsCommandes values
(5002, 5002, 50000001, 1, 1)

INSERT INTO PPDetailsCommandes values
(5010, 5010, 50100001, 1, 1)
INSERT INTO PPDetailsCommandes values
(5011, 5011, 50100001, 1, 1)
INSERT INTO PPDetailsCommandes values
(5012, 5012, 50100001, 1, 1)

INSERT INTO PPDetailsCommandes values
(5020, 5020, 50200001, 1, 1)
INSERT INTO PPDetailsCommandes values
(5021, 5021, 50200001, 1, 1)
INSERT INTO PPDetailsCommandes values
(5022, 5022, 50200001, 1, 1)

-- 2 ans
INSERT INTO PPDetailsCommandes values
(5100, 5100, 51000001, 1, 1)
INSERT INTO PPDetailsCommandes values
(5101, 5101, 51000001, 1, 1)
INSERT INTO PPDetailsCommandes values
(5102, 5102, 51000001, 1, 1)

INSERT INTO PPDetailsCommandes values
(5110, 5110, 51100001, 1, 1)
INSERT INTO PPDetailsCommandes values
(5111, 5111, 51100001, 1, 1)
INSERT INTO PPDetailsCommandes values
(5112, 5112, 51100001, 1, 1)

INSERT INTO PPDetailsCommandes values
(5120, 5120, 51200001, 1, 1)
INSERT INTO PPDetailsCommandes values
(5121, 5121, 51200001, 1, 1)
INSERT INTO PPDetailsCommandes values
(5122, 5122, 51200001, 1, 1)

-- 3 ans
INSERT INTO PPDetailsCommandes values
(5200, 5200, 52000001, 1, 1)
INSERT INTO PPDetailsCommandes values
(5201, 5201, 52000001, 1, 1)
INSERT INTO PPDetailsCommandes values
(5202, 5202, 52000001, 1, 1)

INSERT INTO PPDetailsCommandes values
(5210, 5210, 52100001, 1, 1)
INSERT INTO PPDetailsCommandes values
(5211, 5211, 52100001, 1, 1)
INSERT INTO PPDetailsCommandes values
(5212, 5212, 52100001, 1, 1)

INSERT INTO PPDetailsCommandes values
(5220, 5220, 52200001, 1, 1)
INSERT INTO PPDetailsCommandes values
(5221, 5221, 52200001, 1, 1)
INSERT INTO PPDetailsCommandes values
(5222, 5222, 52200001, 1, 1)

-- Produits
-- 1 an
INSERT INTO PPDetailsCommandes values
(6000, 6000, 60000001, 1, 1)
INSERT INTO PPDetailsCommandes values
(6001, 6001, 60000001, 1, 1)
INSERT INTO PPDetailsCommandes values
(6002, 6002, 60000001, 1, 1)

INSERT INTO PPDetailsCommandes values
(6010, 6010, 60100001, 1, 1)
INSERT INTO PPDetailsCommandes values
(6011, 6011, 60100001, 1, 1)
INSERT INTO PPDetailsCommandes values
(6012, 6012, 60100001, 1, 1)

INSERT INTO PPDetailsCommandes values
(6020, 6020, 60200001, 1, 1)
INSERT INTO PPDetailsCommandes values
(6021, 6021, 60200001, 1, 1)
INSERT INTO PPDetailsCommandes values
(6022, 6022, 60200001, 1, 1)

-- 2 ans
INSERT INTO PPDetailsCommandes values
(6100, 6100, 61000001, 1, 1)
INSERT INTO PPDetailsCommandes values
(6101, 6101, 61000001, 1, 1)
INSERT INTO PPDetailsCommandes values
(6102, 6102, 61000001, 1, 1)

INSERT INTO PPDetailsCommandes values
(6110, 6110, 61100001, 1, 1)
INSERT INTO PPDetailsCommandes values
(6111, 6111, 61100001, 1, 1)
INSERT INTO PPDetailsCommandes values
(6112, 6112, 61100001, 1, 1)

INSERT INTO PPDetailsCommandes values
(6120, 6120, 61200001, 1, 1)
INSERT INTO PPDetailsCommandes values
(6121, 6121, 61200001, 1, 1)
INSERT INTO PPDetailsCommandes values
(6122, 6122, 61200001, 1, 1)

-- 3 ans
INSERT INTO PPDetailsCommandes values
(6200, 6200, 62000001, 1, 1)
INSERT INTO PPDetailsCommandes values
(6201, 6201, 62000001, 1, 1)
INSERT INTO PPDetailsCommandes values
(6202, 6202, 62000001, 1, 1)

INSERT INTO PPDetailsCommandes values
(6210, 6210, 62100001, 1, 1)
INSERT INTO PPDetailsCommandes values
(6211, 6211, 62100001, 1, 1)
INSERT INTO PPDetailsCommandes values
(6212, 6212, 62100001, 1, 1)

INSERT INTO PPDetailsCommandes values
(6220, 6220, 62200001, 1, 1)
INSERT INTO PPDetailsCommandes values
(6221, 6221, 62200001, 1, 1)
INSERT INTO PPDetailsCommandes values
(6222, 6222, 62200001, 1, 1)