import {useEffect, useState} from "react";
import Card from "../components/Card";
import PokemonNotFound from "../components/PokemonNotFound";
import pokeball from "../assets/Pokeball.png";
import searchIcon from "../assets/pngegg.png";
import loadPokemonsByFilter from "../utils/loadPokemonsByFilter";


const SearchPage = () => {
    const DefaultFirstLoadCount = 50
    const DefaultAfterLoadCount = 20
    const [filter, setFilter] = useState('')
    const [pokemonList, setPokemonList] = useState([])
    const [pokemonCount, setPokemonCount] = useState(0)
    const [isSearching, setIsSearching] = useState(false)
    const [isFound, setIsFound] = useState(false)
    const [currentLimit, setCurrentLimit] = useState(DefaultFirstLoadCount)
    const [pokemonsLoaded, setPokemonsLoaded] = useState(0)
    const [fetching, setFetching] = useState(true)
    const [isFirstLoaded, setIsFirstLoaded] = useState(false)
    const [trigger, setTrigger] = useState(false)

    let handleSearch = () => {
        setIsSearching(true)
    }

    useEffect(() => {
        // document.addEventListener('scroll', scrollHandler)
        return function () {
            document.removeEventListener('scroll', scrollHandler)
        }
    }, [pokemonCount]);

    useEffect(() => {
        if (pokemonList.length > 0) {
            setCurrentLimit(DefaultFirstLoadCount)
            setPokemonsLoaded(0)
            setPokemonList([])
            setFetching(true)
        }
    }, [isSearching]);

    useEffect(() => {
        if (fetching) {
            if (isSearching) {
                loadPokemonsByFilter(currentLimit, pokemonsLoaded, filter)
                    .then(json => {
                        if (json.results.length > 0)
                            setIsFound(true)
                        setPokemonList(prev => [...prev, ...json.results])
                        setCurrentLimit(DefaultAfterLoadCount)
                        setPokemonsLoaded(prev => prev + json.results.length)
                        setPokemonCount(json.count)
                        setFetching(false)
                        setIsFirstLoaded(true)
                        setTrigger(true)
                    })
            } else {
                loadPokemonsByFilter(currentLimit, pokemonsLoaded)
                    .then(json => {
                        setPokemonList(prev => [...prev, ...json.results])
                        setCurrentLimit(DefaultAfterLoadCount)
                        setPokemonsLoaded(prev => prev + json.results.length)
                        setPokemonCount(json.count)
                        setFetching(false)
                        setIsFirstLoaded(true)
                        setTrigger(true)
                    })
            }
        }

    }, [fetching]);

    useEffect(() => {
        if (trigger) {
            document.addEventListener('scroll', scrollHandler)
            setTrigger(false)
        }
    }, [trigger]);

    const scrollHandler = (e) => {
        console.log(1)
        console.log(pokemonList.length)
        console.log(pokemonCount)
        if ((e.target.documentElement.scrollHeight - (e.target.documentElement.scrollTop + window.innerHeight) < 200) && pokemonList.length < pokemonCount) {
            console.log(2)
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
                                setIsFound(false)
                                setCurrentLimit(DefaultFirstLoadCount)
                                setPokemonsLoaded(0)
                                document.addEventListener('scroll', scrollHandler)
                            }}/>
                        <button
                            className="search-page__header__sb__submit"
                            onClick={handleSearch}>
                            GO
                        </button>
                    </div>
                </div>
            </div>
            {isSearching && !isFound
                ? <PokemonNotFound/>
                : <div className="search-page__body">
                    {pokemonList.map(i => <Card
                        id={i.id}
                        name={i.name}
                        types={i.types.map(i => i.name)}
                        img={i.image}/>)}
                </div>}
        </searchpage>
    )
}

export default SearchPage