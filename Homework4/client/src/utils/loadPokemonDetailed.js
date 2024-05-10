const loadPokemonDetailed = async (idOrName) => {
    return await fetch(process.env.REACT_APP_API_URL + `Pokemon/GetByIdOrName/${idOrName}`)
        .then(response => response.json())
}

export default loadPokemonDetailed