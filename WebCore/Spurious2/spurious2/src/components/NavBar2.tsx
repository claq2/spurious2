import React from "react";
import { useLoaderData, useNavigation, Link } from "react-router-dom";
import { Density } from "../services/types";
import { store } from "../store";
import { densityApi } from "../services/densities";

const NavBar2 = () => {
  const result: Density[] = useLoaderData() as Density[];
  const navigation = useNavigation();
  if (navigation.state === "loading") {
    return <h1>Loading!</h1>;
  }

  return (
    <div>
      {result.map((res) => (
        <div key={res.shortName}>
          <Link to={`/${res.shortName}`}>
            <h1>{res.title}</h1>
          </Link>
        </div>
      ))}
    </div>
  );
};

export default NavBar2;

export const dataLoader = async () => {
  const p = store.dispatch(densityApi.endpoints.getDensities.initiate());
  try {
    const ds = await p.unwrap();
    console.log("ds", ds);
    return ds;
  } finally {
    p.unsubscribe();
  }
};
