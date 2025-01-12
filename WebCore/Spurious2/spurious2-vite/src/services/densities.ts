import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { Density } from "./types";
import { url } from "../constants";

export const densityApi = createApi({
  reducerPath: "densityApi",
  baseQuery: fetchBaseQuery({ baseUrl: url }),
  endpoints: (builder) => ({
    getDensities: builder.query<Density[], void>({
      query: () => "densities",
    }),
  }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const { useGetDensitiesQuery, useLazyGetDensitiesQuery } = densityApi;
