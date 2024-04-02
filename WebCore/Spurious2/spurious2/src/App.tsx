import React from "react";
import "./App.css";
import Home from "./pages/Home";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import NavBar from "./components/NavBar";
import DefaultMap from "./components/DefaultMap";
import Container from "@mui/material/Container";

const App = () => {
  return (
    <BrowserRouter>
      <NavBar />
      <Container>
        <DefaultMap />
      </Container>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/:id" element={<Home />} />
      </Routes>
    </BrowserRouter>
  );
};

export default App;
