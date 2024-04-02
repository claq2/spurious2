import React from "react";
import "./App.css";
import Home from "./pages/Home";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import NavBar from "./components/NavBar";
import DefaultMap from "./components/DefaultMap";
import Container from "@mui/material/Container";
import SubdivisionList from "./components/SubdivisionList";

const App = () => {
  return (
    <BrowserRouter>
      <NavBar />
      <Container>
        <div>
          <h1>Alcohol Density per Census Subdivision</h1>
        </div>
        <DefaultMap />
        <SubdivisionList />
      </Container>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/:id" element={<Home />} />
      </Routes>
    </BrowserRouter>
  );
};

export default App;
