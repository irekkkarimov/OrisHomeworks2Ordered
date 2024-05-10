import typeColors from "../utils/typeColors";
import statColors from "../utils/statColors";


const PokemonMain = ({id, name, types, image, stats}) => {

    return (
        <div className="pokemon-page__hero">
            <div className="pp__hero__upper">
                <div className="pp__hero__upper__left">
                    <p className="pp__hero__upper__left__id">
                        #{id}
                    </p>
                    <div className="pp__hero__upper__left__name__wrapper">
                        <p className="pp__hero__upper__left__name">
                            {name}
                        </p>
                    </div>
                </div>
                <div className="pp__hero__upper__right">
                    {types.map(i =>
                        <div
                            style={{backgroundColor: typeColors[i.name]}}
                            className="pp__hero__upper__right__option">{i.name}</div>)}
                </div>
            </div>
            <div className="pp__hero__lower">
                <div className="pp__hero__lower__left">
                    {stats.map(i => <StatBar
                        name={i.name}
                        level={i.level}
                        fore={statColors[i.name].foreground}
                        back={statColors[i.name].background}/>)}
                </div>
                <div className="pp__hero__lower__right">
                    <img src={image} alt=""/>
                </div>
            </div>
        </div>
    )
}

export default PokemonMain

const StatBar = ({name, level, fore, back}) => {
    if (level > 100)
        level = 100

    return (
        <div className="pp__hero__lower__left__stat">
            <p>{name}</p>
            <div
                style={{backgroundColor: back}}
                className="bar">
                <div
                    style={
                        {
                            width: level + '%',
                            backgroundColor: fore
                        }
                    }
                    className="bar-filled">
                </div>
            </div>
        </div>
    )
}