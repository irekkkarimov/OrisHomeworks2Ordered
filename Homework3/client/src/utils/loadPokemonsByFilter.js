const loadPokemonsByFilter = async (limit, offset, filter = '') => {
    if (filter === '')
        return await fetch(process.env.REACT_APP_API_URL + 'Pokemon/GetAll?' + new URLSearchParams({
            limit, offset
        }))
            .then(response => response.json())

    return await fetch(process.env.REACT_APP_API_URL + `Pokemon/GetByFilter/${filter}?` + new URLSearchParams({
        limit, offset
    }))
        .then(response => response.json())
}

export default loadPokemonsByFilter