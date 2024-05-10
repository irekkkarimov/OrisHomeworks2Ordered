const PokemonAbilities = ({abilities}) => {

    return (
        <div className="pokemon-page__abilities">
            <div className="pp__abilities__header">
                <p>Abilities</p>
            </div>
            <div className="pp__abilities__lower">
                {abilities.map(i => <AbilityCard name={i.name}/>)}
            </div>
        </div>
    )
}

export default PokemonAbilities

const AbilityCard = ({name}) => {

    return (
        <div className="ability-card">
            <div className="ability-card__icon">
                <p>{name.slice(0, 1)[0]}</p>
            </div>
            <p>{name}</p>
        </div>
    )
}