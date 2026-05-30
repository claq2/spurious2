import { render, screen } from "@testing-library/react";
import { describe, expect, it, vi } from "vitest";
import MapController from "./MapController";

const hookMocks = vi.hoisted(() => ({
  useLazyGetStoresBySubdivisionIdQuery: vi.fn(),
  useLazyGetBoundaryBySubdivisionIdQuery: vi.fn(),
  popup: {
    setOptions: vi.fn(),
    open: vi.fn(),
    close: vi.fn(),
  },
  mapRefMock: {
    sources: { add: vi.fn() },
    layers: { add: vi.fn() },
    popups: { add: vi.fn() },
    events: { add: vi.fn() },
    setCamera: vi.fn(),
    getCamera: vi.fn(() => ({ zoom: 5 })),
  },
}));

vi.mock("./MapComponent", () => ({
  default: () => <div data-testid="map-component">Map</div>,
}));

vi.mock("../services/stores", () => ({
  useLazyGetStoresBySubdivisionIdQuery:
    hookMocks.useLazyGetStoresBySubdivisionIdQuery,
}));

vi.mock("../services/subdivisions", () => ({
  useLazyGetBoundaryBySubdivisionIdQuery:
    hookMocks.useLazyGetBoundaryBySubdivisionIdQuery,
}));

vi.mock("azure-maps-control", () => {
  class MockDataSource {
    add = vi.fn();
    clear = vi.fn();
    getShapes = vi.fn(() => [{ getBounds: () => [0, 0, 1, 1] }]);
  }

  class MockLayer {}

  return {
    data: {
      BoundingBox: {
        getCenter: () => [0.5, 0.5],
      },
      Feature: class {},
      Point: class {},
      Position: class {},
    },
    source: {
      DataSource: MockDataSource,
    },
    layer: {
      LineLayer: MockLayer,
      PolygonLayer: MockLayer,
      SymbolLayer: MockLayer,
    },
    Shape: class {},
  };
});

vi.mock("react-azure-maps", async () => {
  const React = await vi.importActual<typeof import("react")>("react");
  return {
    AzureMapsContext: React.createContext({
      mapRef: hookMocks.mapRefMock,
      isMapReady: true,
    }),
    useCreatePopup: () => hookMocks.popup,
  };
});

describe("MapController", () => {
  it("renders map and requests boundary data with subdivision id", () => {
    const getStoresQuery = vi.fn();
    const getBoundaryQuery = vi.fn();

    hookMocks.useLazyGetStoresBySubdivisionIdQuery.mockReturnValue([
      getStoresQuery,
      {
        isSuccess: false,
        isFetching: false,
        isLoading: false,
        data: [],
      },
    ]);

    hookMocks.useLazyGetBoundaryBySubdivisionIdQuery.mockReturnValue([
      getBoundaryQuery,
      {
        isSuccess: false,
        isFetching: false,
        isLoading: false,
        data: { type: "FeatureCollection", features: [] },
      },
    ]);

    render(<MapController subdivisionId={8} />);

    expect(screen.getByTestId("map-component")).toBeInTheDocument();
    expect(getBoundaryQuery).toHaveBeenCalledWith(8, true);
    expect(hookMocks.mapRefMock.sources.add).toHaveBeenCalledTimes(1);
    expect(hookMocks.mapRefMock.layers.add).toHaveBeenCalledTimes(3);
    expect(hookMocks.mapRefMock.popups.add).toHaveBeenCalledTimes(1);
    expect(getStoresQuery).not.toHaveBeenCalled();
  });
});
