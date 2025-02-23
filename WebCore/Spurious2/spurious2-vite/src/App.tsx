import "./App.css";
import Home from "./pages/Home";
import {
  Route,
  RouterProvider,
  createBrowserRouter,
  createRoutesFromElements,
} from "react-router-dom";
import Shell from "./pages/Shell";
import { staticRoutes } from "./staticRoutes";
import { dataLoader } from "./pages/dataLoader";

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
