import { densityApi } from "../services/densities";
import { store } from "../store";

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
