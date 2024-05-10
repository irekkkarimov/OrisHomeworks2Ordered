const PokemonBreeding = ({height, weight}) => {
    let heightInFeet = (height / 10) * 3.281
    let remainder = heightInFeet - parseInt(heightInFeet)
    let remainderInInches = Math.round(remainder * 12)
    let inchesStringified = remainderInInches > 9 ? remainderInInches : `0${remainderInInches}`

    let weightInPounds = ((weight / 10) * 2.2).toFixed(1)

    if (weightInPounds % 1 === 0)
        weightInPounds = parseInt(weightInPounds)

    return (
        <div className="pokemon-page__breeding">
            <div className="pp__breeding__header">
                <p>Breeding</p>
            </div>
            <div className="pp__breeding__lower">
                <div className="pp__breeding__lower__stat">
                    <p>Height</p>
                    <StatCard first={parseInt(heightInFeet) + `'` + inchesStringified + `"`}
                              second={height / 10 + ' m'}/>
                </div>
                <div className="pp__breeding__lower__stat">
                    <p>Weight</p>
                    <StatCard first={weightInPounds + ' lbs'} second={weight / 10 + ' kg'}/>
                </div>
            </div>
        </div>
    )
}

export default PokemonBreeding


const StatCard = ({first, second}) => {


    return (
        <div className="stat-card">
            <p className="stat-card__first">{first}</p>
            <p className="stat-card__second">{second}</p>
        </div>
    )
}