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
import NavBar from "../components/NavBar";
import Container from "@mui/material/Container";

const Shell = () => {
  const { id } = useParams();
  const location = useLocation();
  const result: Density[] = useLoaderData() as Density[];
  const navigate = useNavigate();
  useEffect(() => {
    console.debug("location", location);
    console.debug("id in shell", id);
    console.debug("result in shell", result);
    if (
      (!id &&
        result.length > 0 &&
        !staticRoutes.find(
          (r) => `/${r.route.toLowerCase()}` === location.pathname.toLowerCase()
        )) ||
      (id &&
        !result.find((r) => r.shortName.toLowerCase() === id.toLowerCase()))
    ) {
      navigate(`/${result[0].shortName.toLowerCase()}`, { replace: true });
    }
  }, [id, result, navigate, location]);

  return (
    <>
      <NavBar />
      <Container>
        <div>
          <h1>Alcohol Density per Census Subdivision</h1>
        </div>
      </Container>
      <Outlet />
    </>
  );
};

export default Shell;

export const dataLoader = async () => {
  const densitiesResult = store.dispatch(
    densityApi.endpoints.getDensities.initiate()
  );
  try {
    const densities = await densitiesResult.unwrap();
    console.debug("densities in Shell", densities);
    return densities;
  } finally {
    densitiesResult.unsubscribe();
  }
};
