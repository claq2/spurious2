import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

export const densityApi = createApi({
  reducerPath: 'densityApi',
  baseQuery: fetchBaseQuery({ baseUrl: 'https://localhost:5207/api' }),
  endpoints: (builder) => ({
    getDensities: builder.query<string[], void>({
      query: () => 'densities',
    }),
  }),
})

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const { useGetDensitiesQuery } = densityApi
