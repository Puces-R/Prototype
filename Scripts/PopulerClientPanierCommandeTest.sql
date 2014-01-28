use BD6B8_424R

-- Suppression

DELETE FROM PPDetailsCommandes WHERE NoCommande / 1000 IN (2, 3)
DELETE FROM PPVendeursClients WHERE NoClient / 10000 IN (2, 3) 
DELETE FROM PPArticlesEnPanier WHERE NoClient / 10000 IN (2, 3)
DELETE FROM PPCommandes WHERE NoClient / 10000 IN (2, 3)
DELETE FROM PPClients WHERE NoClient / 10000 IN (2, 3)

-- Création des clients
-- Tests pour Paniers

INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20000, 'moins_1_mois_panier@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20001, '1_mois_panier@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20002, 'plus_1_mois_panier@a.aa', 'a')

INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20010, 'moins_3_mois_panier@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20011, '3_mois_panier@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20012, 'plus_3_mois_panier@a.aa', 'a')

INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20020, 'moins_6_mois_panier@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20021, '6_mois_panier@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20022, 'plus_6_mois_panier@a.aa', 'a')

INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20030, 'moins_12_mois_panier@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20031, '12_mois_panier@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20032, 'plus_12_mois_panier@a.aa', 'a')

INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20040, 'moins_1_an_panier@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20041, '1_an_panier@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20042, 'plus_1_an_panier@a.aa', 'a')

INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20050, 'moins_2_ans_panier@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20051, '2_ans_panier@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20052, 'plus_2_ans_panier@a.aa', 'a')

INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20060, 'moins_3_ans_panier@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20061, '3_ans_panier@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (20062, 'plus_3_ans_panier@a.aa', 'a')


-- Test pour Commandes

INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30000, 'moins_1_mois_commande@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30001, '1_mois_commande@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30002, 'plus_1_mois_commande@a.aa', 'a')

INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30010, 'moins_3_mois_commande@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30011, '3_mois_commande@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30012, 'plus_3_mois_commande@a.aa', 'a')

INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30020, 'moins_6_mois_commande@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30021, '6_mois_commande@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30022, 'plus_6_mois_commande@a.aa', 'a')

INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30030, 'moins_12_mois_commande@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30031, '12_mois_commande@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30032, 'plus_12_mois_commande@a.aa', 'a')

INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30040, 'moins_1_an_commande@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30041, '1_an_commande@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30042, 'plus_1_an_commande@a.aa', 'a')

INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30050, 'moins_2_ans_commande@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30051, '2_ans_commande@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30052, 'plus_2_ans_commande@a.aa', 'a')

INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30060, 'moins_3_ans_commande@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30061, '3_ans_commande@a.aa', 'a')
INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse)
values (30062, 'plus_3_ans_commande@a.aa', 'a')

-- Création Paniers

-- Vérifications panier
-- 1 mois
INSERT INTO PPArticlesEnPanier values
(200001000010, 20000, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200001000020, 20000, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200001000030, 20000, 10, 1000030, DATEADD(d, 1, DATEADD(m, -1, GETDATE())), 1)

INSERT INTO PPArticlesEnPanier values
(200011000010, 20001, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200011000020, 20001, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200011000030, 20001, 10, 1000030, DATEADD(m, -1, GETDATE()), 1)

INSERT INTO PPArticlesEnPanier values
(200021000010, 20002, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200021000020, 20002, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200021000030, 20002, 10, 1000030, DATEADD(d, -1, DATEADD(m, -1, GETDATE())), 1)

-- 3 mois
INSERT INTO PPArticlesEnPanier values
(200101000010, 20010, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200101000020, 20010, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200101000030, 20010, 10, 1000030, DATEADD(d, 1, DATEADD(m, -3, GETDATE())), 1)

INSERT INTO PPArticlesEnPanier values
(200111000010, 20011, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200111000020, 20011, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200111000030, 20011, 10, 1000030, DATEADD(m, -3, GETDATE()), 1)

INSERT INTO PPArticlesEnPanier values
(200121000010, 20012, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200121000020, 20012, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200121000030, 20012, 10, 1000030, DATEADD(d, -1, DATEADD(m, -3, GETDATE())), 1)

-- 6 mois
INSERT INTO PPArticlesEnPanier values
(200201000010, 20020, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200201000020, 20020, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200201000030, 20020, 10, 1000030, DATEADD(d, 1, DATEADD(m, -6, GETDATE())), 1)

INSERT INTO PPArticlesEnPanier values
(200211000010, 20021, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200211000020, 20021, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200211000030, 20021, 10, 1000030, DATEADD(m, -6, GETDATE()), 1)

INSERT INTO PPArticlesEnPanier values
(200221000010, 20022, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200221000020, 20022, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200221000030, 20022, 10, 1000030, DATEADD(d, -1, DATEADD(m, -6, GETDATE())), 1)

-- 12 mois
INSERT INTO PPArticlesEnPanier values
(200301000010, 20030, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200301000020, 20030, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200301000030, 20030, 10, 1000030, DATEADD(d, 1, DATEADD(m, -12, GETDATE())), 1)

INSERT INTO PPArticlesEnPanier values
(200311000010, 20031, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200311000020, 20031, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200311000030, 20031, 10, 1000030, DATEADD(m, -12, GETDATE()), 1)

INSERT INTO PPArticlesEnPanier values
(200321000010, 20032, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200321000020, 20032, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200321000030, 20032, 10, 1000030, DATEADD(d, -1, DATEADD(m, -12, GETDATE())), 1)

-- 1 an
INSERT INTO PPArticlesEnPanier values
(200401000010, 20040, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200401000020, 20040, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200401000030, 20040, 10, 1000030, DATEADD(d, 1, DATEADD(y, -1, GETDATE())), 1)

INSERT INTO PPArticlesEnPanier values
(200411000010, 20041, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200411000020, 20041, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200411000030, 20041, 10, 1000030, DATEADD(y, -1, GETDATE()), 1)

INSERT INTO PPArticlesEnPanier values
(200421000010, 20042, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200421000020, 20042, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200421000030, 20042, 10, 1000030, DATEADD(d, -1, DATEADD(y, -1, GETDATE())), 1)

-- 2 ans
INSERT INTO PPArticlesEnPanier values
(200501000010, 20050, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200501000020, 20050, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200501000030, 20050, 10, 1000030, DATEADD(d, 1, DATEADD(y, -2, GETDATE())), 1)

INSERT INTO PPArticlesEnPanier values
(200511000010, 20051, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200511000020, 20051, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200511000030, 20051, 10, 1000030, DATEADD(y, -2, GETDATE()), 1)

INSERT INTO PPArticlesEnPanier values
(200521000010, 20052, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200521000020, 20052, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200521000030, 20052, 10, 1000030, DATEADD(d, -1, DATEADD(y, -2, GETDATE())), 1)

-- 3 ans
INSERT INTO PPArticlesEnPanier values
(200601000010, 20060, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200601000020, 20060, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200601000030, 20060, 10, 1000030, DATEADD(d, 1, DATEADD(y, -3, GETDATE())), 1)

INSERT INTO PPArticlesEnPanier values
(200611000010, 20061, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200611000020, 20061, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200611000030, 20061, 10, 1000030, DATEADD(y, -3, GETDATE()), 1)

INSERT INTO PPArticlesEnPanier values
(200621000010, 20062, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200621000020, 20062, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(200621000030, 20062, 10, 1000030, DATEADD(d, -1, DATEADD(y, -3, GETDATE())), 1)

-- Vérifications commande
-- 1 mois
INSERT INTO PPArticlesEnPanier values
(300001000010, 30000, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300001000020, 30000, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300001000030, 30000, 10, 1000030, '2000-01-01', 1)

INSERT INTO PPArticlesEnPanier values
(300011000010, 30001, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300011000020, 30001, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300011000030, 30001, 10, 1000030, '2000-01-01', 1)

INSERT INTO PPArticlesEnPanier values
(300021000010, 30002, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300021000020, 30002, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300021000030, 30002, 10, 1000030, '2000-01-01', 1)

-- 3 mois
INSERT INTO PPArticlesEnPanier values
(300101000010, 30010, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300101000020, 30010, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300101000030, 30010, 10, 1000030, '2000-01-01', 1)

INSERT INTO PPArticlesEnPanier values
(300111000010, 30011, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300111000020, 30011, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300111000030, 30011, 10, 1000030, '2000-01-01', 1)

INSERT INTO PPArticlesEnPanier values
(300121000010, 30012, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300121000020, 30012, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300121000030, 30012, 10, 1000030, '2000-01-01', 1)

-- 6 mois
INSERT INTO PPArticlesEnPanier values
(300201000010, 30020, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300201000020, 30020, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300201000030, 30020, 10, 1000030, '2000-01-01', 1)

INSERT INTO PPArticlesEnPanier values
(300211000010, 30021, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300211000020, 30021, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300211000030, 30021, 10, 1000030, '2000-01-01', 1)

INSERT INTO PPArticlesEnPanier values
(300221000010, 30022, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300221000020, 30022, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300221000030, 30022, 10, 1000030, '2000-01-01', 1)

-- 12 mois
INSERT INTO PPArticlesEnPanier values
(300301000010, 30030, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300301000020, 30030, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300301000030, 30030, 10, 1000030, '2000-01-01', 1)

INSERT INTO PPArticlesEnPanier values
(300311000010, 30031, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300311000020, 30031, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300311000030, 30031, 10, 1000030, '2000-01-01', 1)

INSERT INTO PPArticlesEnPanier values
(300321000010, 30032, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300321000020, 30032, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300321000030, 30032, 10, 1000030, '2000-01-01', 1)

-- 1 an
INSERT INTO PPArticlesEnPanier values
(300401000010, 30040, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300401000020, 30040, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300401000030, 30040, 10, 1000030, '2000-01-01', 1)

INSERT INTO PPArticlesEnPanier values
(300411000010, 30041, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300411000020, 30041, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300411000030, 30041, 10, 1000030, '2000-01-01', 1)

INSERT INTO PPArticlesEnPanier values
(300421000010, 30042, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300421000020, 30042, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300421000030, 30042, 10, 1000030, '2000-01-01', 1)

-- 2 ans
INSERT INTO PPArticlesEnPanier values
(300501000010, 30050, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300501000020, 30050, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300501000030, 30050, 10, 1000030, '2000-01-01', 1)

INSERT INTO PPArticlesEnPanier values
(300511000010, 30051, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300511000020, 30051, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300511000030, 30051, 10, 1000030, '2000-01-01', 1)

INSERT INTO PPArticlesEnPanier values
(300521000010, 30052, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300521000020, 30052, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300521000030, 30052, 10, 1000030, '2000-01-01', 1)

-- 3 ans
INSERT INTO PPArticlesEnPanier values
(300601000010, 30060, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300601000020, 30060, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300601000030, 30060, 10, 1000030, '2000-01-01', 1)

INSERT INTO PPArticlesEnPanier values
(300611000010, 30061, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300611000020, 30061, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300611000030, 30061, 10, 1000030, '2000-01-01', 1)

INSERT INTO PPArticlesEnPanier values
(300621000010, 30062, 10, 1000010, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300621000020, 30062, 10, 1000020, '2000-01-01', 1)
INSERT INTO PPArticlesEnPanier values
(300621000030, 30062, 10, 1000030, '2000-01-01', 1)

-- Création commandes

-- Vérification panier
-- 1 mois
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2000, 20000, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2001, 20000, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2002, 20000, 10, '2000-01-01')

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2010, 20001, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2011, 20001, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2012, 20001, 10, '2000-01-01')

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2020, 20002, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2021, 20002, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2022, 20002, 10, '2000-01-01')

-- 3 mois
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2100, 20010, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2101, 20010, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2102, 20010, 10, '2000-01-01')

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2110, 20011, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2111, 20011, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2112, 20011, 10, '2000-01-01')

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2120, 20012, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2121, 20012, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2122, 20012, 10, '2000-01-01')

-- 6 mois
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2200, 20020, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2201, 20020, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2202, 20020, 10, '2000-01-01')

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2210, 20021, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2211, 20021, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2212, 20021, 10, '2000-01-01')

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2220, 20022, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2221, 20022, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2222, 20022, 10, '2000-01-01')

-- 12 mois
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2300, 20030, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2301, 20030, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2302, 20030, 10, '2000-01-01')

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2310, 20031, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2311, 20031, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2312, 20031, 10, '2000-01-01')

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2320, 20032, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2321, 20032, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2322, 20032, 10, '2000-01-01')

-- 1 an
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2400, 20040, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2401, 20040, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2402, 20040, 10, '2000-01-01')

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2410, 20041, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2411, 20041, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2412, 20041, 10, '2000-01-01')

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2420, 20042, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2421, 20042, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2422, 20042, 10, '2000-01-01')

-- 2 ans
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2500, 20050, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2501, 20050, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2502, 20050, 10, '2000-01-01')

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2510, 20051, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2511, 20051, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2512, 20051, 10, '2000-01-01')

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2520, 20052, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2521, 20052, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2522, 20052, 10, '2000-01-01')

-- 3 ans
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2600, 20060, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2601, 20060, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2602, 20060, 10, '2000-01-01')

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2610, 20061, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2611, 20061, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2612, 20061, 10, '2000-01-01')

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2620, 20062, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2621, 20062, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(2622, 20062, 10, '2000-01-01')

-- Vérification commandes
-- 1 mois
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3000, 30000, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3001, 30000, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3002, 30000, 10, DATEADD(d, 1, DATEADD(m, -1, GETDATE())))

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3010, 30001, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3011, 30001, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3012, 30001, 10, DATEADD(m, -1, GETDATE()))

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3020, 30002, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3021, 30002, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3022, 30002, 10, DATEADD(d, -1, DATEADD(m, -1, GETDATE())))

-- 3 mois
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3100, 30010, 10, DATEADD(d, 1, DATEADD(m, -3, GETDATE())))
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3101, 30010, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3102, 30010, 10, '2000-01-01')

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3110, 30011, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3111, 30011, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3112, 30011, 10, DATEADD(m, -3, GETDATE()))

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3120, 30012, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3121, 30012, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3122, 30012, 10, DATEADD(d, -1, DATEADD(m, -3, GETDATE())))

-- 6 mois
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3200, 30020, 10, DATEADD(d, 1, DATEADD(m, -6, GETDATE())))
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3201, 30020, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3202, 30020, 10, '2000-01-01')

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3210, 30021, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3211, 30021, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3212, 30021, 10, DATEADD(m, -6, GETDATE()))

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3220, 30022, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3221, 30022, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3222, 30022, 10, DATEADD(d, -1, DATEADD(m, -6, GETDATE())))

-- 12 mois
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3300, 30030, 10, DATEADD(d, 1, DATEADD(m, -12, GETDATE())))
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3301, 30030, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3302, 30030, 10, '2000-01-01')

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3310, 30031, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3311, 30031, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3312, 30031, 10, DATEADD(m, -12, GETDATE()))

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3320, 30032, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3321, 30032, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3322, 30032, 10, DATEADD(d, -1, DATEADD(m, -12, GETDATE())))

-- 1 an
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3400, 30040, 10, DATEADD(d, 1, DATEADD(y, -1, GETDATE())))
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3401, 30040, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3402, 30040, 10, '2000-01-01')

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3410, 30041, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3411, 30041, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3412, 30041, 10, DATEADD(y, -1, GETDATE()))

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3420, 30042, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3421, 30042, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3422, 30042, 10, DATEADD(d, -1, DATEADD(y, -1, GETDATE())))

-- 2 ans
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3500, 30050, 10, DATEADD(d, 1, DATEADD(y, -2, GETDATE())))
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3501, 30050, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3502, 30050, 10, '2000-01-01')

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3510, 30051, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3511, 30051, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3512, 30051, 10, DATEADD(y, -2, GETDATE()))

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3520, 30052, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3521, 30052, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3522, 30052, 10, DATEADD(d, -1, DATEADD(y, -2, GETDATE())))

-- 3 ans
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3600, 30060, 10, DATEADD(d, 1, DATEADD(y, -3, GETDATE())))
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3601, 30060, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3602, 30060, 10, '2000-01-01')

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3610, 30061, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3611, 30061, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3612, 30061, 10, DATEADD(y, -3, GETDATE()))

INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3620, 30062, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3621, 30062, 10, '2000-01-01')
INSERT INTO PPCommandes(NoCommande, NoClient, NoVendeur, DateCommande) values
(3622, 30062, 10, DATEADD(d, -1, DATEADD(y, -3, GETDATE())))

-- Details
-- Paniers

-- 1 mois
INSERT INTO PPDetailsCommandes values
(2000, 2000, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2001, 2001, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2002, 2002, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(2010, 2010, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2011, 2011, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2012, 2012, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(2020, 2020, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2021, 2021, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2022, 2022, 1000010, 1, 1)

-- 3 mois
INSERT INTO PPDetailsCommandes values
(2100, 2100, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2101, 2101, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2102, 2102, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(2110, 2110, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2111, 2111, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2112, 2112, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(2120, 2120, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2121, 2121, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2122, 2122, 1000010, 1, 1)

-- 6 mois
INSERT INTO PPDetailsCommandes values
(2200, 2200, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2201, 2201, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2202, 2202, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(2210, 2210, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2211, 2211, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2212, 2212, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(2220, 2220, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2221, 2221, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2222, 2222, 1000010, 1, 1)

-- 12 mois
INSERT INTO PPDetailsCommandes values
(2300, 2300, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2301, 2301, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2302, 2302, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(2310, 2310, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2311, 2311, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2312, 2312, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(2320, 2320, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2321, 2321, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2322, 2322, 1000010, 1, 1)

-- 1 an
INSERT INTO PPDetailsCommandes values
(2400, 2400, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2401, 2401, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2402, 2402, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(2410, 2410, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2411, 2411, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2412, 2412, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(2420, 2420, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2421, 2421, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2422, 2422, 1000010, 1, 1)

-- 2 ans
INSERT INTO PPDetailsCommandes values
(2500, 2500, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2501, 2501, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2502, 2502, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(2510, 2510, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2511, 2511, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2512, 2512, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(2520, 2520, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2521, 2521, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2522, 2522, 1000010, 1, 1)

-- 3 ans
INSERT INTO PPDetailsCommandes values
(2600, 2600, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2601, 2601, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2602, 2602, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(2610, 2610, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2611, 2611, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2612, 2612, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(2620, 2620, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2621, 2621, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(2622, 2622, 1000010, 1, 1)

-- Commandes
-- 1 mois
INSERT INTO PPDetailsCommandes values
(3000, 3000, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3001, 3001, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3002, 3002, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(3010, 3010, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3011, 3011, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3012, 3012, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(3020, 3020, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3021, 3021, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3022, 3022, 1000010, 1, 1)

-- 3 mois
INSERT INTO PPDetailsCommandes values
(3100, 3100, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3101, 3101, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3102, 3102, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(3110, 3110, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3111, 3111, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3112, 3112, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(3120, 3120, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3121, 3121, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3122, 3122, 1000010, 1, 1)

-- 6 mois
INSERT INTO PPDetailsCommandes values
(3200, 3200, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3201, 3201, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3202, 3202, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(3210, 3210, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3211, 3211, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3212, 3212, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(3220, 3220, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3221, 3221, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3222, 3222, 1000010, 1, 1)

-- 12 mois
INSERT INTO PPDetailsCommandes values
(3300, 3300, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3301, 3301, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3302, 3302, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(3310, 3310, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3311, 3311, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3312, 3312, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(3320, 3320, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3321, 3321, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3322, 3322, 1000010, 1, 1)

-- 1 an
INSERT INTO PPDetailsCommandes values
(3400, 3400, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3401, 3401, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3402, 3402, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(3410, 3410, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3411, 3411, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3412, 3412, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(3420, 3420, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3421, 3421, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3422, 3422, 1000010, 1, 1)

-- 2 ans
INSERT INTO PPDetailsCommandes values
(3500, 3500, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3501, 3501, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3502, 3502, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(3510, 3510, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3511, 3511, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3512, 3512, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(3520, 3520, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3521, 3521, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3522, 3522, 1000010, 1, 1)

-- 3 ans
INSERT INTO PPDetailsCommandes values
(3600, 3600, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3601, 3601, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3602, 3602, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(3610, 3610, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3611, 3611, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3612, 3612, 1000010, 1, 1)

INSERT INTO PPDetailsCommandes values
(3620, 3620, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3621, 3621, 1000010, 1, 1)
INSERT INTO PPDetailsCommandes values
(3622, 3622, 1000010, 1, 1)

-- Visites
INSERT INTO PPVendeursClients values
(10, 20001, '2000-01-01')

INSERT INTO PPVendeursClients values
(10, 20021, '2000-01-01')

INSERT INTO PPVendeursClients values
(10, 20041, '2000-01-01')

INSERT INTO PPVendeursClients values
(10, 20061, '2000-01-01')

INSERT INTO PPVendeursClients values
(10, 30001, '2000-01-01')

INSERT INTO PPVendeursClients values
(10, 30021, '2000-01-01')

INSERT INTO PPVendeursClients values
(10, 30041, '2000-01-01')

INSERT INTO PPVendeursClients values
(10, 30061, '2000-01-01')