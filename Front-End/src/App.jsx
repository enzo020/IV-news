import { useState } from "react";
import { Routes, Route } from 'react-router-dom';
import "./App.css";

// Componentes
import Footer from "./components/Footer";
import Header from "./components/Header";
import Navbar from "./components/Navbar";
import Sidebar from "./components/Sidebar";

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
            </Routes>

            <Footer />
        </>
    );
}

export default App;
