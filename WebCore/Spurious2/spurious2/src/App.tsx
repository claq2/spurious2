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
          <Route key={r.route} path={r.route} element={r.element} />
        ))}
        <Route path=":id" element={<Home />}></Route>
      </Route>
    </>
  )
);

const App = () => {
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
