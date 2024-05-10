import SearchPage from "../pages/SearchPage";
import PokemonPage from "../pages/pokemonPage/PokemonPage";

export default [
    {
        path: '/',
        Component: SearchPage
    },
    {
        path: '/pokemon/:name',
        Component: PokemonPage
    }
]