export interface Point {
  type: string;
  coordinates: number[];
}

export interface Subdivision {
  id: number;
  name: string;
  population: number;
  requestedDensityAmount: number;
  boundaryLink: string;
  centreCoordinates: Point;
}

export interface Density {
  address: string;
  shortName: string;
  title: string;
}

export interface Boundary {
  type: string;
  coordinates: [number[]];
}

export interface Inventory {
  alcoholType: string;
  volume: number;
}

export interface Store {
  id: number;
  locationCoordinates: Point;
  name: string;
  city: string;
  inventories: Inventory[];
}
