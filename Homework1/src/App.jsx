import './App.css';
import {Route, Routes} from "react-router-dom";
import routes from "./utils/routes";

function App() {
    return (
        <div className="App">
            <Routes>
                {
                    routes.map(i =>
                        <Route path={i.path} element={<i.Component/>}/>)
                }
            </Routes>
        </div>
    );
}

export default App;
