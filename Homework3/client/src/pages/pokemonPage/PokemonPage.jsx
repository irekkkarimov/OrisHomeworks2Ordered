import PokemonMain from "../../components/PokemonMain";
import "./PokemonPage.css"
import {useEffect, useState} from "react";
import PokemonBreeding from "../../components/PokemonBreeding";
import PokemonMoves from "../../components/PokemonMoves";
import PokemonAbilities from "../../components/PokemonAbilities";
import {useNavigate, useParams} from "react-router-dom";
import pokeball from "../../assets/Pokeball.png";
import arrow from "../../assets/arrow-89-32.ico";
import loadPokemonDetailed from "../../utils/loadPokemonDetailed";


const PokemonPage = () => {
    const navigate = useNavigate()
    const [pokemon, setPokemon] = useState({
        name: '',
        sprites: {other: {home: {front_Default: ''}}}
    })
    const [types, setTypes] = useState([])
    const [stats, setStats] = useState([])
    const [moves, setMoves] = useState([])
    const [abilities, setAbilities] = useState([])
    const {name} = useParams()
    let statNamesRequired = [
        'hp', 'attack', 'defense', 'speed'
    ]

    useEffect(() => {
        loadPokemonDetailed(name)
            .then(json => {
                setPokemon(json)
                setTypes(json.types.map(i => i.type))
                setStats(json.stats.map(i => ({
                    level: i.base_stat,
                    stat: i.stat
                })).filter(i => statNamesRequired.includes(i.stat.name)))
                setMoves(json.moves.map(i => i.move))
                setAbilities(json.abilities.map(i => i.ability))
            })
    }, []);

    return (
        <div className="pokemon-page">
            <div className="pokemon-page__header">
                <img
                    onClick={() => navigate('/')}
                    className="pp__header__arrow"
                    src={arrow}
                    alt="Go Back"/>
                <img
                    className="pp__header__pokeball"
                    src={pokeball}
                    alt="Pokeball"/>
            </div>
            <div className="pokemon-page__body">
                <PokemonMain
                    id={pokemon.id}
                    name={pokemon.name}
                    types={types}
                    stats={stats}
                    image={pokemon.sprites.other.home.front_Default
                        ? pokemon.sprites.other.home.front_Default
                        : "https://www.picclickimg.com/gTsAAOSwoUBfoJx6/Pokemon-placeholder-special-listing-to-be-updated.webp"}/>
                <PokemonBreeding height={pokemon.height} weight={pokemon.weight}/>
                <PokemonMoves moves={moves}/>
                <PokemonAbilities abilities={abilities}/>
            </div>
        </div>
    )
}

export default PokemonPage