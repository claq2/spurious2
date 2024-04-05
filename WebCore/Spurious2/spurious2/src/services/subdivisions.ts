import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { Subdivision } from './types'

export const subdivisionApi = createApi({
  reducerPath: 'subvdivisionApi',
  baseQuery: fetchBaseQuery({ baseUrl: 'https://localhost:5207/api' }),
  endpoints: (builder) => ({
    getSubdivisionById: builder.query<Subdivision, number>({
      query: (id) => `subdivisions/${id}/boundary`,
    }),
  }),
})

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const { useGetSubdivisionByIdQuery } = subdivisionApi
