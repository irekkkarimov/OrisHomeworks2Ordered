import typeColors from "../utils/typeColors";
import {useNavigate} from "react-router-dom";


const Card = ({id, name, types, img}) => {
    const navigate = useNavigate()
    let typesJustifyContent = types.length > 1 ? "space-between" : "start"
    let placeHolder = "https://www.picclickimg.com/gTsAAOSwoUBfoJx6/Pokemon-placeholder-special-listing-to-be-updated.webp"

    return (
        <div
            onClick={() => navigate(`pokemon/${name}`)}
            className='card'>
            <div className="card__upper">
                <div className="card__upper__wrapper">
                    <p className='card__upper__name'>
                        {name}
                    </p>
                    <p className="card__upper__id">
                        #{id}
                    </p>
                </div>
            </div>
            <div className="card__pic"><img
                src={img ? img : placeHolder}
                alt={name}
                className="card__pic"/>
            </div>
            <div className="card__lower" style={{justifyContent: typesJustifyContent}}>
                {types.map(i => {
                    return (
                        <div className="card__lower__option"
                             style={{backgroundColor: typeColors[i.name]}}>
                            <p>{i.name}</p>
                        </div>
                    )
                })}
            </div>
        </div>

    )
}

export default Card