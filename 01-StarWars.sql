CREATE DATABASE StarWars
GO

USE StarWars
GO

CREATE TABLE dbo.Planet(
	Id int NOT NULL,
	Name varchar(256) NOT NULL,
	Climate varchar(256) NOT NULL,
	CreateDate Datetime,
	CONSTRAINT PK_Planet PRIMARY KEY (Id)
)
GO

CREATE TABLE dbo.Terrain(
	Id int NOT NULL,
	Name varchar(256) NOT NULL,
	CreateDate Datetime,
	CONSTRAINT PK_Terrain PRIMARY KEY (Id)
)
GO

CREATE TABLE dbo.Film(
	Id int NOT NULL,
	Name varchar(256) NOT NULL,
	Director varchar(256) NOT NULL,
	ReleaseDate Datetime,
	CreateDate Datetime,
	CONSTRAINT PK_Film PRIMARY KEY (Id)
)
GO

CREATE TABLE dbo.Planet_Film(
	IdPlanet int NOT NULL,
	IdFilm int NOT NULL,
    CONSTRAINT FK_PlanetFilm FOREIGN KEY (IdPlanet) REFERENCES dbo.Planet(Id),
    CONSTRAINT FK_FilmPlanet FOREIGN KEY (IdFilm) REFERENCES dbo.Film(Id)
)
GO

CREATE TABLE dbo.Planet_Terrain(
	IdPlanet int NOT NULL,
	IdTerrain int NOT NULL,
    CONSTRAINT FK_PlanetTerrain FOREIGN KEY (IdPlanet) REFERENCES dbo.Planet(Id),
    CONSTRAINT FK_TerrainPlanet FOREIGN KEY (IdTerrain) REFERENCES dbo.Terrain(Id)
)
GO