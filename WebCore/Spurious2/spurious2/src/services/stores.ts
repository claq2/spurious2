import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { Store } from "./types";

export const storeApi = createApi({
  reducerPath: "storeApi",
  baseQuery: fetchBaseQuery({ baseUrl: "http://localhost:5207/api" }),
  endpoints: (builder) => ({
    getStoresBySubdivisionId: builder.query<Store[], number>({
      query: (id) => `subdivisions/${id}/stores`,
    }),
  }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const {
  useLazyGetStoresBySubdivisionIdQuery,
  useGetStoresBySubdivisionIdQuery,
} = storeApi;
