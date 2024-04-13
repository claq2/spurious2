import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { Density } from './types'

export const densityApi = createApi({
  reducerPath: 'densityApi',
  baseQuery: fetchBaseQuery({ baseUrl: 'http://localhost:5207/api' }),
  endpoints: (builder) => ({
    getDensities: builder.query<Density[], void>({
      query: () => 'densities',
    }),
  }),
})

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const { useGetDensitiesQuery, useLazyGetDensitiesQuery } = densityApi
