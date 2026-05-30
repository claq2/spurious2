import type { ReactElement } from "react";
import About from "./components/About";

export type StaticRoute = { route: string; element: ReactElement };

export const staticRoutes: StaticRoute[] = [
  { route: "about", element: <About /> },
];
