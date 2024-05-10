namespace DataLayer.Services.DbSeeder;

public interface IDbSeeder
{
    Task SeedAllEntitiesAsync();
}