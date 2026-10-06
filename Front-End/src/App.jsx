import { useState } from "react";
import { Routes, Route } from 'react-router-dom';
import "./App.css";

// Componentes
import Footer from "./components/Footer";
import Header from "./components/Header";
import Navbar from "./components/Navbar";
import Sidebar from "./components/Sidebar";
import About from "./pages/About";
import Help from "./pages/Help";
import News from "./pages/News";

// Páginas
import Home from "./pages/Home";
import NewsDetails from "./pages/NewsDetails";

function App() {

    const [isMenuOpen, setIsMenuOpen] = useState(false);

    return (
        <>
            <Header
                onMenuClick={() => setIsMenuOpen(true)}
            />

            <Sidebar
                isOpen={isMenuOpen}
                onClose={() => setIsMenuOpen(false)}
            />

            <Navbar />

            <Routes>
                <Route
                    path="/"
                    element={<Home />}
                />

                <Route
                    path="/noticia/:id"
                    element={<NewsDetails />}
                />

                <Route
                    path="/noticias"
                    element={<News />}
                />

                <Route
                    path="/sobre"
                    element={<About />}
                />

                <Route
                    path="/ajuda"
                    element={<Help />}
                />
            </Routes>

            <Footer />
        </>
    );
}

export default App;
