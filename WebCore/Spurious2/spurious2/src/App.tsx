import React, { useEffect, useState } from "react";
import "./App.css";
import Home from "./pages/Home";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import NavBar from "./components/NavBar";
import DefaultMap from "./components/DefaultMap";
import Container from "@mui/material/Container";
import SubdivisionList from "./components/SubdivisionList";
import { useGetDensitiesQuery } from "./services/densities";
import { Density } from "./services/types";
import { useLazyGetSubdivisionsByDensityQuery } from "./services/subdivisions";

const App = () => {
  const [densities, setDensities] = useState<Density[]>([]);
  const resp = useGetDensitiesQuery();
  const [getSubdivsQuery, subdivsResult, z] =
    useLazyGetSubdivisionsByDensityQuery();

  useEffect(() => {
    if (resp.isSuccess && resp.data) {
      console.log("data", resp.data);
      setDensities(resp.data);
    }
  }, [resp]);

  useEffect(() => {
    getSubdivsQuery(densities[0].shortName);
  }, [densities, getSubdivsQuery]);

  useEffect(() => {
    if (subdivsResult.isSuccess && subdivsResult.data) {
      console.log(subdivsResult.data);
    }
  }, [subdivsResult]);

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
