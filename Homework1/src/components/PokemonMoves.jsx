import {useEffect, useState} from "react";
import typeImages from "../utils/typeImages";
import typeColors from "../utils/typeColors";

const PokemonMoves = ({moves}) => {
    moves = moves.slice(0, 6)

    return (
        <div className="pokemon-page__moves">
            <div className="pp__moves__header">
                <p>Moves</p>
            </div>
            <div className="pp__moves__lower">
                {moves.map(i => <PokemonMoveCard name={i.name} url={i.url}/>)}
            </div>
        </div>
    )
}

export default PokemonMoves

const PokemonMoveCard = ({name, url}) => {
    const [type, setType] = useState('')
    if (name.includes('-'))
        name = name.split('-').join(' ')

    useEffect(() => {
        fetch(url)
            .then(response => response.json())
            .then(json => setType(json.type.name))
    }, []);

    return (
        <div
            style={{backgroundColor: typeColors[type]}}
            className="pokemon-move-card">
            <img src={typeImages[type]} alt={type}/>
            <p>{name}</p>
        </div>
    )
}