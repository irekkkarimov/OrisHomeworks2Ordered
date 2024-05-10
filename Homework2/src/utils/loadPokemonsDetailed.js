const load = async (urls) => {
    let pokemonsLoaded = []
    for (const url of urls) {
        await fetch(url)
            .then(response => response.json())
            .then(json => {
                let pokemonUrlSplit = url.split('/')
                let pokemonId = pokemonUrlSplit[pokemonUrlSplit.length - 2]
                let pokemonIdParsed = pokemonId.toString()

                if (pokemonId < 100)
                    pokemonIdParsed = `0${pokemonId}`
                if (pokemonId < 10)
                    pokemonIdParsed = `00${pokemonId}`

                let newPokemonDetailed = {
                    id: pokemonIdParsed,
                    name: json.name,
                    types: json.types.map(i => i.type),
                }

                if (json.sprites.other.home.front_default)
                    newPokemonDetailed.img = json.sprites.other.home.front_default
                else
                    newPokemonDetailed.img = json.sprites.other.home.front_default

                pokemonsLoaded.push(newPokemonDetailed)
            })
    }

    return pokemonsLoaded
}

export default load