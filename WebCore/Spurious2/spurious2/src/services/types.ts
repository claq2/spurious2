export interface CentreCoordinates {
  type: string,
  coordinates: number[],
}

export interface Subdivision {
  id: number,
  name: string,
  population: number,
  requestedDensityAmount: number,
  boundaryLink: string,
  centreCoordinates: CentreCoordinates,
}

export interface Density {
  address: string,
  shortName: string,
  title: string,
}

export interface Boundary {
  type: string,
  coordinates: [number[]],
}
