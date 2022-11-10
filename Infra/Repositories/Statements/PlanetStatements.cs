namespace StarWars.Api.Infra.Repositories.Statements
{
    internal static class PlanetStatements
    {
        internal const string GetById = @"SELECT Id, Name, Climate, Terrain FROM StarWars.dbo.Planet p 
                                          WHERE ID = @id";
        
        internal const string Insert = @"INSERT INTO StarWars.dbo.Planet (Id, Name, Climate, Terrain, CreatedDate)
                                        VALUES (@id, @name, @climate, @terrain, getdate())";
    }
}