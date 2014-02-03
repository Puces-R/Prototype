DROP TABLE PPEvaluations

CREATE TABLE PPEvaluations (
	NoClient bigint FOREIGN KEY REFERENCES PPClients(NoClient),
	NoProduit bigint FOREIGN KEY REFERENCES PPProduits(NoProduit),
	Cote numeric(2,1) NOT NULL,
	Commentaire varchar(150),
	DateMAJ smalldatetime,
	DateCreation smalldatetime NOT NULL,
	CONSTRAINT pk_PPEvaluations PRIMARY KEY (NoClient,NoProduit)
)

INSERT INTO PPEvaluations VALUES (10000, 1000010, 3.5, 'Fonctionne très bien et pas chère.', NULL, 2013-02-01)
INSERT INTO PPEvaluations VALUES (10400, 1000010, 4, 'Je le recommande à tout mes amis.', NULL, 2013-01-15)
INSERT INTO PPEvaluations VALUES (10200, 1000010, 1.5, 'Très fragile et ne fonctionne pas toujours.', NULL, 2014-01-24)
INSERT INTO PPEvaluations VALUES (10000, 2000030, 3.5, 'Fonctionne très bien et pas chère.', NULL, 2013-02-01)
INSERT INTO PPEvaluations VALUES (10400, 2000030, 4, 'Je le recommande à tout mes amis.', NULL, 2013-01-15)
INSERT INTO PPEvaluations VALUES (10200, 2000030, 1.5, 'Très fragile et ne fonctionne pas toujours.', NULL, 2014-01-24)
INSERT INTO PPEvaluations VALUES (10000, 3000030, 3.5, 'Fonctionne très bien et pas chère.', NULL, 2013-02-01)
INSERT INTO PPEvaluations VALUES (10400, 3000030, 4, 'Je le recommande à tout mes amis.', NULL, 2013-01-15)
INSERT INTO PPEvaluations VALUES (10200, 3000030, 1.5, 'Très fragile et ne fonctionne pas toujours.', NULL, 2014-01-24)