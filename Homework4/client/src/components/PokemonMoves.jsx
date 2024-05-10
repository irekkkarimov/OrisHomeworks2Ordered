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
                {moves.map(i => <PokemonMoveCard name={i.name} type={i.type}/>)}
            </div>
        </div>
    )
}

export default PokemonMoves

const PokemonMoveCard = ({name, type}) => {
    if (name.includes('-'))
        name = name.split('-').join(' ')

    return (
        <div
            style={{backgroundColor: typeColors[type.name]}}
            className="pokemon-move-card">
            <img src={typeImages[type.name]} alt={type.name}/>
            <p>{name}</p>
        </div>
    )
}