import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { Density, Subdivision } from './types'

export const subdivisionApi = createApi({
  reducerPath: 'subvdivisionApi',
  baseQuery: fetchBaseQuery({ baseUrl: 'https://localhost:5207/api' }),
  endpoints: (builder) => ({
    getBoundaryBySubdivisionId: builder.query<Subdivision, number>({
      query: (id) => `subdivisions/${id}/boundary`,
    }),
    getSubdivisionsByDensity: builder.query<Density[], string>({
      query: (name) => `densities/${name}/subdivisions`
    }),
  }),
})

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const { useGetBoundaryBySubdivisionIdQuery, useGetSubdivisionsByDensityQuery, useLazyGetSubdivisionsByDensityQuery } = subdivisionApi
