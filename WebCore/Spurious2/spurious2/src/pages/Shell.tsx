import * as React from "react";
import {
  Outlet,
  useLoaderData,
  useParams,
  useNavigate,
} from "react-router-dom";
import { Density } from "../services/types";
import { useEffect, useState } from "react";
import { store } from "../store";
import { densityApi } from "../services/densities";

const Shell = () => {
  const { id } = useParams();
  const result: Density[] = useLoaderData() as Density[];
  const navigate = useNavigate();
  useEffect(() => {
    console.log("id in shell", id);
    console.log("result in shell", result);
    if (!id && result.length > 0) {
      navigate(`/${result[0].shortName}`, { replace: true });
    }
  }, [id, result, navigate]);

  return (
    <>
      {/* <NavBar /> */}
      <Outlet />
    </>
  );
};

export default Shell;

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
