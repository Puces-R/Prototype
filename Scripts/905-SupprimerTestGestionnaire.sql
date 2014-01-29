use BD6B8_424R

DELETE FROM PPDetailsCommandes WHERE NoCommande / 1000 IN (2, 3, 5, 6)
DELETE FROM PPVendeursClients WHERE NoClient / 10000 IN (2, 3) 
DELETE FROM PPArticlesEnPanier WHERE NoClient / 10000 IN (2, 3)
DELETE FROM PPCommandes WHERE NoClient / 10000 IN (2, 3)
DELETE FROM PPClients WHERE NoClient / 10000 IN (2, 3)
DELETE FROM PPCommandes WHERE NoVendeur / 100 IN (5, 6)
DELETE FROM PPProduits WHERE NoVendeur / 100 IN (5, 6)
DELETE FROM PPVendeurs WHERE NoVendeur / 100 IN (5, 6)