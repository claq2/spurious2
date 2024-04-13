import React, { useEffect, useState } from "react";
import "./App.css";
import Home from "./pages/Home";
import { BrowserRouter, Routes, Route, RouterProvider } from "react-router-dom";
import NavBar from "./components/NavBar";
import DefaultMap from "./components/DefaultMap";
import Container from "@mui/material/Container";
import SubdivisionList from "./components/SubdivisionList";
import { useGetDensitiesQuery } from "./services/densities";
import { Density, Subdivision } from "./services/types";
import {
  useLazyGetSubdivisionsByDensityQuery,
  useLazyGetBoundaryBySubdivisionIdQuery,
} from "./services/subdivisions";
import {
  createBrowserRouter,
  createRoutesFromElements,
} from "react-router-dom";
import NavBar2, { dataLoader } from "./components/NavBar2";

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" element={<NavBar2 />} loader={dataLoader}></Route>
  )
);

const App = () => {
  const [densities, setDensities] = useState<Density[]>([]);
  const denstitiesResponse = useGetDensitiesQuery();
  const [getSubdivsQuery, subdivsResult] =
    useLazyGetSubdivisionsByDensityQuery();
  const [getBoundary, boundaryResult] =
    useLazyGetBoundaryBySubdivisionIdQuery();
  const [subdivisions, setSubdivisions] = useState<Subdivision[]>([]);

  useEffect(() => {
    if (denstitiesResponse.isSuccess && denstitiesResponse.data) {
      console.log("densities", denstitiesResponse.data);
      setDensities(denstitiesResponse.data);
    }
  }, [denstitiesResponse]);

  useEffect(() => {
    if (densities.length > 0) {
      getSubdivsQuery(densities[0].shortName);
    }
  }, [densities, getSubdivsQuery]);

  useEffect(() => {
    if (subdivsResult.isSuccess && subdivsResult.data) {
      console.log("subdivsResult.data", subdivsResult.data);
      setSubdivisions(subdivsResult.data);
    }
  }, [subdivsResult]);

  useEffect(() => {
    if (subdivisions.length > 0) {
      getBoundary(subdivisions[0].id);
    }
  }, [subdivisions, getBoundary]);

  useEffect(() => {
    if (boundaryResult.isSuccess && boundaryResult.data) {
      console.log("boundaryResult", boundaryResult.data);
    }
  }, [boundaryResult]);

  return (
    <>
      <RouterProvider router={router} />
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
    </>
  );
};

export default App;
