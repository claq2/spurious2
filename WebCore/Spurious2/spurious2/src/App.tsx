import React from "react";
import "./App.css";
import Home from "./pages/Home";
import {
  // BrowserRouter,
  // Routes,
  Route,
  RouterProvider,
  // Outlet,
} from "react-router-dom";
// import NavBar, { dataLoader } from "./components/NavBar";
// import DefaultMap from "./components/DefaultMap";
// import Container from "@mui/material/Container";
// import SubdivisionList from "./components/SubdivisionList";
// import { useGetDensitiesQuery } from "./services/densities";
// import { Density, Subdivision } from "./services/types";
// import {
//   useLazyGetSubdivisionsByDensityQuery,
//   useLazyGetBoundaryBySubdivisionIdQuery,
// } from "./services/subdivisions";
import {
  createBrowserRouter,
  createRoutesFromElements,
} from "react-router-dom";
import Shell, { dataLoader } from "./pages/Shell";
import About from "./components/About";

export type StaticRoute = { route: string; element: JSX.Element };

export const staticRoutes: StaticRoute[] = [
  { route: "about", element: <About /> },
];

const router = createBrowserRouter(
  createRoutesFromElements(
    <>
      <Route path="/" element={<Shell />} id="root" loader={dataLoader}>
        {staticRoutes.map((r) => (
          <Route path={r.route} element={r.element} />
        ))}
        <Route path=":id" element={<Home />}></Route>
      </Route>
    </>
  )
);

const App = () => {
  // const [densities, setDensities] = useState<Density[]>([]);
  // const denstitiesResponse = useGetDensitiesQuery();
  // const [getSubdivsQuery, subdivsResult] =
  //   useLazyGetSubdivisionsByDensityQuery();
  // const [getBoundary, boundaryResult] =
  //   useLazyGetBoundaryBySubdivisionIdQuery();
  // const [subdivisions, setSubdivisions] = useState<Subdivision[]>([]);

  // useEffect(() => {
  //   if (denstitiesResponse.isSuccess && denstitiesResponse.data) {
  //     console.log("densities", denstitiesResponse.data);
  //     setDensities(denstitiesResponse.data);
  //   }
  // }, [denstitiesResponse]);

  // useEffect(() => {
  //   if (densities.length > 0) {
  //     getSubdivsQuery(densities[0].shortName);
  //   }
  // }, [densities, getSubdivsQuery]);

  // useEffect(() => {
  //   if (subdivsResult.isSuccess && subdivsResult.data) {
  //     console.log("subdivsResult.data", subdivsResult.data);
  //     setSubdivisions(subdivsResult.data);
  //   }
  // }, [subdivsResult]);

  // useEffect(() => {
  //   if (subdivisions.length > 0) {
  //     getBoundary(subdivisions[0].id);
  //   }
  // }, [subdivisions, getBoundary]);

  // useEffect(() => {
  //   if (boundaryResult.isSuccess && boundaryResult.data) {
  //     console.log("boundaryResult", boundaryResult.data);
  //   }
  // }, [boundaryResult]);

  return (
    <>
      <RouterProvider router={router} />
      {/* <Container>
        <div>
          <h1>Alcohol Density per Census Subdivision</h1>
        </div>
        <DefaultMap />
        <SubdivisionList />
      </Container> */}
      {/* <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/:id" element={<Home />} />
        </Routes> */}
    </>
  );
};

export default App;
