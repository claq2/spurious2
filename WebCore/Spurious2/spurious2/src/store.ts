import { configureStore } from "@reduxjs/toolkit";
import { subdivisionApi } from "./services/subdivisions";
import { densityApi } from "./services/densities";
import { storeApi } from "./services/stores";

export const store = configureStore({
  reducer: {
    [subdivisionApi.reducerPath]: subdivisionApi.reducer,
    [densityApi.reducerPath]: densityApi.reducer,
    [storeApi.reducerPath]: storeApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware()
      .concat(subdivisionApi.middleware)
      .concat(densityApi.middleware)
      .concat(storeApi.middleware),
});
