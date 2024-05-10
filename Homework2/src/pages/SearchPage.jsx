import {useEffect, useState} from "react";
import Card from "../components/Card";
import PokemonNotFound from "../components/PokemonNotFound";
import pokeball from "../assets/Pokeball.png";
import searchIcon from "../assets/pngegg.png";
import load from "../utils/loadPokemonsDetailed";


const SearchPage = () => {
    const DefaultFirstLoadCount = 50
    const DefaultAfterLoadCount = 20
    const searchUrl = "https://pokeapi.co/api/v2/pokemon?"
    const [filter, setFilter] = useState('')
    const [pokemonFirstArray, setPokemonFirstArray] = useState([])
    const [pokemonDetailedArray, setPokemonDetailedArray] = useState([])
    const [searchArray, setSearchArray] = useState([])
    const [searchArrayDetailed, setSearchArrayDetailed] = useState([])
    const [isSearching, setIsSearching] = useState(false)
    const [isFound, setIsFound] = useState(false)
    const [currentLimit, setCurrentLimit] = useState(DefaultFirstLoadCount)
    const [searchCurrentLimit, setSearchCurrentLimit] = useState(DefaultFirstLoadCount)
    const [pokemonsLoaded, setPokemonsLoaded] = useState(0)
    const [searchPokemonsLoaded, setSearchPokemonsLoaded] = useState(0)
    const [fetching, setFetching] = useState(false)

    let handleSearch = () => {
        let newSearchArray = []

        pokemonFirstArray.forEach(pokemon => {
            if (pokemon.name.includes(filter)) {
                newSearchArray.push(pokemon.url)
            }
        })

        document.removeEventListener('scroll', scrollHandler)
        setSearchArray(newSearchArray)
        setIsSearching(true)
        setFetching(true)
    }

    useEffect(() => {
        if (fetching && isSearching) {
            let pokemonsToLoad = searchArray.slice(searchPokemonsLoaded, searchCurrentLimit)
            console.log(pokemonsToLoad)
            load(pokemonsToLoad)
                .then(response => {
                    console.log(response)
                    setSearchArrayDetailed(prev => [...prev, ...response])
                    setSearchPokemonsLoaded(prev => prev + response.length)
                    setSearchCurrentLimit(prev => prev + DefaultAfterLoadCount)

                    if (searchArrayDetailed.length > 0 || response.length > 0)
                        setIsFound(true)
                })
                .finally(() => {
                    setFetching(false)
                    document.addEventListener('scroll', scrollHandler)
                })

        }
    }, [fetching, isSearching]);

    useEffect(() => {
        document.addEventListener('scroll', scrollHandler)
        return function () {
            document.removeEventListener('scroll', scrollHandler)
        }
    }, []);

    useEffect(() => {
        fetch(searchUrl + `limit=10000`)
            .then(response => response.json())
            .then(json => {
                setPokemonFirstArray(json.results)
                setFetching(true)
            })
    }, []);

    useEffect(() => {
            if (fetching && !isSearching) {
                let newPokemons = pokemonFirstArray.slice(pokemonsLoaded, currentLimit)
                let newPokemonsLoaded = load(newPokemons.map(i => i.url))
                    .then(response => {
                        setPokemonDetailedArray(prev => [...prev, ...response])
                        setCurrentLimit(prev => prev + DefaultAfterLoadCount)
                        setPokemonsLoaded(prev => prev + response.length)
                    })
                    .finally(() => {
                        setFetching(false)
                        document.addEventListener('scroll', scrollHandler)
                    })
            }
        }, [fetching]
    );

    const scrollHandler = (e) => {
        console.log(isSearching && (searchArrayDetailed.length < searchArray.length))
        console.log(isSearching)
        console.log(searchArrayDetailed.length)
        console.log(searchArrayDetailed)
        console.log(searchArray.length)
        if ((e.target.documentElement.scrollHeight - (e.target.documentElement.scrollTop + window.innerHeight) < 100)
            && ((!isSearching && (pokemonDetailedArray.length < pokemonFirstArray.length))
            || (isSearching && (searchArrayDetailed.length < searchArray.length))) ) {
            console.log('reached')
            document.removeEventListener('scroll', scrollHandler)
            setFetching(true)
        }
    }

    return (
        <searchpage className="search-page">
            <div className="search-page__header">
                <img src={pokeball} alt="Pokeball" className="search-page__header__pokeball"/>
                <div className="search-page__header__wrapper">
                    <div className="search-page__header__text">
                        <h2 className="search-page__header__text">Who are you looking for?</h2>
                    </div>
                    <div className="search-page__header__sb">
                        <img src={searchIcon} alt="Search Icon" className="search-page__header__sb__img"/>
                        <input
                            type="text"
                            value={filter}
                            className="search-page__header__sb__input"
                            onChange={(e) => {
                                setFilter(e.target.value)
                                setIsSearching(false)
                                setSearchArray([])
                                setSearchArrayDetailed([])
                                setSearchCurrentLimit(DefaultFirstLoadCount)
                                setSearchPokemonsLoaded(0)
                                setIsFound(false)
                            }}/>
                        <button
                            className="search-page__header__sb__submit"
                            onClick={handleSearch}>
                            GO
                        </button>
                    </div>
                </div>
            </div>
            {isSearching ?
                !isFound
                    ? <PokemonNotFound/>
                    : <div className="search-page__body">
                        {searchArrayDetailed.map(i => <Card
                            id={i.id}
                            name={i.name}
                            types={i.types}
                            img={i.img}/>)}
                    </div>
                :
                <div className="search-page__body">
                    {pokemonDetailedArray.map(i => <Card
                        id={i.id}
                        name={i.name}
                        types={i.types}
                        img={i.img}/>)}
                </div>}
        </searchpage>
    )
}

export default SearchPage