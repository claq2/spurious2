import React from "react";
import "./App.css";
import Home from "./pages/Home";
import { Route, RouterProvider } from "react-router-dom";
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
    </>
  );
};

export default App;
