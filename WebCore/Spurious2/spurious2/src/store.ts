import { configureStore } from '@reduxjs/toolkit'
import { subdivisionApi } from './services/subdivisions'
import { densityApi } from './services/densities'

export const store = configureStore({
  reducer: {
    [subdivisionApi.reducerPath]: subdivisionApi.reducer,
    [densityApi.reducerPath]: densityApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(subdivisionApi.middleware).concat(densityApi.middleware),
})

