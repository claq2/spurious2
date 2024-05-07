import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { Store } from "./types";

export const storeApi = createApi({
  reducerPath: "storeApi",
  baseQuery: fetchBaseQuery({ baseUrl: "/api" }),
  endpoints: (builder) => ({
    getStoresBySubdivisionId: builder.query<Store[], number>({
      query: (id) => `subdivisions/${id}/stores`,
      // transformResponse: (response: Store[]) => response.map(s =>
      // ({
      //   ...s,
      //   inventories: s.inventories.map(i =>
      //     ({ ...i, key: i.alcoholType }),
      //   )
      // }
      // )),
    }),
  }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const { useLazyGetStoresBySubdivisionIdQuery } = storeApi;
