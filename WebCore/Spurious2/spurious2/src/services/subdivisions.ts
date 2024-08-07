import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { Boundary, Subdivision } from "./types";
import { url } from "../constants";

export const subdivisionApi = createApi({
  reducerPath: "subvdivisionApi",
  baseQuery: fetchBaseQuery({ baseUrl: url }),
  endpoints: (builder) => ({
    getBoundaryBySubdivisionId: builder.query<Boundary, number>({
      query: (id) => `subdivisions/${id}/boundary`,
    }),
    getSubdivisionsByDensity: builder.query<Subdivision[], string>({
      query: (name) => `densities/${name}/subdivisions`,
    }),
  }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const {
  useLazyGetBoundaryBySubdivisionIdQuery,
  useGetSubdivisionsByDensityQuery,
  useLazyGetSubdivisionsByDensityQuery,
} = subdivisionApi;
