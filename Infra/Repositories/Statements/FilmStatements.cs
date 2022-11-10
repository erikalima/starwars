namespace StarWars.Api.Infra.Repositories.Statements
{
    internal static class FilmStatements
    {
        internal const string GetByIds = @"	SELECT
												Id,
												Title,
												Director,
												ReleaseDate,
												CreatedDate
											FROM
												StarWars.dbo.Film f 
											WHERE 
												Id IN (@ids)";

        internal const string Insert = @"INSERT INTO StarWars.dbo.Film (Id, Title, Director, ReleaseDate, CreatedDate)
										VALUES(@id, @title, @director, @releaseDate, getdate())";
        
        internal const string InsertFilmsForPlanet = @"INSERT INTO StarWars.dbo.Planet_Film (IdPlanet, IdFilm)
                                                    VALUES(@planetId, @filmId)";
        
        internal const string GetAll = @"SELECT P.ID as PlanetId, F.Title, F.Director, F.ReleaseDate 
									    FROM Film F
									    INNER JOIN Planet_Film PF ON PF.IdFilm = F.Id 
									    INNER JOIN Planet P ON P.ID = PF.IdPlanet";

        internal const string GetByPlanetId = @"SELECT F.Title, F.Director, F.ReleaseDate 
												FROM Film F
												INNER JOIN Planet_Film PF ON PF.IdFilm = F.Id 
												INNER JOIN Planet P ON P.ID = PF.IdPlanet
												WHERE P.Id = @planetId";
    }
}