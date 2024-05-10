using DataLayer.FetchDTO.Ability;
using DataLayer.FetchDTO.Move;
using DataLayer.FetchDTO.Types;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Persistence.Configurations;

public class PokemonConfiguration : IEntityTypeConfiguration<Pokemon>
{
    public void Configure(EntityTypeBuilder<Pokemon> builder)
    {
        builder.HasMany(i => i.Abilities)
            .WithMany(i => i.Pokemons)
            .UsingEntity<PokemonAbilityRelationship>("PokemonAbilities");

        builder.HasMany(i => i.Moves)
            .WithMany(i => i.Pokemons)
            .UsingEntity<PokemonMoveRelationship>("PokemonMoves");
        
        builder.HasMany(i => i.Types)
            .WithMany(i => i.Pokemons)
            .UsingEntity<PokemonTypeRelationship>("PokemonTypes");
    }
}