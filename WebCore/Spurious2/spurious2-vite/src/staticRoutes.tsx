import About from "./components/About";

export type StaticRoute = { route: string; element: JSX.Element };

export const staticRoutes: StaticRoute[] = [
  { route: "about", element: <About /> },
];
