import { useState } from "react";
import "./App.css";
import Footer from "./components/Footer";
import Header from "./components/Header";
import Navbar from "./components/Navbar";
import Sidebar from "./components/Sidebar";
import Home from "./pages/Home";

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
            <Home />
            <Footer />
        </>
    );
}

export default App;
