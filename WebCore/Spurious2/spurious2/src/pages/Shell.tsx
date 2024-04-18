import * as React from "react";
import {
  Outlet,
  useLoaderData,
  useParams,
  useNavigate,
  useLocation,
} from "react-router-dom";
import { Density } from "../services/types";
import { useEffect } from "react";
import { store } from "../store";
import { densityApi } from "../services/densities";
import { staticRoutes } from "../App";

const Shell = () => {
  const { id } = useParams();
  const location = useLocation();
  const result: Density[] = useLoaderData() as Density[];
  const navigate = useNavigate();
  useEffect(() => {
    console.log("location", location);
    console.log("id in shell", id);
    console.log("result in shell", result);
    if (
      !id &&
      result.length > 0 &&
      !staticRoutes.find((r) => `/${r.route}` === location.pathname)
    ) {
      navigate(`/${result[0].shortName}`, { replace: true });
    }
  }, [id, result, navigate, location]);

  return (
    <>
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
