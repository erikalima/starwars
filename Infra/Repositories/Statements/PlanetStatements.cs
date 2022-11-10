namespace StarWars.Api.Infra.Repositories.Statements
{
    internal static class PlanetStatements
    {
        internal const string GetById = @"SELECT Id, Name, Climate, Terrain 
                                          FROM StarWars.dbo.Planet p 
                                          WHERE ID = @id";
        
        internal const string Insert = @"INSERT INTO StarWars.dbo.Planet (Id, Name, Climate, Terrain, CreatedDate)
                                        VALUES (@id, @name, @climate, @terrain, getdate())";

        internal const string GetAll = @"SELECT P.ID, P.Name, P.Climate, P.Terrain
                                         FROM StarWars.dbo.Planet P";

        internal const string GetByName = @"SELECT Id, Name, Climate, Terrain 
                                            FROM StarWars.dbo.Planet p 
                                            WHERE Name LIKE @name";

        internal const string Delete = @"
                                        DELETE FROM Planet_Film 
                                        WHERE IdPlanet = @id;

                                        DELETE FROM Planet 
                                        WHERE ID = @id";
    }
}