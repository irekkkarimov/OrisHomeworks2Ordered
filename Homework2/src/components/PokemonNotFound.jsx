const PokemonNotFound = () => {


    return (
        <div className="pokemonNotFound">
            <p className="pokemonNotFound__header">Ooops! Try again.</p>
            <p className="pokemonNotFound__description">The pokemon you're looking for is a unicorn. It doesn't exist in
                this list.</p>
            <img
                src="https://marriland.com/wp-content/plugins/marriland-core/images/pokemon/sprites/home/full/pikachu-partner-cap.png"
                alt="pikachu with cap"/>
        </div>
    )
}

export default PokemonNotFound