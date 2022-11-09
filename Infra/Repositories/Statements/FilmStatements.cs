namespace StarWars.Api.Infra.Repositories.Statements
{
    internal static class FilmStatements
    {
        internal const string GetByIds = @"	SELECT
												Id,
												Name,
												Director,
												ReleaseDate,
												CreatedDate
											FROM
												StarWars.dbo.Film f 
											WHERE 
												Id IN (@ids)";

        internal const string Insert = @"		
										INSERT INTO StarWars.dbo.Film (Id, Title, Director, ReleaseDate, CreatedDate)
										VALUES(@id, @title, @director, @releaseDate, getdate())";
    }
}