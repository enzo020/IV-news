import "./App.css";
import Footer from "./components/Footer";
import Header from "./components/Header";
import Navbar from "./components/Navbar";
import Home from "./pages/Home";

function App() {
    return (
        <>
            <Header />
            <Navbar />
            <Home />
            <Footer />
        </>
    );
}

export default App;
